using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    private EnemyManager enemyManager;
    public GameObject enemyManagerObject;

    private void Start()
    {
        enemyManager = enemyManagerObject.GetComponent<EnemyManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            if (enemyManager.AllDead)
            {
                WinGame();
            }
            else
            {
                Debug.Log("Kill all enemies first");
            }
        }
    }

    void WinGame()
    {
        // Implement win game logic here
        Debug.Log("Congratulations! You have won the game!");

        // Load the victory scene or perform other win-related actions
        SceneManager.LoadScene("Victory Scene"); // Replace "VictoryScene" with your actual victory scene name
    }
}
