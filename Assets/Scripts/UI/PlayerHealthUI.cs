using UnityEngine;
using TMPro;

public class PlayerHealthUI : MonoBehaviour
{
    public TextMeshProUGUI playerHealthText;
    public Health playerHealth;

    void Start()
    {
        // Assign player health script
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            playerHealth = playerObject.GetComponent<Health>();
        }
        else
        {
            Debug.LogError("Player object not found!");
        }
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
