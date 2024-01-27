using UnityEngine;

public class JokePool : MonoBehaviour
{
    public static JokePool Instance;
    
    public ExtendedObjectPool<JokePickup> jokePickupPool; // Reference to the enemy object pool
    public JokePickup jokePickupPrefab;
    
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
        jokePickupPool = new ExtendedObjectPool<JokePickup>(
            createFunc: () => Instantiate(jokePickupPrefab),
            actionOnGet: (jokePickup) => jokePickup.gameObject.SetActive(true),
            actionOnRelease: (jokePickup) => jokePickup.gameObject.SetActive(false),
            actionOnDestroy: (jokePickup) => Destroy(jokePickup.gameObject),
            maxSize: 20
        );
    }
    
    public void ReturnToPool(JokePickup jokePickup)
    {
        jokePickup.gameObject.SetActive(false);
        jokePickupPool.Release(jokePickup);
    }
    
    public JokePickup GetFromPool()
    {
        return jokePickupPool.Get();
    }
}