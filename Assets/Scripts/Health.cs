// Health.cs
using UnityEngine;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    private int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
    }

    public void TakeDamage(int damage)
    {
        currentHealth -= damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    void Die()
    {
        // Handle death logic (e.g., play death animation, disable gameObject)
        Destroy(gameObject);
    }

    public bool IsAlive()
    {
        return currentHealth > 0;
    }
}
