using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check for win condition when the player enters the door trigger
            CheckWinCondition();
        }
    }

    void CheckWinCondition()
    {
        // Ensure that EnemyManager.Instance is not null
        if (EnemyManager.Instance != null)
        {
            // Check if all enemies are defeated
            if (EnemyManager.Instance.RemainingEnemies == 0)
            {
                // All enemies defeated, player wins
                WinGame();
            }
        }
        else
        {
            Debug.LogError("EnemyManager.Instance is null. Make sure EnemyManager is properly set.");
        }
    }

    void WinGame()
    {
        // Implement win game logic here
        Debug.Log("Congratulations! You have won the game!");

        // Load the victory scene or perform other win-related actions
        SceneManager.LoadScene("VictoryScene"); // Replace "VictoryScene" with your actual victory scene name
    }
}
