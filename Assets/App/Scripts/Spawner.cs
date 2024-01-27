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
        InitializeEnemyPool();
        InitializeTimer();
        CalculateScreenBoundaries();
    }

    private void Update()
    {
        timer -= Time.deltaTime;

        if (timer <= 0)
        {
            SpawnEnemy();
            timer = timer = randomSpawnInterval;
        }
    }

    private void InitializeEnemyPool()
    {
        enemyPool = new ObjectPool<Enemy>(
            createFunc: () => Instantiate(enemyPrefab, this.transform),
            actionOnGet: (enemy) => enemy.gameObject.SetActive(true),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
            maxSize: 50 // Set according to your needs
        );
    }

    private void InitializeTimer()
    {
        timer = randomSpawnInterval;
    }

    private void CalculateScreenBoundaries()
    {
        float screenHalfWidth = CalculateScreenHalfWidth();
        float enemyHalfWidth = CalculateEnemyHalfWidth();
        minX = -screenHalfWidth + enemyHalfWidth;
        maxX = screenHalfWidth - enemyHalfWidth;
    }

    private float CalculateScreenHalfWidth()
    {
        return Camera.main.aspect * Camera.main.orthographicSize;
    }

    private float CalculateEnemyHalfWidth()
    {
        return enemyPrefab.GetComponent<SpriteRenderer>().bounds.extents.x;
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