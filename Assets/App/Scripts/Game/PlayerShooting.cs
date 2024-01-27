using UnityEngine;
using UnityEngine.Pool;

public class PlayerShooting : MonoBehaviour
{
    public float shootInterval = 0.5f; // Time between shots in seconds
    private float shootTimer;

    void Start()
    {
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
        BulletPool.Instance.GetFromPool();
    }
}