using UnityEngine;

public class Climbing : MonoBehaviour
{
    public float climbSpeed = 5f; // Speed of climbing
    public LayerMask climbableLayer; // Layer where climbable objects are placed
    public float distanceToClimb = 1f; // Distance from the player to start climbing

    private bool isClimbing;

    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // Check if the player wants to climb
        if (!isClimbing && Input.GetKeyDown(KeyCode.UpArrow))
        {
            TryClimb();
        }
        else if (isClimbing)
        {
            // Continue climbing
            float climbInput = Input.GetAxis("Vertical");
            Climb(climbInput);
        }
    }

    void TryClimb()
    {
        // Raycast upwards to check if there's something climbable
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.up, distanceToClimb, climbableLayer);

        if (hit.collider != null)
        {
            isClimbing = true;
            rb.gravityScale = 0f; // Disable gravity while climbing
        }
    }

    void Climb(float input)
    {
        // Move the player up or down
        transform.Translate(Vector3.up * input * climbSpeed * Time.deltaTime);

        // Check if the player wants to stop climbing
        if (Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.Space))
        {
            StopClimbing();
        }
    }

    void StopClimbing()
    {
        isClimbing = false;
        rb.gravityScale = 1f; // Re-enable gravity
    }
}
