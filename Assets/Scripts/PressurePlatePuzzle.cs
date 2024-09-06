using UnityEngine;

public class PressurePlatePuzzle : MonoBehaviour
{
    public GameObject door; // Reference to the door GameObject
    private bool weightOnPlate = false;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            weightOnPlate = true;
            door.SetActive(false); // Disable the door
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Pickup"))
        {
            weightOnPlate = false;
            Invoke("EnableDoor", 3f); // Re-enable the door after 3 seconds
        }
    }

    private void EnableDoor()
    {
        if (!weightOnPlate)
        {
            door.SetActive(true); // Re-enable the door
        }
    }
}
