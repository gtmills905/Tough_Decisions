using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public GameObject normalModelPrefab;       // Prefab for the normal character model
    public GameObject boostShoesModelPrefab;   // Prefab for the boost shoes model
    public GameObject handBlastersModelPrefab; // Prefab for the hand blasters model
    public GameObject noLegsModelPrefab;       // Prefab for the no legs model

    private GameObject currentModelInstance;   // The currently active model

    private int currentFormIndex = 0;          // Index to keep track of the current form
    private GameObject[] modelPrefabs;         // Array to hold the model prefabs

    public float moveSpeed;
    public float jumpForce;
    private Rigidbody rb;
    private bool isGrounded = false;

    public float bounceForce = 5f; // Force applied when bouncing off an enemy
    public float health = 100f;


    // Unlock flags
    private bool isBoostShoesUnlocked = false;
    private bool isHandBlastersUnlocked = false;
    private bool isNoLegsUnlocked = true;  // Start with no legs form unlocked by default
    private bool isNormalFormUnlocked = false; // Normal form is locked initially

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        // Initialize model prefabs array, now including the no legs model
        modelPrefabs = new GameObject[] { noLegsModelPrefab, boostShoesModelPrefab, handBlastersModelPrefab, normalModelPrefab };

        // Start with the no legs model
        currentModelInstance = Instantiate(noLegsModelPrefab, transform.position, Quaternion.identity, transform);
        SetFormAttributes(); // Initialize attributes for the starting form
    }

    void Update()
    {
        Move();
        Jump();

        if (Input.GetKeyDown(KeyCode.E))
        {
            CycleTransform();
        }
    }

    void Move()
    {
        // In a 3D side-scroller, movement is typically constrained to the X axis
        float moveInput = Input.GetAxis("Horizontal");
        Vector3 moveVelocity = new Vector3(moveInput * moveSpeed, rb.velocity.y, 0);
        rb.velocity = moveVelocity;

        // Optionally, rotate the character to face the direction of movement
        if (moveInput > 0)
        {
            currentModelInstance.transform.localRotation = Quaternion.Euler(0, 180, 0);  // Face right
        }
        else if (moveInput < 0)
        {
            currentModelInstance.transform.localRotation = Quaternion.Euler(0, -180, 0);  // Face left
        }
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            isGrounded = true;
        }
    }

    public void TakeDamage(float damage)
    {
        health -= damage;
        if (health <= 0)
        {
            SceneManager.LoadScene("Demo");
        }
    }

    public void Bounce()
    {
        // Apply bounce force to the player
        Rigidbody rb = GetComponent<Rigidbody>();
        rb.velocity = new Vector3(rb.velocity.x, bounceForce, rb.velocity.z);
    }


    void CycleTransform()
    {
        int originalFormIndex = currentFormIndex;
        do
        {
            // Cycle through the forms
            currentFormIndex = (currentFormIndex + 1) % modelPrefabs.Length;
        } while (!IsFormUnlocked(currentFormIndex) && currentFormIndex != originalFormIndex);

        // If no unlocked form is available, return to the original form
        if (!IsFormUnlocked(currentFormIndex))
        {
            currentFormIndex = originalFormIndex;
            return;
        }

        // Destroy the current model instance
        if (currentModelInstance != null)
        {
            Destroy(currentModelInstance);
        }

        // Instantiate and attach the new model to the player
        currentModelInstance = Instantiate(modelPrefabs[currentFormIndex], transform.position, Quaternion.identity, transform);

        // Set player attributes based on the current form
        SetFormAttributes();

        // Optionally trigger any animations or effects for the new form
        Animator anim = currentModelInstance.GetComponent<Animator>();
        if (anim != null)
        {
            anim.SetTrigger("Transform");
        }
    }

    void SetFormAttributes()
    {
        // Adjust player attributes based on the current form
        switch (currentFormIndex)
        {
            case 0: // No Legs form
                moveSpeed = 4f;
                jumpForce = 4f;
                break;
            case 1: // Boost Shoes form
                moveSpeed = 7.5f; // Increase speed
                jumpForce = 12f;  // Increase jump height
                break;
            case 2: // Hand Blasters form
                moveSpeed = 5f;  // Normal speed
                jumpForce = 10f; // Normal jump height
                // Enable shooting or other special abilities
                break;
            case 3: // Normal form
                moveSpeed = 5f;  // Normal speed
                jumpForce = 10f;  // Normal jump height
                break;
        }
    }

    bool IsFormUnlocked(int formIndex)
    {
        switch (formIndex)
        {
            case 0: // No Legs form
                return isNoLegsUnlocked;
            case 1: // Boost Shoes form
                return isBoostShoesUnlocked;
            case 2: // Hand Blasters form
                return isHandBlastersUnlocked;
            case 3: // Normal form
                return isNormalFormUnlocked;
            default:
                return false;
        }
    }

    // Example methods to unlock transformations via triggers
    public void UnlockBoostShoes()
    {
        isBoostShoesUnlocked = true;
    }

    public void UnlockHandBlasters()
    {
        isHandBlastersUnlocked = true;
    }

    public void UnlockNoLegs()
    {
        isNoLegsUnlocked = true;
    }

    public void UnlockNormalForm()
    {
        isNormalFormUnlocked = true;
    }
}

