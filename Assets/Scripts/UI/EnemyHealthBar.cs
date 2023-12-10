using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EnemyHealthBar : MonoBehaviour
{
    public Health enemyHealth;
    public Image healthBar;

    void Start()
    {
        // Assign enemy health script
        enemyHealth = GetComponent<Health>();
        if (enemyHealth == null)
        {
            Debug.LogError("EnemyHealth script not found on the enemy GameObject.");
            return;
        }

        // Create a health bar above the enemy's head
        CreateHealthBar();
    }

    void Update()
    {
        // Update the health bar position above the enemy's head
        if (healthBar != null)
        {
            Vector3 screenPos = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 2f);
            healthBar.transform.position = screenPos;
        }

        // Update the health bar fill amount based on the enemy's health
        if (healthBar != null)
        {
            float fillAmount = (float)enemyHealth.currentHealth / enemyHealth.maxHealth;
            healthBar.fillAmount = fillAmount;
        }
    }

    void CreateHealthBar()
    {
        // Instantiate the health bar prefab and parent it to the Canvas
        GameObject healthBarObject = Instantiate(Resources.Load<GameObject>("EnemyHealthBarPrefab"));
        healthBarObject.transform.SetParent(GameObject.Find("EnemyHealthCanvas").transform, false);

        // Assign the health bar reference
        healthBar = healthBarObject.GetComponentInChildren<Image>();
    }
}
