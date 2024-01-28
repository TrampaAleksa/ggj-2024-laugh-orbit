using UnityEngine;

public class BossExplosion : MonoBehaviour
{
    public float delay;

    public void StartExplosion()
    {
        Invoke(nameof(Explode), delay);
    }
    
    public void Explode()
    {
        var explosion = ExplosionPool.Instance.GetFromPool();
        GameSounds.Instance.PlayBossExplosionSound();
        explosion.transform.position = transform.position;
        explosion.gameObject.SetActive(true);
    }
}