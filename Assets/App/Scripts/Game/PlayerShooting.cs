using UnityEngine;
using UnityEngine.Pool;

public class PlayerShooting : MonoBehaviour
{
    public Bullet bulletPrefab;
    public Transform bulletSpawnPoint;
    private ObjectPool<Bullet> bulletPool;

    public float shootInterval = 0.5f; // Time between shots in seconds
    private float shootTimer;

    void Start()
    {
        // Initialize the bullet pool
        bulletPool = new ObjectPool<Bullet>(
            createFunc: () => Instantiate(bulletPrefab), // Method to create a new bullet
            actionOnGet: (bullet) => bullet.Initialize(bulletSpawnPoint.position, Quaternion.identity), // Initialize the bullet when retrieved from the pool
            actionOnRelease: (bullet) => bullet.gameObject.SetActive(false), // Deactivate the bullet when returned to the pool
            actionOnDestroy: (bullet) => Destroy(bullet.gameObject), // Destroy the bullet when the pool is cleared
            maxSize: 100 // Maximum number of bullets the pool can hold
        );

        shootTimer = shootInterval; // Initialize the shoot timer
    }

    void Update()
    {
        // Update the shoot timer
        shootTimer -= Time.deltaTime;

        // Check if it's time to shoot a new bullet
        if (shootTimer <= 0)
        {
            ShootBullet();
            shootTimer = shootInterval; // Reset the shoot timer
        }
    }

    void ShootBullet()
    {
        // Get a bullet from the pool and initialize it
        bulletPool.Get();
    }
}