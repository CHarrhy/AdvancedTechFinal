using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    public static EnemyManager Instance; // Singleton instance

    public int TotalEnemies { get; private set; }
    public int RemainingEnemies { get; private set; }

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
    }

    private void Start()
    {
        Debug.Log(TotalEnemies);
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
        Debug.Log("YOU WIN!!");
    }
}
