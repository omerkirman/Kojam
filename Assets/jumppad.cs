using UnityEngine;

public class JumpPad : MonoBehaviour
{
    [SerializeField] private float bounceForce = 20f;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player") || collision.gameObject.CompareTag("Enemy"))
        {
            Rigidbody2D rb = collision.gameObject.GetComponent<Rigidbody2D>();
            if (rb != null)
            {
                // Apply upward force to the Rigidbody2D
                rb.AddForce(Vector2.up * bounceForce, ForceMode2D.Impulse);
            }
        }
    }
}
