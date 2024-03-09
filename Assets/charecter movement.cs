using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float maxMoveSpeed = 10f;
    public float acceleration = 5f;
    public float jumpForce = 10f;
    public float gravityScale = 2f;
    public LayerMask groundLayer;

    private Rigidbody2D rb;
    private Collider2D col;
    private bool isGrounded;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        rb.gravityScale = gravityScale; // Apply custom gravity scale
    }

    void Update()
    {
        // Check if the character is grounded
        isGrounded = col.IsTouchingLayers(groundLayer);

        // Get horizontal input
        float moveInput = Input.GetAxis("Horizontal");

        // Calculate horizontal movement
        float targetVelocityX = moveInput * moveSpeed;
        float newVelocityX = Mathf.MoveTowards(rb.velocity.x, targetVelocityX, acceleration * Time.deltaTime);
        rb.velocity = new Vector2(newVelocityX, rb.velocity.y);

        // Limit max horizontal velocity
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxMoveSpeed, maxMoveSpeed), rb.velocity.y);

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }
}
