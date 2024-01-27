using System.Collections.Generic;
using UnityEngine;

public class EnemyPool : MonoBehaviour
{
    public static EnemyPool Instance;
    
    public ExtendedObjectPool<Enemy> enemyPool; // Reference to the enemy object pool
    public Enemy enemyPrefab;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        
        InitializeEnemyPool();
    }
    
    private void InitializeEnemyPool()
    {
        enemyPool = new ExtendedObjectPool<Enemy>(
            createFunc: () => Instantiate(enemyPrefab),
            actionOnGet: (enemy) => enemy.gameObject.SetActive(true),
            actionOnRelease: (enemy) => enemy.gameObject.SetActive(false),
            actionOnDestroy: (enemy) => Destroy(enemy.gameObject),
            maxSize: 50
        );
    }
    
    public void ReturnToPool(Enemy enemy)
    {
        enemy.gameObject.SetActive(false);
        enemyPool.Release(enemy);
    }
    
    public Enemy GetFromPool()
    {
        return enemyPool.Get();
    }
    
    public List<Enemy> GetActiveEnemies() => enemyPool.GetActiveObjects();

}