using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    public ObjectPool<Enemy> enemyPool; // Reference to the enemy object pool
    public Enemy enemyPrefab;
    public float maxSpawnInterval = 2f; // Time between each spawn
    public float minSpawnInterval = 2f; // Time between each spawn
    
    private float timer; // Timer to keep track of spawning\
    private float minX, maxX;
    private float randomSpawnInterval => Random.Range(minSpawnInterval, maxSpawnInterval);

    private void Start()
    {
        enemyPool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(enemyPrefab, this.transform),
            actionOnGet: (enemy) => enemy.gameObject.SetActive(true),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
            maxSize: 50 // Set according to your needs
        );
        // Initialize timer
        timer = randomSpawnInterval;
        
        // Calculate screen boundaries
        float screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize;
        float enemyHalfWidth = enemyPrefab.GetComponent<SpriteRenderer>().bounds.extents.x;
        minX = -screenHalfWidthInWorldUnits + enemyHalfWidth;
        maxX = screenHalfWidthInWorldUnits - enemyHalfWidth;
    }

    private void Update()
    {
        // Update the timer each frame
        timer -= Time.deltaTime;

        // Check if it's time to spawn a new enemy
        if (timer <= 0)
        {
            SpawnEnemy();
            timer = timer = randomSpawnInterval;
        }
    }

    void SpawnEnemy()
    {
        // Get an enemy from the pool and position it at the spawner's position
        Enemy enemy = enemyPool.Get();
        Vector2 spawnPosition = new Vector2(Random.Range(minX, maxX), transform.position.y);
        enemy.transform.position = spawnPosition;
        enemy.gameObject.SetActive(true);
    }
}