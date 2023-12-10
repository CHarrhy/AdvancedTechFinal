using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthToAdd = 50;  // Amount of health to add

    void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object has a Health component
        Health healthComponent = other.GetComponent<Health>();

        // If the colliding object has a Health component, add health
        if (healthComponent != null)
        {
            // Check if the player's health is less than 100
            if (healthComponent.currentHealth < healthComponent.maxHealth)
            {
                // Calculate the amount to add, capped at 100 health
                int amountToAdd = Mathf.Min(healthToAdd, healthComponent.maxHealth - healthComponent.currentHealth);

                // Add health to the player
                healthComponent.AddHealth(amountToAdd);

                // Destroy the pickup object after it's been collected
                Destroy(gameObject);
            }
        }
    }
}
