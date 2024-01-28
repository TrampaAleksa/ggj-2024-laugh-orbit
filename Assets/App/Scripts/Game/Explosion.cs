using System;
using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particleSystem;

    private void Awake()
    {
        particleSystem = GetComponent<ParticleSystem>();
    }

    public void OnEnable()
    {
        particleSystem.Play();
        Invoke(nameof(ReturnToPoolAfterFinishing), particleSystem.main.duration);
    }

    public void OnDisable()
    {
        CancelInvoke();
    }

    private void ReturnToPoolAfterFinishing()
    {
        ExplosionPool.Instance.ReturnToPool(this);
    }
}