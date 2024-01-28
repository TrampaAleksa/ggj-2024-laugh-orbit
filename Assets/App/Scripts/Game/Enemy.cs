using System;
using System.Collections;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [HideInInspector]
    public Health health;
    
    public float speed = 5f; 
    public float despawnTime = 10f;

    public float laughDeathTimer = 1f;

    [SerializeField]
    public KnockbackEffect knockbackEffect;

    private ColorLerp _colorLerp;

    private void Awake()
    {
        health = GetComponent<Health>();
        health.OnZeroHealth.AddListener(DeactivateWithExplosion);

        _colorLerp = GetComponentInChildren<ColorLerp>();
        knockbackEffect = gameObject.AddComponent<KnockbackEffect>();
    }

    private void OnEnable()
    {
        Invoke(nameof(Deactivate), despawnTime);
    }

    private void Update()
    {
        transform.Translate(Vector3.down * (speed * Time.deltaTime));
    }

    public void Deactivate()
    {
        EnemyPool.Instance.ReturnToPool(this);
        _colorLerp.ResetColor();
        health.ResetHealth();
     
    }
    public void DeactivateWithExplosion()
    {
        var explosion = ExplosionPool.Instance.GetFromPool();
        explosion.transform.position = transform.position;
        Deactivate();
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
    
    public void StartDeathLaughing()
    {
        Invoke(nameof(FinishDeathLaughing), laughDeathTimer);
        _colorLerp.ChangeColor(laughDeathTimer * 0.8f);
    }

    public void FinishDeathLaughing()
    {
        DeactivateWithExplosion();
    }

}