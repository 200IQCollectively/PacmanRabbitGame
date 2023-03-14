using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject enemyPrefab; // Reference to the enemy prefab to be spawned
    public Transform[] spawnPoints; // Reference to the point where the enemy will be spawned
    public float spawnInterval = 30f;
    public int maxEnemies= 5;
    private float timeSinceLastSpawn;


    private void Start()
    {
        timeSinceLastSpawn = spawnInterval;

    }

    private void Update()
    {
        timeSinceLastSpawn += Time.deltaTime;

        if (timeSinceLastSpawn <= spawnInterval)
        {
            Spawn();
            timeSinceLastSpawn = 0f;

        }
    }


    void OnDestroy()
    {
        Spawn(); // Call the Spawn function when the script is started
    }

    void Spawn()
    {
        int spawnIndex = Random.Range(0, spawnPoints.Length);
        Transform spawnPoint = spawnPoints[spawnIndex];
        if (timeSinceLastSpawn >= spawnInterval && GameObject.FindGameObjectsWithTag("Enemy").Length < maxEnemies)
        {
            GameObject enemy = Instantiate(enemyPrefab, spawnPoint.position, spawnPoint.rotation); // Spawn the enemy object
            AiLocomotion enemyController = enemy.GetComponent<AiLocomotion>(); // Get a reference to the enemy's controller script
        }
    }
}
