using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public float speed = 10f;
    public float maxLifetime = 5f;
    private float lifetime;
    private AudioSource BulletSource;

    private void OnEnable()
    {
        BulletSource = GetComponent<AudioSource>();
        BulletSource.Play();
        lifetime = maxLifetime;
    }

    void Update()
    {
        // Move the bullet
        transform.Translate(Vector3.up * (speed * Time.deltaTime));

        // Decrease the lifetime of the bullet
        lifetime -= Time.deltaTime;
        if (lifetime <= 0)
        {
            // Return the bullet to the pool instead of destroying it
            gameObject.SetActive(false); // Assuming the pool will handle reactivation
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.gameObject.CompareTag("Enemy")) 
            return;
        
        if (other.gameObject.TryGetComponent(out Enemy enemy))
            EventManager.Instance.BulletHitEnemyEvent(this, enemy);
        else
            Debug.LogError("Enemy component not found on the enemy object");
    }
    
    public void Deactivate()
    {
        gameObject.SetActive(false);
    }
}