using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaypointNavigation : MonoBehaviour
{
    public float speed = 2f;
    private Transform[] waypoints;
    private int currentWaypointIndex = 0;

    void Start()
    {
        GameObject[] waypointObjects = GameObject.FindGameObjectsWithTag("Waypoint");
        waypoints = System.Array.ConvertAll(waypointObjects, wp => wp.transform);
    }

    void Update()
    {
        if (waypoints == null || waypoints.Length == 0) return;

        Transform target = waypoints[currentWaypointIndex];

        transform.position = Vector2.MoveTowards(transform.position, target.position, speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, target.position) < 0.1f)
        {
            currentWaypointIndex = (currentWaypointIndex + 1) % waypoints.Length;
        }
    }
}
