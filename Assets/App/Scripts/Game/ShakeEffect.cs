using System;
using DG.Tweening;
using UnityEngine;

public class ShakeEffect : MonoBehaviour
{
    public float strength = 1f; // Strength of the shake effect
    public int vibrato = 10; // Indicates how much will the shake vibrate
    public float randomness = 90f; // The shake randomness
    public bool snapping = false; // If true, the shake will snap to integer values
    public float loopDuration = 1f;

    private Action onShakeFinished;

    private RectTransform _rectTransform;

    private void Awake()
    {
        _rectTransform = GetComponent<RectTransform>();
    }

    public ShakeEffect StartShake()
    {
        _rectTransform.DOShakeAnchorPos(loopDuration, new Vector3(0.1f, strength, 0), vibrato, randomness, snapping)
            .SetLoops(-1, LoopType.Restart) // -1 for infinite loops
            .SetUpdate(UpdateType.Normal, true);

        // Invoke(nameof(StopShake), duration);
        return this;
    }
    
    public void StopShake()
    {
        _rectTransform.DOKill();
        onShakeFinished?.Invoke();
    }
    
    public void OnComplete(Action action)
    {
        onShakeFinished = action;
    }
}