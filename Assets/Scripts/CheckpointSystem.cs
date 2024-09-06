using UnityEngine;
using UnityEngine.UI;

public class CheckpointSystem : MonoBehaviour
{
    public Transform[] checkpoints;
    private int currentCheckpoint = 0;
    private float deathWaterLevel = 0f;
    private float timeSinceStart = 0f;
    public float deathWaterRiseRate = 0.5f; // Units per minute
    public Transform deathWater;

    void Update()
    {
        timeSinceStart += Time.deltaTime;
        deathWaterLevel += (deathWaterRiseRate / 60f) * Time.deltaTime;
        deathWater.position = new Vector3(deathWater.position.x, deathWaterLevel, deathWater.position.z);

        if (Input.GetKeyDown(KeyCode.R))
        {
            ResetToLastCheckpoint();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            currentCheckpoint = System.Array.IndexOf(checkpoints, other.transform);
            // Disable checkpoints before the current one
            for (int i = 0; i < currentCheckpoint; i++)
            {
                checkpoints[i].gameObject.SetActive(false);
            }
        }
    }

    void ResetToLastCheckpoint()
    {
        transform.position = checkpoints[currentCheckpoint].position;
        // Reverse death water and time to last checkpoint
        deathWaterLevel = checkpoints[currentCheckpoint].position.y - 5; // Example
    }
}
