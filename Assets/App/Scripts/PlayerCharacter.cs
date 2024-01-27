using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCharacter : MonoBehaviour
{
    public float speed = 5.0f; // Speed of the character movement

    private float screenHalfWidthInWorldUnits;
    private Transform _transform;

    // Start is called before the first frame update
    void Start()
    {
        _transform = transform;
        
        // Calculate half the screen width in world units
        float halfPlayerWidth = _transform.localScale.x / 2f;
        screenHalfWidthInWorldUnits = Camera.main.aspect * Camera.main.orthographicSize + halfPlayerWidth;
    }

    // Update is called once per frame
    void Update()
    {
        // Get player input
        float inputX = Input.GetAxis("Horizontal");
        Vector2 newPosition = _transform.position + Vector3.right * (inputX * speed * Time.deltaTime);

        // Ensure the player does not move off screen
        newPosition.x = Mathf.Clamp(newPosition.x, -screenHalfWidthInWorldUnits, screenHalfWidthInWorldUnits);

        // Update the player's position
        _transform.position = newPosition;

        // Check for bounds and prevent the player from moving beyond the screen width
        if (_transform.position.x < -screenHalfWidthInWorldUnits)
        {
            _transform.position = new Vector2(-screenHalfWidthInWorldUnits, _transform.position.y);
        }
        if (transform.position.x > screenHalfWidthInWorldUnits)
        {
            _transform.position = new Vector2(screenHalfWidthInWorldUnits, _transform.position.y);
        }
    }


}