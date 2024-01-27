using UnityEngine;
using UnityEngine.Pool;

public class EnemySpawner : MonoBehaviour
{
    public EnemyPool enemyPool; // Reference to the enemy object pool
    public Enemy enemyPrefab;
    public SpawnTimer spawnTimer;
    
    private float _minX, _maxX;

    private void Start()
    {
        CalculateScreenBoundaries();
        spawnTimer.Init(SpawnEnemy);
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
        Enemy enemy = enemyPool.GetFromPool();
        Vector2 spawnPosition = new Vector2(Random.Range(_minX, _maxX), transform.position.y);
        enemy.transform.position = spawnPosition;
    }
}