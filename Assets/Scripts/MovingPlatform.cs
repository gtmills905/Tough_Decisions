using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float speed = 3.0f;              // Speed of the platform's movement
    public float height = 5.0f;             // How far the platform moves up and down
    private Vector3 startPosition;          // The initial position of the platform

    void Start()
    {
        // Store the starting position of the platform
        startPosition = transform.position;
    }

    void Update()
    {
        // Calculate the new Y position using a sine wave for smooth movement
        float newY = startPosition.y + Mathf.Sin(Time.time * speed) * height;

        // Update the platform's position
        transform.position = new Vector3(startPosition.x, newY, startPosition.z);
    }
}
