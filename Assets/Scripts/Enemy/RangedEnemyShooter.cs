using UnityEngine;

public class RangedEnemyShooter : MonoBehaviour
{
    public Transform player;
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float shootCooldown = 2f;
    public float detectionRadius = 10f; // Add this line

    private float shootTimer = 0f;

    void Update()
    {
        if (shootTimer > 0)
        {
            shootTimer -= Time.deltaTime;
        }

        // Check if it's time to shoot
        if (shootTimer <= 0 && Vector3.Distance(transform.position, player.position) < detectionRadius)
        {
            // Shoot towards the player
            Shoot();

            // Reset the shoot timer
            shootTimer = shootCooldown;
        }
    }

    void Shoot()
    {
        // Instantiate a bullet at the fire point
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);

        // Set the bullet's direction towards the player
        BulletController bulletController = bullet.GetComponent<BulletController>();
        if (bulletController != null)
        {
            bulletController.SetDirection((player.position - firePoint.position).normalized);
        }
    }
}
