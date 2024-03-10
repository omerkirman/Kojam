using UnityEngine;

public class EnemyAttack : MonoBehaviour
{
    public int damageAmount = 10;

    void OnTriggerEnter(Collider other)
    {
        // Check if the object that was collided with has a PlayerHealth component
        PlayerHealth playerHealth = other.GetComponent<PlayerHealth>();
        if (playerHealth != null)
        {
            // If the object has a PlayerHealth component, deal damage to it
            playerHealth.TakeDamage(damageAmount);
        }
    }
}
