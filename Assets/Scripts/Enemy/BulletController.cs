using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 10f;
    public float lifetime = 2f;

    private void Start()
    {
        // Destroy the bullet after a specified lifetime
        Destroy(gameObject, lifetime);
    }

    void Update()
    {
        // Move the bullet forward
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    public void SetDirection(Vector3 direction)
    {
        // Rotate the bullet to face the specified direction
        transform.rotation = Quaternion.LookRotation(direction);
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the bullet hits the player or any other target
        if (other.CompareTag("Player"))
        {
            // Deal damage to the player or perform any other action
            // You may want to implement a health system or use the existing one
            Health playerHealth = other.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(10);
            }

            // Destroy the bullet upon hitting a target
            Destroy(gameObject);
        }
    }
}
