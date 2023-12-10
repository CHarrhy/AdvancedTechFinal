using UnityEngine;

public class AmmoPickup : MonoBehaviour
{
    public int minAmmoToAdd = 10;
    public int maxAmmoToAdd = 20;
    public float spinSpeed = 5f;
    public Vector3 spinDirection = new Vector3(0, 0, 1);
    public int rotationSpeed = 1;
    public AudioClip pickupSound; // Add this variable for the pickup sound

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Trigger entered!");

            // Generate a random ammo value within the specified range
            int ammoToAdd = Random.Range(minAmmoToAdd, maxAmmoToAdd + 1);

            // Set the ammo value
            FPSController controller = other.GetComponent<FPSController>();
            if (controller != null)
            {
                controller.AddAmmo(ammoToAdd);
                AudioSource.PlayClipAtPoint(pickupSound, transform.position); // Play pickup sound

                // Destroy the pickup object after it's picked up
                Destroy(gameObject);
            }
            else
            {
                Debug.LogWarning("FPSController not found on the player.");
            }

            Debug.Log("Trigger entered!");

        }
    }

    void Update()
    {
        // Rotate the parent GameObject around the z-axis
        transform.Rotate(spinDirection * Time.deltaTime * rotationSpeed);
    }
}
