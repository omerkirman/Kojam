using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab; // The prefab of the enemy you want to spawn
    public float minSpawnInterval = 1f; // Minimum time interval between spawns
    public float maxSpawnInterval = 3f; // Maximum time interval between spawns
    public Transform[] spawnPoints; // Array of spawn points

    private float timer = 0f;
    private float spawnInterval;
    

    private Transform player; // Reference to the player's transform

    void Start()
    {
        // Set initial spawn interval
        spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
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
        // Increment timer
        timer += Time.deltaTime;

        // Check if it's time to spawn
        if (timer >= spawnInterval)
        {
            // Reset timer
            timer = 0f;

            // Spawn enemy
            SpawnEnemy();

            // Randomize next spawn interval
            spawnInterval = Random.Range(minSpawnInterval, maxSpawnInterval);
        }
    }
    
    void SpawnEnemy()
    {
        // Choose a random spawn point
        int randomIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[randomIndex];

        // Instantiate enemy at the chosen spawn point
        GameObject enemyInstance = Instantiate(enemyPrefab, spawnPoint.position, Quaternion.identity);

        // Access the EnemyHealth script attached to the enemy and set max health
        EnemyHealth enemyHealth = enemyInstance.GetComponent<EnemyHealth>();
        if (enemyHealth != null)
        {
            enemyHealth.maxHealth = 100; // Set max health of the enemy
        }
        else
        {
            Debug.LogError("EnemyHealth script not found on enemy prefab!");
        }
    }
}
