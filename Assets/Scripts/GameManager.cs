// GameManager.cs
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
        for (int i = 0; i < Mathf.Min(maxEnemies, respawnPoints.Length); i++)
        {
            SpawnEnemy();
        }
    }

    void Update()
    {
    }

    void SpawnEnemy()
    {
        // Instantiate the initial enemies at random respawn points
        if (currentEnemyCount < maxEnemies)
        {
            Transform randomRespawnPoint = respawnPoints[Random.Range(0, respawnPoints.Length)];
            GameObject newEnemy = Instantiate(enemyPrefab, randomRespawnPoint.position, randomRespawnPoint.rotation);

            // Attach the EnemyAI component to the new enemy
            EnemyAI enemyAI = newEnemy.GetComponent<EnemyAI>();

            if (enemyAI != null)
            {
                enemyAI.TakeDamage(0); // Simulate initial damage to trigger respawn logic
            }

            currentEnemyCount++;
        }
    }

    public void RespawnEnemy(EnemyAI enemyToRespawn)
    {
        // Reactivate the specific enemy at a random respawn point
        Transform randomRespawnPoint = respawnPoints[Random.Range(0, respawnPoints.Length)];
        enemyToRespawn.transform.position = randomRespawnPoint.position;
        enemyToRespawn.transform.rotation = randomRespawnPoint.rotation;
        enemyToRespawn.gameObject.SetActive(true);
    }
}
