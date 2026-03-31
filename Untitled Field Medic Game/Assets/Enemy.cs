using UnityEngine;

public class Enemy : MonoBehaviour
{
   public GameObject projectilePrefab;
   public Transform firePoint;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        // Vector2 shootDirection = (firePoint.position - transform.position).normalized;
        Vector2 shootDirection = Vector2.down;
        projectile.GetComponent<Projectile>().Initialize(shootDirection);
    }
}
