using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 3f; // Speed of enemy movement
    private Transform player; // Reference to the player's transform

    void Start()
    {
        // Find the player GameObject in the scene
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Check if player is found
        if (player == null)
        {
            Debug.LogError("Player not found in the scene. Make sure to tag your player GameObject with 'Player'.");
        }
    }

    void Update()
    {
        if (player != null)
        {
            // Calculate the direction from the enemy to the player
            Vector3 direction = (player.position - transform.position).normalized;

            // Lock rotation
            transform.rotation = Quaternion.LookRotation(Vector3.forward, direction);

            // Move the enemy towards the player
            transform.Translate(Vector3.up * speed * Time.deltaTime);
        }
    }
}
