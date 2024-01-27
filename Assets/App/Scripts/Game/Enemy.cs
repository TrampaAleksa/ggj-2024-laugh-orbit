using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 5f; 
    public float despawnTime = 10f; 

    private void OnEnable()
    {
        Invoke(nameof(Deactivate), despawnTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.down * (speed * Time.deltaTime));
    }

    private void Deactivate()
    {
        EnemyPool.Instance.ReturnToPool(this);
    }

    private void OnDisable()
    {
        CancelInvoke(); // Ensure that no invokes are left pending
    }

    private void OnTriggerEnter2D( Collider2D other )
    {
        if (!other.gameObject.CompareTag("Player")) 
            return;
        
        if (other.gameObject.TryGetComponent(out PlayerCharacter player))
            EventManager.Instance.EnemyHitPlayerEvent(player, this);
        else
            Debug.LogError("PlayerCharacter component not found on the player object");
    }
}