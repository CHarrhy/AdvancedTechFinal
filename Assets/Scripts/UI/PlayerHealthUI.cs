using UnityEngine;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    public TextMeshProUGUI playerHealthText;
    public Health playerHealth;

    void Start()
    {
        // Assign player health script
        playerHealth = GameObject.FindGameObjectWithTag("Player").GetComponent<Health>();
    }

    void Update()
    {
        // Update player health UI
        if (playerHealthText != null)
        {
            playerHealthText.text = "Player Health: " + playerHealth.currentHealth.ToString();
        }
    }
}
