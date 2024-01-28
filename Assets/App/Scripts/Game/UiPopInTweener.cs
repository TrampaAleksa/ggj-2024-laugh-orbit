using System;
using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Serialization;

public class UiPopInTweener : MonoBehaviour
{
    public RectTransform rectTransform;
    public RectTransform originalPosition;

    public void PopIn(float duration)
    {
        rectTransform.anchoredPosition = originalPosition.anchoredPosition;

        gameObject.SetActive(true);
        MoveToCenter(duration);
    }
    
    public void PopOut(float duration)
    {
        gameObject.SetActive(true);
        MoveToOriginalPosition(duration);
    }
    
    private void MoveToCenter(float duration)
    {
        // Calculate the center of the screen in the local space of the RectTransform
        Vector2 screenCenter = new Vector2(Screen.width / 2f, Screen.height / 2f);
        Vector2 localCenter = Vector2.zero;
        Canvas parentCanvas = rectTransform.GetComponentInParent<Canvas>();
        if (parentCanvas != null)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(parentCanvas.GetComponent<RectTransform>(), screenCenter, parentCanvas.worldCamera, out localCenter);
        }

        // Use DoTween to move the RectTransform towards the calculated center
        rectTransform.DOAnchorPos(localCenter, duration).SetEase(Ease.OutQuad).SetUpdate(true);
    }
    
    private void MoveToOriginalPosition(float duration)
    {
        rectTransform.DOAnchorPos(originalPosition.anchoredPosition, duration).SetEase(Ease.OutQuad).SetUpdate(true);
    }
    
}