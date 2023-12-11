using TMPro;
using UnityEngine;

public class RemainingEnemiesUI : MonoBehaviour
{
    public TextMeshProUGUI remainingEnemiesText;

    void Update()
    {
        // Update the remaining enemies text
        UpdateRemainingEnemiesText();
    }

    void UpdateRemainingEnemiesText()
    {
        // Get the remaining enemies count from the EnemyManager
        int remainingEnemies = EnemyManager.Instance.RemainingEnemies;

        // Update the UI text
        remainingEnemiesText.text = "Remaining Enemies: " + remainingEnemies;
    }
}
