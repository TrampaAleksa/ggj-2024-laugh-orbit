using UnityEngine;
using UnityEngine.Pool;

public class JokeSpawner : MonoBehaviour
{
    public ObjectPool<JokePickup> jokePickupPool;
    public JokePickup jokePickupPrefab;
    public float maxSpawnInterval = 2f; 
    public float minSpawnInterval = 2f;
    public bool IsSpawningActive = true;
    
    private float timer;
    private float minX, maxX;
    private float randomSpawnInterval => Random.Range(minSpawnInterval, maxSpawnInterval);

    private void Start()
    {
        InitializeJokePickupPool();
        InitializeTimer();
        CalculateScreenBoundaries();
    }

    private void Update()
    {
        if (!IsSpawningActive)
            return;
        
        timer -= Time.deltaTime;
        
        if (timer <= 0)
        {
            SpawnJokePickup();
            timer = randomSpawnInterval;
        }
    }

    private void InitializeJokePickupPool()
    {
        jokePickupPool = new ObjectPool<JokePickup>(
            createFunc: () => Instantiate(jokePickupPrefab, this.transform),
            actionOnGet: (pickup) => pickup.gameObject.SetActive(true),
            actionOnRelease: (pickup) => pickup.gameObject.SetActive(false),
            actionOnDestroy: (pickup) => Destroy(pickup.gameObject),
            maxSize: 20 // Adjust as needed
        );
    }

    private void InitializeTimer()
    {
        timer = randomSpawnInterval;
    }

    private void CalculateScreenBoundaries()
    {
        float screenHalfWidth = CalculateScreenHalfWidth();
        float enemyHalfWidth = CalculatePickupHalfWidth();
        minX = -screenHalfWidth + enemyHalfWidth;
        maxX = screenHalfWidth - enemyHalfWidth;
    }
    
    private float CalculateScreenHalfWidth()
    {
        return Camera.main.aspect * Camera.main.orthographicSize;
    }

    void SpawnJokePickup()
    {
        JokePickup pickup = jokePickupPool.Get();
        Vector2 spawnPosition = new Vector2(Random.Range(minX, maxX), transform.position.y);
        pickup.transform.position = spawnPosition;
        pickup.gameObject.SetActive(true);
    }


    private float CalculatePickupHalfWidth()
    {
        // Adjust if JokePickup uses a different component or sizing logic
        return jokePickupPrefab.GetComponent<SpriteRenderer>().bounds.extents.x;
    }

}