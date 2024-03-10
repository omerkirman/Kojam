using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    public int maxHealth = 100; // Maximum health of the enemy
    private int currentHealth; // Current health of the enemy

    void Start()
    {
        currentHealth = maxHealth; // Initialize current health to max health
    }

    public void TakeDamage(int damageAmount)
    {
        currentHealth -= damageAmount; // Decrease current health by damage amount

        // Check if the enemy is dead
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Destroy the enemy when it dies
        Destroy(gameObject);
    }
}
