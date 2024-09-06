using System.Collections.Generic;
using UnityEngine;

public class FollowerAI : MonoBehaviour
{
    public Transform player; // Reference to the player transform
    public float followDelay = 2.0f; // 2-second delay
    public float checkInterval = 0.1f; // Interval to store positions
    public float radiusToResume = 5.0f; // Radius to resume following when stuck

    private Queue<Vector3> positionHistory; // Queue to store player positions
    private float timeSinceLastCheck;

    private Rigidbody2D rb;

    void Start()
    {
        positionHistory = new Queue<Vector3>();
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        timeSinceLastCheck += Time.deltaTime;

        // Record the player's position at intervals
        if (timeSinceLastCheck >= checkInterval)
        {
            positionHistory.Enqueue(player.position);
            timeSinceLastCheck = 0.0f;
        }

        // Remove positions older than followDelay
        while (positionHistory.Count > followDelay / checkInterval)
        {
            positionHistory.Dequeue();
        }

        if (positionHistory.Count > 0)
        {
            Vector3 targetPosition = positionHistory.Peek();
            Vector2 targetDirection = (targetPosition - transform.position).normalized;

            RaycastHit2D hit = Physics2D.Raycast(transform.position, targetDirection, 1.0f);

            if (hit.collider == null || hit.collider.transform == player)
            {
                MoveTowards(targetPosition);
            }
            else
            {
                // Check distance from player to resume movement
                float distanceToPlayer = Vector2.Distance(player.position, transform.position);
                if (distanceToPlayer >= radiusToResume)
                {
                    MoveTowards(targetPosition);
                }
            }
        }
    }

    void MoveTowards(Vector3 targetPosition)
    {
        Vector2 newPosition = Vector2.MoveTowards(rb.position, new Vector2(targetPosition.x, rb.position.y), Time.deltaTime);
        rb.MovePosition(newPosition);
    }
}
