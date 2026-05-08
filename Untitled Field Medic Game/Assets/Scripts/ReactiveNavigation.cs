using UnityEngine;

public class ReactiveNavigation : MonoBehaviour
{
    public Transform target;
    public float detectionRange = 5f;
    public float speed = 2f;
    private Rigidbody2D rb;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance = Vector2.Distance(transform.position, target.position);

        if(distance < detectionRange)
        {
            Vector2 direction = (target.position - transform.position).normalized;
            rb.linearVelocity = direction * speed;
        }
        else
        {
            rb.linearVelocity = Vector2.zero;
        }
    }
}
