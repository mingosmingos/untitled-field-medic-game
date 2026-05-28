using UnityEngine;

public class WaypointNavigation : MonoBehaviour
{
    public enum Allegiance { Enemy, Friendly }
    public Allegiance unitAllegiance;


    public float speed = 2f; // It should depend on the entities' speed stat.

    private Transform[] waypoints;
    private int currentWaypointIndex = 0;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string targetTag = unitAllegiance == Allegiance.Friendly ? "Friendly" : "Enemy";

        GameObject[] waypointObjects = GameObject.FindGameObjectsWithTag(targetTag);

        if (waypointObjects.Length == 0)
        {
            Debug.LogWarning($"[{gameObject.name}] No waypoints found with tag '{targetTag}'.");
            return;
        }

        waypoints = System.Array.ConvertAll(waypointObjects, wp => wp.transform);
    }

    // Update is called once per frame
    void Update()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Transform target = waypoints[currentWaypointIndex];

        Vector3 direction = target.position - transform.position;

        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        float zRotation = angle + 90f;
        transform.rotation = Quaternion.Euler(0f, 0f, zRotation);

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}
