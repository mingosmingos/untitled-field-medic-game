using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NPCBehaviour : MonoBehaviour, IDamageable, IGrabbable
{
    public int maxHealth = 5;
    public int curHealth;
    public int panicMultiplier = 1;
    
    public Node currentNode;
    public List<Node> path = new List<Node>();
    public float speed = 3f;

    public float rotationSpeed = 180f;
    public float zRotationOffset = 90f;

    public enum StateMachine { Patrol, Engage, Evade, Injured }
    public StateMachine currentState;
    public bool IsGrabbable { get; private set; } = false;
    private bool isGrabbed = false;

    public Transform player;
    public GameObject projectilePrefab;
    public Transform projectilePoint; 
    public int ammunition = 10;
    public float fireRate = 1.0f;
    private float nextFireTime = 0f;

    public TrailRenderer trailRenderer;

    private Rigidbody2D rb;

    private float lastRecalculateTime = 0f;

    private void Start()
    {
        curHealth = maxHealth;
        currentNode = AStarManager.instance.FindNearestNode(transform.position);

        if (trailRenderer != null) trailRenderer.emitting = false;

        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (isGrabbed) return; 

        if (curHealth <= (maxHealth * 0.5f) && currentState != StateMachine.Injured && currentState != StateMachine.Evade)
        {
            currentState = StateMachine.Injured;
            path.Clear(); // Stop moving
        }

        // --- HANDLE TRAIL RENDERER ---
        if (currentState == StateMachine.Injured)
        {
            if (trailRenderer != null) trailRenderer.emitting = true;
            rb.bodyType = RigidbodyType2D.Kinematic;
            IsGrabbable = true;

            return; 
        }
        else
        {
            if (trailRenderer != null) trailRenderer.emitting = false;
        }

        bool playerSeen = Vector2.Distance(transform.position, player.position) < 15.0f;

        if(!playerSeen && currentState != StateMachine.Patrol && curHealth > (maxHealth * 20) / 100)
        {
            currentState = StateMachine.Patrol;
            path.Clear();
        }else if(playerSeen && currentState != StateMachine.Engage && curHealth > (maxHealth * 20) / 100 && ammunition > 0)
        {
            currentState = StateMachine.Engage;
            path.Clear();
        }else if(currentState != StateMachine.Evade && (curHealth <= (maxHealth * 20) / 100 || ammunition <= 0))
        {
            panicMultiplier = 2;
            currentState = StateMachine.Evade;
            path.Clear();
        }

        if (path.Count > 0 && rb.linearVelocity.magnitude < 0.1f && Time.time > lastRecalculateTime + 1f)
        {
            path = AStarManager.instance.GeneratePath(currentNode, path[path.Count-1]);
            lastRecalculateTime = Time.time;
        }

        switch (currentState)
        {
            case StateMachine.Patrol:
                Patrol();
                break;
            case StateMachine.Engage: 
                Engage(); 
                break;
            case StateMachine.Evade: 
                Evade(); 
                break;
        }

        CreatePath();
        RotateTowardsPath();
    }

    void Patrol()
    {
        if(path.Count == 0)
        {
            path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.AllNodes()[Random.Range(0, AStarManager.instance.AllNodes().Length)]);
        }
    }

    void Engage()
    {
        /*
        if (path.Count == 0)
        {
            path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.FindNearestNode(player.transform.position));
        }
        */
        RotateTowardsPlayer();
        Shoot();
    }

    void Evade()
    {
        if (path.Count == 0)
        {
            path = AStarManager.instance.GeneratePath(currentNode, AStarManager.instance.FindFurthestNode(player.transform.position));
        }
    }

    public void CreatePath()
    {
        if (path.Count > 0 && path[0] != null)
        {
            Vector3 targetPos = new Vector3(path[0].transform.position.x, path[0].transform.position.y, -2);
            Vector3 newPos = Vector3.MoveTowards(transform.position, targetPos, (speed * panicMultiplier) * Time.deltaTime);
            
            // ✅ Physics-compliant movement
            rb.MovePosition(newPos);

            // ✅ Slightly increased threshold to prevent getting stuck when pushed
            if (Vector2.Distance(transform.position, path[0].transform.position) < 0.3f)
            {
                currentNode = path[0];
                path.RemoveAt(0);
            }
        }
    }

    private void RotateTowardsPath()
    {
        if (path.Count > 0)
        {
            Vector3 direction = path[0].transform.position - transform.position;
            
            if (direction.sqrMagnitude > 0.01f)
            {
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
                float zRotation = angle + zRotationOffset;
                Quaternion targetRotation = Quaternion.Euler(0f, 0f, zRotation);
                transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }

    private void RotateTowardsPlayer()
    {
        Vector2 direction = player.position - transform.position;
        if (direction.sqrMagnitude > 0.01f)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            float zRotation = angle + zRotationOffset;
            Quaternion targetRotation = Quaternion.Euler(0f, 0f, zRotation);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        }
    }

    void Shoot()
    {
        if (ammunition <= 0) return;
        if (Time.time < nextFireTime) return;

        nextFireTime = Time.time + fireRate;
        ammunition--;

        Vector2 shootDirection = -transform.up; 

        GameObject projectile = Instantiate(projectilePrefab, projectilePoint.position, transform.rotation);
        
        float projAngle = Mathf.Atan2(shootDirection.y, shootDirection.x) * Mathf.Rad2Deg;
        projectile.transform.rotation = Quaternion.Euler(0, 0, projAngle);

        projectile.GetComponent<Projectile>().Initialize(shootDirection);
    }

    public void TakeDamage()
    {
        curHealth--;
        /*
        if (curHealth < 1)
        {
            IsGrabbable = true;
        }
        */
    }

    public void OnGrabbed()
    {
        isGrabbed = true;
        rb.bodyType = RigidbodyType2D.Dynamic;
        rb.linearVelocity = Vector2.zero;
    }

    public void OnDropped()
    {
        isGrabbed = false;
        rb.linearVelocity = Vector2.zero;
        if (currentState == StateMachine.Injured)
            rb.bodyType = RigidbodyType2D.Kinematic;
    }
}
