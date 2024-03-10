using UnityEngine;

public class Attack : MonoBehaviour
{
    public float attackRange = 2f; // Range within which the enemy can attack
    public float attackDamage = 10f; // Damage inflicted by the enemy's attack
    public float attackCooldown = 1f; // Cooldown between attacks
    public LayerMask playerLayer; // LayerMask for detecting the player
    public int scoreValue = 100; // Score added when enemy successfully attacks and kills something

    private float lastAttackTime; // Time of the last attack


    void Update()
    {
        // Check if it's time to attack
        if (Time.time >= lastAttackTime + attackCooldown && Input.GetMouseButtonDown(0))
        {
            // Detect the player within attack range
            Collider2D[] hitPlayers = Physics2D.OverlapCircleAll(transform.position, attackRange, playerLayer);

            // If there are players in range, attack the first one
            if (hitPlayers.Length > 0)
            {
                // For simplicity, let's assume we damage the player by sending a message
                hitPlayers[0].SendMessage("TakeDamage", attackDamage);

                // Check if the player has been killed
                if (hitPlayers[0].GetComponent<PlayerHealth>() != null && hitPlayers[0].GetComponent<PlayerHealth>().IsDead())
                {
                    // Add score if the player has been killed
                    UpdateScore(scoreValue);
                }

                // Update the last attack time
                lastAttackTime = Time.time;
            }
        }
        // Check if the ScoreScript is available in the scene
        ScoreScript scoreScript = FindObjectOfType<ScoreScript>();
        //if (Input.GetMouseButtonDown(0))
        //{
        //    scoreScript.Addpoint();
        //}
    }


    void UpdateScore(int value)
    {
        // Here you can implement your score updating logic, e.g., adding to a score variable, displaying on UI, etc.
        // For simplicity, let's just print the score to the console.
        ScoreScript scoreScript = FindObjectOfType<ScoreScript>();
      
        
        Debug.Log("Score: " + value);
    }

    // Visualize attack range in the Unity Editor
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
    }
}
