using System;
using System.Collections;
using UnityEngine;

public class UiPopInTweener : MonoBehaviour
{
    private Vector3 originalPosition;
    private RectTransform _rectTransform;

    private void Awake()
    {
        originalPosition = _rectTransform.anchoredPosition;
        _rectTransform = GetComponent<RectTransform>();
    }

    public void PopIn(float duration)
    {
        _rectTransform.anchoredPosition = originalPosition;
        gameObject.SetActive(true);
        StartCoroutine(MoveAndResize(duration));
    }

    private IEnumerator MoveAndResize(float duration)
    {
        float timeElapsed = 0f;

        var center = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2, 0));
        Vector2 targetPosition = new Vector2(center.x, center.y);

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration; // Normalized time

            _rectTransform.anchoredPosition = Vector2.Lerp(originalPosition, targetPosition, t);

            timeElapsed += Time.unscaledDeltaTime;
            yield return null;
        }
        _rectTransform.anchoredPosition = targetPosition;
    }
}