using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float speed = 5.0f; // Speed of the player movement

    private float screenHalfWidthInWorldUnits;
    private Transform _transform;
    private SpriteRenderer spriteRenderer;

    void Start()
    {
        _transform = transform;
        spriteRenderer = GetComponent<SpriteRenderer>();

        if (spriteRenderer == null)
        {
            Debug.LogError("SpriteRenderer component not found on the object.");
            return;
        }

        float spriteWidth = spriteRenderer.sprite.bounds.size.x;
        float halfSpriteWidth = spriteWidth * 0.5f * _transform.localScale.x; // Consider the sprite's scale
        screenHalfWidthInWorldUnits = (Camera.main.aspect * Camera.main.orthographicSize) - halfSpriteWidth;
    }

    void Update()
    {
        if (spriteRenderer == null) return;

        // Get player input
        float inputX = Input.GetAxis("Horizontal");
        Vector2 newPosition = _transform.position + Vector3.right * (inputX * speed * Time.deltaTime);

        // Ensure the player does not move off screen
        newPosition.x = Mathf.Clamp(newPosition.x, -screenHalfWidthInWorldUnits, screenHalfWidthInWorldUnits);

        // Update the player's position
        _transform.position = newPosition;
    }
}