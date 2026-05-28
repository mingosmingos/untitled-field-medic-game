using UnityEngine;
using TMPro;

public class NPCBehaviour : MonoBehaviour, IGrabbable, IDamageable
{
    public bool IsGrabbable { get; private set; } = true;
    
    [Header("Health")]
    public int healthPoints = 1;

    [Header("Offense")]
    public GameObject projectilePrefab;
    public Transform projectilePoint;
    public int ammunition = 2;
    public float fireRate = 1.5f; // NEW: Seconds between shots
    private float nextFireTime = 0f;

    [SerializeField] private TextMeshProUGUI healthPointsText;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        UpdateHealthPointsText();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage()
    {
        healthPoints--;
        UpdateHealthPointsText();
        if (healthPoints < 1)
        {
            GetComponent<WaypointNavigation>().enabled = false; // Dead behaviour.
            IsGrabbable = true;
        }
    }

    private void UpdateHealthPointsText()
    {
        if (healthPointsText == null) return;

        healthPointsText.text = healthPoints.ToString();
    }

    public void OnVisionDetected(Collider2D other)
    {
        /*
        if (other.CompareTag("Player"))
        {
            Shoot();
        }
        */

        Shoot();
    }

    void Shoot()
    {
        if (ammunition < 1) return;

        if (Time.time < nextFireTime) return;

        nextFireTime = Time.time + fireRate;
        ammunition--;
        
        Vector2 shootDirection = -transform.up;

        GameObject projectile = Instantiate(projectilePrefab, projectilePoint.position, transform.rotation);
        projectile.GetComponent<Projectile>().Initialize(shootDirection);
        
        Debug.Log($"[{gameObject.name}] Shot a projectile. Remaining ammunition: {ammunition}");
    }
}