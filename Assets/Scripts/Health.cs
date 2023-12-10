using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public int maxHealth = 100;
    public int currentHealth;

    public UnityEvent OnDeath;  // Event to be invoked when the object dies

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
        // Invoke the OnDeath event
        OnDeath.Invoke();
    }

    public void AddHealth(int amount)
    {
        currentHealth += amount;
        // You can add additional logic here, such as clamping health to a maximum value
    }
}
