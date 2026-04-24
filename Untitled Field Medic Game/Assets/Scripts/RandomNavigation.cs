using UnityEngine;

public class RandomNavigation : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public float speed = 2f;
    public float changeDirectionTime = 2f;
    private Vector2 direction;
    private float timer;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        ChangeDirection();
    }

    void Update()
    {
        timer -= Time.deltaTime;
        if (timer <= 0)
        {
            ChangeDirection();
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = direction * speed;
    }

    void ChangeDirection()
    {
        direction = Random.insideUnitCircle.normalized;
        timer = changeDirectionTime;
    }
}
