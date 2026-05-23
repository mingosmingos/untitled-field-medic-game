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
        GameObject projectile = Instantiate(projectilePrefab, projectilePoint.position, Quaternion.identity);
        Vector2 shootDirection = Vector2.down;
        projectile.GetComponent<Projectile>().Initialize(shootDirection);
    }
}