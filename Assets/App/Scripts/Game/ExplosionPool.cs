using UnityEngine;
using System.Collections.Generic;

public class ExplosionPool : MonoBehaviour
{
    public static ExplosionPool Instance;

    public ExtendedObjectPool<Explosion> explosionPool; // Reference to the explosion object pool
    public Explosion explosionPrefab;

    private void Awake()
    {
        Instance = this;
        InitializeExplosionPool();
    }

    private void InitializeExplosionPool()
    {
        explosionPool = new ExtendedObjectPool<Explosion>(
            createFunc: () => Instantiate(explosionPrefab),
            actionOnGet: (explosion) => explosion.gameObject.SetActive(true),
            actionOnRelease: (explosion) => explosion.gameObject.SetActive(false),
            actionOnDestroy: (explosion) => Destroy(explosion.gameObject),
            maxSize: 50 // Adjust pool size as needed
        );
    }

    public void ReturnToPool(Explosion explosion)
    {
        explosion.gameObject.SetActive(false); // Optionally, reset other properties or state here
        explosionPool.Release(explosion);
    }

    public Explosion GetFromPool()
    {
        return explosionPool.Get(); // Get an explosion from the pool
    }

    public List<Explosion> GetActiveExplosions() => explosionPool.GetActiveObjects(); // Get a list of active explosions
}