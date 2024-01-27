using System.Collections;
using UnityEngine;

public class ColorLerp : MonoBehaviour
{
    public Color targetColor;
    
    // Call this method to start the color change
    public void ChangeColor(float duration)
    {
        StartCoroutine(ChangeColorGradually(duration));
    }

    private IEnumerator ChangeColorGradually(float duration)
    {
        SpriteRenderer spriteRenderer = GetComponent<SpriteRenderer>();
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
}