using System;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class JokeNarrator : MonoBehaviour
{
    public static JokeNarrator Instance; 
    public RectTransform endPosition; // Use RectTransform for UI elements
    public float entryTime;

    private Vector2 _startPosition; // Store as Vector2 for RectTransform
    private ShakeEffect _shakeEffect; // Ensure your ShakeEffect script is adapted for RectTransform
    
    private void Awake()
    {
        Instance = this;
        _shakeEffect = GetComponent<ShakeEffect>(); // Ensure ShakeEffect can handle RectTransform

        _startPosition = GetComponent<RectTransform>().anchoredPosition; // Use anchoredPosition for RectTransform
    }

    public void StartNarrating()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();
        
        // Move to the end position
        rectTransform.DOAnchorPos(endPosition.anchoredPosition, entryTime)
            .SetEase(Ease.InOutSine)
            .OnComplete(() =>
            {
                _shakeEffect.StartShake();
            });
    }
    
    public void EndNarrating()
    {
        RectTransform rectTransform = GetComponent<RectTransform>();

        _shakeEffect.StopShake();
        // Move back to the start position
        rectTransform.DOAnchorPos(_startPosition, 0.3f).SetEase(Ease.InOutSine);
    }
}