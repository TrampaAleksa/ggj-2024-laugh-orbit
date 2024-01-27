using System;
using System.Collections;
using UnityEngine;

public class ColorLerp : MonoBehaviour
{
    public Color targetColor;

    private Color _initialColor;
    private SpriteRenderer _spriteRenderer;

    private void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _initialColor = _spriteRenderer.color;
    }

    // Call this method to start the color change
    public void ChangeColor(float duration)
    {
        StartCoroutine(ChangeColorGradually(duration));
    }

    private IEnumerator ChangeColorGradually(float duration)
    {
        SpriteRenderer spriteRenderer = _spriteRenderer;
        if (spriteRenderer == null)
        {
            yield break; // Exit if no SpriteRenderer is found
        }

        Color startColor = spriteRenderer.color; // Store the original color
        float time = 0; // Initialize time

        while (time < duration)
        {
            // Interpolate color based on the elapsed time
            spriteRenderer.color = Color.Lerp(startColor, targetColor, time / duration);
            time += Time.deltaTime; // Increment time by the time taken to render the last frame
            yield return null; // Wait for the next frame
        }

        spriteRenderer.color = targetColor; // Ensure the final color is set
    }

    public void ResetColor()
    {
        _spriteRenderer.color = _initialColor;
    }
}