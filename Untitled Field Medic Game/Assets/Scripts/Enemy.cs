using UnityEngine;

public class Enemy : MonoBehaviour
{
   public GameObject projectilePrefab;
   public Transform firePoint;

   public float fireRate = 1.5f;
   private float fireTimer;

    void Start()
    {
        InvokeRepeating(nameof(Shoot), 1f, 1.5f);
    }

    // Update is called once per frame
    /*
    void Update()
    {
        fireTimer += Time.deltaTime;

        if (fireTimer >= fireRate)
        {
            Shoot();
            fireTimer = 0f;
        }
    }
    */

    void Shoot()
    {
        GameObject projectile = Instantiate(projectilePrefab, firePoint.position, Quaternion.identity);
        // Vector2 shootDirection = (firePoint.position - transform.position).normalized;
        Vector2 shootDirection = Vector2.down;
        projectile.GetComponent<Projectile>().Initialize(shootDirection);
    }
}
