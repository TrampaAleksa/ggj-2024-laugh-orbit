using System;
using DG.Tweening;
using UnityEngine;

public class ExplosionSequence : MonoBehaviour
{
    public PlayerCharacter playerCharacter;
    private BossExplosion[] bossExplosions;
    public float endSequenceDelay = 0.3f;

    private void Awake()
    {
        bossExplosions = gameObject.GetComponentsInChildren<BossExplosion>();
    }

    private Action onEndSequenceAction;

    public void BeginSequence(Action onEndSequence)
    {
        onEndSequenceAction = onEndSequence;
        
        foreach (var bossExplosion in bossExplosions)
        {
            bossExplosion.StartExplosion();
        }
        
        Invoke(nameof(DisableBossAfterFinalExplosion), 2f);
        Invoke(nameof(EndSequence), endSequenceDelay);
    }

    private void DisableBossAfterFinalExplosion()
    {
        transform.parent.GetComponent<SpriteRenderer>().DOFade(0f, 0.3f).SetEase(Ease.InOutSine);
    }

    public void EndSequence()
    {
        var playerExplosion = ExplosionPool.Instance.GetFromPool();
        playerExplosion.transform.position = playerCharacter.transform.position;
        playerExplosion.gameObject.SetActive(true);
        playerCharacter.playerRenderer.DOFade(0f, 0.3f).SetEase(Ease.InOutSine);
        
        onEndSequenceAction?.Invoke();
    }

    // public void Update()
    // {
    //     if (Input.GetKeyDown(KeyCode.Space))
    //     {
    //         BeginSequence();
    //     }
    // }
}