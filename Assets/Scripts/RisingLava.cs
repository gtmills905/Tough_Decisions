using UnityEngine;

public class RisingLava : MonoBehaviour
{
    // Speed at which the lava rises
    public float riseSpeed = 0.5f;

    // Maximum height the lava can reach
    public float maxHeight = 10.0f;

    // Update is called once per frame
    void Update()
    {
        // Check if the lava has reached the maximum height
        if (transform.position.y < maxHeight)
        {
            // Move the lava upwards
            transform.position += Vector3.up * riseSpeed * Time.deltaTime;
        }
    }
}
