using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health = 3; // Number of hits to kill the enemy
    public float attackRange = 2.0f; // Range within which the enemy will attack the player
    public float attackDamage = 10.0f; // Damage dealt to the player
    public float attackCooldown = 1.0f; // Time between attacks
    public float moveSpeed = 3.0f; // Speed at which the enemy moves towards the player
    public float detectionRange = 10.0f; // Range within which the enemy starts chasing the player

    private float lastAttackTime = 0f;
    private Transform player;
    private bool isDead = false;
    private Rigidbody rb;

    void Start()
    {
        // Find the player by tag (make sure the player has the "Player" tag)
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (isDead) return;

        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Move towards the player if within detection range
        if (distanceToPlayer <= detectionRange)
        {
            MoveTowardsPlayer();
        }

        // Check if the player is in attack range
        if (distanceToPlayer <= attackRange)
        {
            Attack();
        }
    }

    void MoveTowardsPlayer()
    {
        // Calculate the direction to the player
        Vector3 direction = (player.position - transform.position).normalized;
        // Move the enemy towards the player
        rb.MovePosition(transform.position + direction * moveSpeed * Time.deltaTime);
        // Face the enemy towards the player
        transform.LookAt(new Vector3(player.position.x, transform.position.y, player.position.z));
    }

    void Attack()
    {
        // Check for attack cooldown
        if (Time.time > lastAttackTime + attackCooldown)
        {
            // Call a method to deal damage to the player (you need to implement this in your player script)
            player.GetComponent<PlayerController>().TakeDamage(attackDamage);
            lastAttackTime = Time.time;
        }
    }

    public void TakeDamage(int damage)
    {
        if (isDead) return;

        health -= damage;

        if (health <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        isDead = true;
        // Add logic for enemy death, e.g., play death animation, destroy enemy object
        Destroy(gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        // Check if the player landed on top of the enemy
        if (collision.gameObject.CompareTag("Player") && collision.relativeVelocity.y < 0)
        {
            Vector3 contactPoint = collision.contacts[0].point;
            Vector3 enemyPosition = transform.position;
            if (contactPoint.y > enemyPosition.y + 0.5f) // Ensure collision is from above
            {
                player.GetComponent<PlayerController>().Bounce(); // Call a method to bounce the player up after hitting the enemy
                Die();
            }
        }
    }
}
