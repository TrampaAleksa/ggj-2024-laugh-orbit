using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f; // Speed at which the enemy moves down
    public float despawnTime = 10f; // Time after which the enemy despawns

    private void OnEnable()
    {
        Invoke(nameof(Deactivate), despawnTime); // Schedule the deactivation
    }

    private void Update()
    {
        // Move the enemy downwards each frame
        transform.Translate(Vector3.down * (speed * Time.deltaTime));
    }

    private void Deactivate()
    {
        gameObject.SetActive(false); // Deactivate the enemy
    }

    private void OnDisable()
    {
        CancelInvoke(); // Ensure that no invokes are left pending
    }
}