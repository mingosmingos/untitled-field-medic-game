using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Spawn(new Vector2(2, 2));
        Spawn(new Vector2(4, 2));
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
