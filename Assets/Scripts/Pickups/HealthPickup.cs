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
            healthComponent.AddHealth(healthToAdd);

            // Destroy the pickup object after it's been collected
            Destroy(gameObject);
        }
    }
}
