using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance; // Singleton instance

    public int TotalEnemies { get; private set; }
    public int RemainingEnemies { get; private set; }

    public bool AllDead;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        // Get the initial count of enemies in the scene
        TotalEnemies = GameObject.FindGameObjectsWithTag("Enemy").Length;
        RemainingEnemies = TotalEnemies;
        AllDead = false;
    }

    public void EnemyDefeated()
    {
        // Called when an enemy is defeated
        RemainingEnemies--;

        // Check for win condition
        if (RemainingEnemies == 0)
        {
            // All enemies defeated, trigger win condition
            WinGame();
        }
    }

    void WinGame()
    {
        // Implement win game logic here
        AllDead = true;
    }
}
