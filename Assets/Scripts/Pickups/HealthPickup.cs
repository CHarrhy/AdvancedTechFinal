using UnityEngine;

public class HealthPickup : MonoBehaviour
{
    public int healthToAdd = 50;  // Amount of health to add
    public float spinSpeed = 5f;
    public Vector3 spinDirection = new Vector3(0, 0, 1);
    public int rotationSpeed = 1;
    public AudioClip pickupSound; // Add this variable for the pickup sound

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

                AudioSource.PlayClipAtPoint(pickupSound, transform.position); // Play pickup sound

                // Destroy the pickup object after it's been collected
                Destroy(gameObject);
            }
        }
    }

    void Update()
    {
        // Rotate the parent GameObject around the z-axis
        transform.Rotate(spinDirection * Time.deltaTime * rotationSpeed);
    }
}
