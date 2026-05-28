using UnityEngine;

public class WaypointSpawner : MonoBehaviour
{
    public GameObject prefab;
    public int spawnCount = 3;
    public float xOffset = 1f;
    public float yOffset = 1f;

    public enum Allegiance { Enemy, Friendly }
    public Allegiance waypointAllegiance;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        string targetTag = waypointAllegiance == Allegiance.Friendly ? "Friendly" : "Enemy";

        for (int i = 0; i < spawnCount; i++)
        {
            GameObject waypoint = Spawn(new Vector2(xOffset * i, yOffset * i));
            waypoint.tag = targetTag;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    GameObject Spawn(Vector2 offset)
    {
        return Instantiate(prefab, (Vector2)transform.position + offset, Quaternion.identity);
    }
}
