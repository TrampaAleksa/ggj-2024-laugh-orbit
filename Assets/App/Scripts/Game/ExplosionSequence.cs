using System;
using UnityEngine;

public class ExplosionSequence : MonoBehaviour
{
    public PlayerCharacter playerCharacter;
    private BossExplosion[] bossExplosions;

    private void Awake()
    {
        bossExplosions = gameObject.GetComponentsInChildren<BossExplosion>();
    }

    public void BeginSequence()
    {
        foreach (var bossExplosion in bossExplosions)
        {
            bossExplosion.Explode();
        }
        
        Invoke(nameof(EndSequence), 2f);
    }

    public void EndSequence()
    {
        var playerExplosion = ExplosionPool.Instance.GetFromPool();
        playerExplosion.transform.position = playerCharacter.transform.position;
        playerExplosion.gameObject.SetActive(true);
        playerCharacter.GetComponent<SpriteRenderer>().enabled = false;
    }
}