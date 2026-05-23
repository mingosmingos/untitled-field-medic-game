using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 3f;

    private Vector2 direction;
    public void Initialize(Vector2 direction)
    {
        this.direction = direction.normalized;
        Destroy(gameObject, lifetime);
    }
    void Start()
    {

    }

    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        other.GetComponent<IDamageable>()?.TakeDamage();
        Destroy(gameObject);
    }
}