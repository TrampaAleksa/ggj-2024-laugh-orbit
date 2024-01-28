using System.Collections;
using UnityEngine;

public class KnockbackEffect : MonoBehaviour
{
    public void ApplyKnockback(Transform enemy, float knockbackStrength, float duration)
    {
        if (enemy.gameObject.activeSelf)
        {
            StartCoroutine(KnockbackCoroutine(enemy, knockbackStrength, duration));
        }
    }

    private IEnumerator KnockbackCoroutine(Transform enemy, float knockbackStrength, float duration)
    {
        float timeElapsed = 0f;

        while (timeElapsed < duration)
        {
            enemy.Translate(Vector3.up * (knockbackStrength * Time.deltaTime));

            // Increment the elapsed time
            timeElapsed += Time.deltaTime;

            yield return null;
        }
    }
}