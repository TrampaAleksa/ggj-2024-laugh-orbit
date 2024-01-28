using UnityEngine;
using UnityEngine.Pool;

public class JokeSpawner : MonoBehaviour
{
    public JokePool jokePickupPool;
    public SpawnTimer spawnTimer;
    
    private float minX, maxX;

    private void Start()
    {
        CalculateScreenBoundaries();
        spawnTimer.Init(SpawnJokePickup);
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
        return jokePickupPool.jokePickupPrefab.GetComponent<SpriteRenderer>().bounds.extents.x;
    }

    void SpawnJokePickup()
    {
        JokePickup pickup = jokePickupPool.GetFromPool();
        Vector2 spawnPosition = new Vector2(Random.Range(minX, maxX), transform.position.y);
        pickup.transform.position = spawnPosition;
        pickup.gameObject.SetActive(true);
    }

    private void ContinueSpawning() => spawnTimer.Continue();
}