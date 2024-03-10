using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 3f; // Speed of enemy movement
    private Transform player; // Reference to the player's transform
    public float enemyHealth = 5f;
    private bool isAlive = true; // Flag to check if the enemy is alive

    void Start()
    {
        // Find the player GameObject in the scene
        player = GameObject.FindGameObjectWithTag("Player")?.transform;

        // Check if player is found
        if (player == null)
        {
            Debug.LogError("Player not found in the scene. Make sure to tag your player GameObject with 'Player'.");
        }
    }

    void Update()
    {
        if (isAlive && player != null)
        {
            // Calculate the direction from the enemy to the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Move the enemy towards the player
            transform.Translate(direction * speed * Time.deltaTime);
        }
    }

    public void TakeDamage(float damage)
    {
        // Check if the enemy is alive before applying damage
        if (isAlive)
        {
            enemyHealth -= damage;
            if (enemyHealth <= 0)
            {
                Die();
            }
        }
    }

    public void Die()
    {
        // Set isAlive flag to false
        isAlive = false;

        // Check if the ScoreScript is available in the scene
        ScoreScript scoreScript = FindObjectOfType<ScoreScript>();
        if (scoreScript != null)
        {
            // Add score when enemy dies
            scoreScript.Addpoint();
        }
        else
        {
            Debug.LogWarning("ScoreScript not found in the scene. Make sure to have a ScoreScript GameObject.");
        }

        // Destroy the enemy GameObject
        Destroy(gameObject);
    }
}
