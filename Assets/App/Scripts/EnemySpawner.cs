using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    public ObjectPool<Enemy> enemyPool; // Reference to the enemy object pool
    public Enemy enemyPrefab;
    public SpawnTimer spawnTimer;
    
    private float _minX, _maxX;

    private void Start()
    {
        InitializeEnemyPool();
        CalculateScreenBoundaries();
        spawnTimer.Init(SpawnEnemy);
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

    private void CalculateScreenBoundaries()
    {
        float screenHalfWidth = CalculateScreenHalfWidth();
        float enemyHalfWidth = CalculateEnemyHalfWidth();
        _minX = -screenHalfWidth + enemyHalfWidth;
        _maxX = screenHalfWidth - enemyHalfWidth;
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
        Vector2 spawnPosition = new Vector2(Random.Range(_minX, _maxX), transform.position.y);
        enemy.transform.position = spawnPosition;
        enemy.gameObject.SetActive(true);
    }
}