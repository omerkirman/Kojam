using UnityEngine;

public class CharacterMovement : MonoBehaviour
{
    public float moveSpeed = 5f;
    public float maxMoveSpeed = 10f;
    public float acceleration = 5f;
    public float jumpForce = 10f;
    public float gravityScale = 2f;
    public LayerMask groundLayer;
    Animator playerAnimator;

    private Rigidbody2D rb;
    private Collider2D col;
    private bool isGrounded;
    private SpriteRenderer spriteRenderer;
    private bool canFlip = true; // Flag to control if the player can flip

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        rb.gravityScale = gravityScale; // Apply custom gravity scale
        spriteRenderer = GetComponent<SpriteRenderer>();

        // Lock rotation
        rb.constraints = RigidbodyConstraints2D.FreezeRotation;

        playerAnimator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the character is grounded
        isGrounded = col.IsTouchingLayers(groundLayer);

        // Get horizontal input
        float moveInput = Input.GetAxis("Horizontal");
        playerAnimator.SetFloat("speed", Mathf.Abs(moveInput));


        // Calculate horizontal movement
        float targetVelocityX = moveInput * moveSpeed;
        float newVelocityX = Mathf.MoveTowards(rb.velocity.x, targetVelocityX, acceleration * Time.deltaTime);
        rb.velocity = new Vector2(newVelocityX, rb.velocity.y);

        // Limit max horizontal velocity
        rb.velocity = new Vector2(Mathf.Clamp(rb.velocity.x, -maxMoveSpeed, maxMoveSpeed), rb.velocity.y);

        // Flip sprite if moving in the opposite direction and canFlip is true
        if (canFlip && ((moveInput < 0 && !spriteRenderer.flipX) || (moveInput > 0 && spriteRenderer.flipX)))
        {
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }

        // Jump
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
        }
    }

    // Method to set canFlip flag
    public void SetCanFlip(bool canFlipValue)
    {
        canFlip = canFlipValue;
    }
}
