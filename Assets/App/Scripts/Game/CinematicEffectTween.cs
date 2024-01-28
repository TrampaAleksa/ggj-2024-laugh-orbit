using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Serialization;

public class CinematicEffectTween : MonoBehaviour
{
    public RectTransform topBar;
    public RectTransform bottomBar;
    private Camera _camera;

    public float cameraYOffset = -1.7f;
    public float cameraOrtographicSize = 4f;
    public float barOffset = 100f;

    public float duration = 4f;

    private void Awake()
    {
        _camera = Camera.main;
    }

    public void StartMoveAndResize()
    {
        StartCoroutine(MoveAndResize());
    }

    // private void Update()
    // {
    //     if (Input.GetKey(KeyCode.C))
    //     {
    //         StartMoveAndResize();
    //     }
    // }

    private IEnumerator MoveAndResize()
    {
        float timeElapsed = 0f;

        // Initial positions and size
        // Initial positions
        Vector2 topBarStartPosition = topBar.anchoredPosition;
        Vector2 bottomBarStartPosition = bottomBar.anchoredPosition;

        float startCameraSize = _camera.orthographicSize;
        Vector3 cameraStartPosition = _camera.transform.position;

        Vector2 topBarTargetPosition = topBarStartPosition - new Vector2(0, barOffset);
        Vector2 bottomBarTargetPosition = bottomBarStartPosition + new Vector2(0, barOffset);
        float targetCameraSize = cameraOrtographicSize;
        Vector3 cameraTargetPosition = cameraStartPosition + new Vector3(0, cameraYOffset, 0);

        while (timeElapsed < duration)
        {
            float t = timeElapsed / duration; // Normalized time

            topBar.anchoredPosition = Vector2.Lerp(topBarStartPosition, topBarTargetPosition, t);
            bottomBar.anchoredPosition = Vector2.Lerp(bottomBarStartPosition, bottomBarTargetPosition, t);
            _camera.orthographicSize = Mathf.Lerp(startCameraSize, targetCameraSize, t);
            _camera.transform.position = Vector3.Lerp(cameraStartPosition, cameraTargetPosition, t);

            timeElapsed += Time.deltaTime;
            yield return null;
        }

        // Ensure final values are set
        topBar.anchoredPosition = topBarTargetPosition;
        bottomBar.anchoredPosition = bottomBarTargetPosition;
        _camera.orthographicSize = targetCameraSize;
        _camera.transform.position = cameraTargetPosition;
    }

}