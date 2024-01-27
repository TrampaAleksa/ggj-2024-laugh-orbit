using UnityEngine;
using UnityEngine.Pool;

public class Spawner : MonoBehaviour
{
    public ObjectPool<Enemy> enemyPool; // Reference to the enemy object pool
    public Enemy enemyPrefab;
    public float spawnInterval = 2f; // Time between each spawn

    private float timer; // Timer to keep track of spawning

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
        timer = spawnInterval;
    }

    private void Update()
    {
        // Update the timer each frame
        timer -= Time.deltaTime;

        // Check if it's time to spawn a new enemy
        if (timer <= 0)
        {
            SpawnEnemy();
            timer = spawnInterval; // Reset the timer
        }
    }

    void SpawnEnemy()
    {
        // Get an enemy from the pool and position it at the spawner's position
        Enemy enemy = enemyPool.Get();
        enemy.transform.position = transform.position;
        enemy.gameObject.SetActive(true);
    }
}