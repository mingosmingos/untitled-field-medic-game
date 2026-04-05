using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject enemyPrefab;
    void Start()
    {
        SpawnEnemy(new Vector2(2, 2));
        SpawnEnemy(new Vector2(4, 2));
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnEnemy(Vector2 position)
    {
        Instantiate(enemyPrefab, position, Quaternion.identity);
    }
}
