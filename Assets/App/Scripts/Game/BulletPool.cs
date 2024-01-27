using System.Collections.Generic;
using UnityEngine;

public class BulletPool : MonoBehaviour
{
    public static BulletPool Instance;

    public ExtendedObjectPool<Bullet> bulletPool; // Reference to the bullet object pool
    public Bullet bulletPrefab;
    private PlayerCharacter _playerCharacter;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Make sure the pool persists across scene loads
        }
        else
        {
            Destroy(gameObject); // Ensure there's only one instance in the scene
        }

        InitializeBulletPool();
    }

    private void InitializeBulletPool()
    {
        _playerCharacter = FindObjectOfType<PlayerCharacter>();
        
        bulletPool = new ExtendedObjectPool<Bullet>(
            createFunc: () => Instantiate(bulletPrefab),
            actionOnGet: GetBulletAction,
            actionOnRelease: (bullet) => bullet.gameObject.SetActive(false),
            actionOnDestroy: (bullet) => Destroy(bullet.gameObject),
            maxSize: 50 // Adjust the pool size as needed
        );
    }

    private void GetBulletAction(Bullet bullet)
    {
        bullet.transform.position = _playerCharacter.transform.position;
        bullet.transform.rotation = _playerCharacter.transform.rotation;
        gameObject.SetActive(true);
    }

    public void ReturnToPool(Bullet bullet)
    {
        bullet.gameObject.SetActive(false); // Optionally reset other bullet properties here
        bulletPool.Release(bullet);
    }

    public Bullet GetFromPool()
    {
        return bulletPool.Get(); // Get a bullet from the pool
    }

    public List<Bullet> GetActiveBullets() => bulletPool.GetActiveObjects(); // Get a list of active bullets
}