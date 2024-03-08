using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;            // Speed of the character movement
    public float jumpForce = 10f;           // Force of the character jump
    public float groundCheckDistance = 0.2f; // Distance to check for grounding
    public LayerMask groundLayer;           // Layer mask for the ground objects

    private Rigidbody2D rb;
    private bool isGrounded;
    private Vector2 moveDirection;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        // Read input from the player
        float horizontalInput = Input.GetAxisRaw("Horizontal");
        float verticalInput = Input.GetAxisRaw("Vertical");
        bool jump = Input.GetKeyDown(KeyCode.Space);

        // Set movement direction based on input
        moveDirection = new Vector2(horizontalInput, verticalInput).normalized;

        // Check if the character is grounded
        isGrounded = CheckGrounded();

        // Apply movement and jump
        MoveCharacter(moveDirection.x);

        if (jump && isGrounded)
        {
            Jump();
        }
    }

    private bool CheckGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundCheckDistance, groundLayer);
        return hit.collider != null;
    }

    private void MoveCharacter(float horizontalInput)
    {
        // Move the character based on the input
        Vector2 movement = new Vector2(horizontalInput * moveSpeed * Time.deltaTime, rb.velocity.y);
        rb.velocity = movement;

        // Rotate the character to face the direction of movement (optional)
        if (moveDirection != Vector2.zero)
        {
            float angle = Mathf.Atan2(moveDirection.y, moveDirection.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }

    private void Jump()
    {
        // Apply jump force
        rb.velocity = new Vector2(rb.velocity.x, jumpForce);
    }
}
