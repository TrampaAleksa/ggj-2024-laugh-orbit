using UnityEngine;
using UnityEngine.Pool;

public class JokeSpawner : MonoBehaviour
{
    public ObjectPool<JokePickup> jokePickupPool;
    public JokePickup jokePickupPrefab;
    public SpawnTimer spawnTimer;
    
    private float minX, maxX;

    private void Start()
    {
        InitializeJokePickupPool();
        CalculateScreenBoundaries();
        spawnTimer.Init(SpawnJokePickup);
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

    private float CalculatePickupHalfWidth()
    {
        return jokePickupPrefab.GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    void SpawnJokePickup()
    {
        JokePickup pickup = jokePickupPool.Get();
        Vector2 spawnPosition = new Vector2(Random.Range(minX, maxX), transform.position.y);
        pickup.transform.position = spawnPosition;
        pickup.gameObject.SetActive(true);
    }
}