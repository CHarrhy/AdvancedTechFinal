using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject enemyPrefab;
    public Transform[] respawnPoints;
    public int maxEnemies = 5; // Maximum number of enemies allowed

    private int currentEnemyCount = 0;

    void Start()
    {
        // Spawn the initial enemies
        for (int i = 0; i < maxEnemies; i++)
        {
            SpawnEnemy();
        }
    }

    public void RespawnEnemy()
    {
        // Deactivate the current enemy
        currentEnemyCount--;

        // Instantiate a new enemy prefab at a random respawn point if the limit allows
        if (currentEnemyCount < maxEnemies)
        {
            Transform randomRespawnPoint = respawnPoints[Random.Range(0, respawnPoints.Length)];
            GameObject newEnemy = Instantiate(enemyPrefab, randomRespawnPoint.position, randomRespawnPoint.rotation);
            currentEnemyCount++;
        }
    }

    void SpawnEnemy()
    {
        // Instantiate the initial enemies at random respawn points
        for (int i = 0; i < maxEnemies; i++)
        {
            Transform randomRespawnPoint = respawnPoints[Random.Range(0, respawnPoints.Length)];
            GameObject newEnemy = Instantiate(enemyPrefab, randomRespawnPoint.position, randomRespawnPoint.rotation);
            currentEnemyCount++;
        }
    }
}
