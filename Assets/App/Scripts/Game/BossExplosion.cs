using UnityEngine;

public class BossExplosion : MonoBehaviour
{
    public float delay;
    
    public void Start()
    {
        Invoke(nameof(Explode), delay);
    }
    
    public void Explode()
    {
        var explosion = ExplosionPool.Instance.GetFromPool();
        explosion.transform.position = transform.position;
        explosion.gameObject.SetActive(true);
    }
}