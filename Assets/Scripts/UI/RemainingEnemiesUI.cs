using UnityEngine;
using TMPro;

public class RemainingEnemiesUI : MonoBehaviour
{
    public TextMeshProUGUI remainingEnemiesText;

    void Start()
    {
        // Ensure the Text component is assigned
        if (remainingEnemiesText == null)
        {
            Debug.LogError("RemainingEnemiesUI: Text component is not assigned!");
            return;
        }

        // Initialize the text with the starting number of enemies
        UpdateRemainingEnemiesCount(EnemyManager.Instance.RemainingEnemies);
    }

    void Update()
    {
        // Update the text with the remaining enemies count
        UpdateRemainingEnemiesCount(EnemyManager.Instance.RemainingEnemies);
    }

    void UpdateRemainingEnemiesCount(int count)
    {
        // Update the TextMeshPro Text with the remaining enemies count
        remainingEnemiesText.text = "Remaining Enemies: " + count.ToString();
    }
}
