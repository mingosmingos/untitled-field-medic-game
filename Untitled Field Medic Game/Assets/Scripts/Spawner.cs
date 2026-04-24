using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    public int spawnCount = 3;
    public float xOffset = 2f;
    public float yOffset = 2f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < spawnCount; i++)
        {
            Spawn(new Vector2(xOffset * i, yOffset));
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    void Spawn(Vector2 offset)
    {
        Vector2 spawnPosition = (Vector2)transform.position + offset;
        GameObject spawnedObject = Instantiate(prefab, spawnPosition, Quaternion.identity);
    }
}
