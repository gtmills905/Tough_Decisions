using UnityEngine;

public class FeatureUnlockTrigger : MonoBehaviour
{
    public enum FeatureType { BoostShoes, HandBlasters, NoLegs, NormalForm }
    public FeatureType featureToUnlock;

    void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            PlayerController player = other.GetComponent<PlayerController>();
            if (player != null)
            {
                // Unlock the specific feature based on the feature type
                switch (featureToUnlock)
                {
                    case FeatureType.BoostShoes:
                        player.UnlockBoostShoes();
                        Debug.Log("Boost Shoes Unlocked!");
                        break;
                    case FeatureType.HandBlasters:
                        player.UnlockHandBlasters();
                        Debug.Log("Hand Blasters Unlocked!");
                        break;
                    case FeatureType.NoLegs:
                        player.UnlockNoLegs();
                        Debug.Log("No Legs Unlocked!");
                        break;
                    case FeatureType.NormalForm:
                        player.UnlockNormalForm();
                        Debug.Log("Normal Form Unlocked!");
                        break;
                }

                // Optionally, destroy the trigger after it's used
                Destroy(gameObject);
            }
        }
    }
}

