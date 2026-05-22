using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public int spawnCount = 3;
    public float xOffset = 1f;
    public float yOffset = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Spawn(new Vector2(xOffset * i, yOffset * i));
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void Spawn(Vector2 offset)
    {
        Instantiate(prefab, (Vector2)transform.position + offset, Quaternion.identity);
    }
}
