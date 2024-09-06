using UnityEngine;

public class PickupControl : MonoBehaviour
{
    public Transform attachmentPoint; // The point where the object will be held
    public float pickupRange = 2f;    // The maximum distance for pickup detection
    private GameObject currentObject; // The object currently held
    private Collider pickupZone;      // The collider that acts as the trigger zone

    void Update()
    {
        // Check if the F key is pressed and the player is not already holding an object
        if (Input.GetKeyDown(KeyCode.F))
        {
            if (currentObject == null)
            {
                TryPickupObject();
            }
            else
            {
                DropObject();
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object that entered the trigger zone is a pickup object
        if (other.CompareTag("Pickup"))
        {
            pickupZone = other; // Store the pickup object collider
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Clear the pickup object collider when it exits the trigger zone
        if (other.CompareTag("Pickup"))
        {
            if (pickupZone == other)
            {
                pickupZone = null;
            }
        }
    }

    private void TryPickupObject()
    {
        // Check if there is a pickup object in the trigger zone
        if (pickupZone != null)
        {
            Pickup(pickupZone.gameObject);
        }
    }

    private void Pickup(GameObject obj)
    {
        currentObject = obj;
        obj.GetComponent<Rigidbody>().isKinematic = true; // Disable physics on the object
        obj.transform.position = attachmentPoint.position; // Move the object to the attachment point
        obj.transform.rotation = attachmentPoint.rotation; // Align the object's rotation
        obj.transform.SetParent(attachmentPoint); // Attach the object to the attachment point
    }

    private void DropObject()
    {
        if (currentObject != null)
        {
            currentObject.GetComponent<Rigidbody>().isKinematic = false; // Re-enable physics
            currentObject.transform.SetParent(null); // Detach the object
            currentObject = null;
        }
    }
}