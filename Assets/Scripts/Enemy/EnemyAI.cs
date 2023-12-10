using UnityEngine;
using UnityEngine.AI;

public enum EnemyType
{
    Melee,
    Ranged
}

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 10f;
    public float chaseRadius = 15f;
    public float attackDistance = 2f; // Adjust this based on your preference
    public float attackCooldown = 2f;
    public float patrolSpeed = 3f; // Speed during patrolling
    public float chaseSpeed = 5f; // Speed during chasing
    public Transform[] patrolPoints;
    public EnemyType enemyType;

    private NavMeshAgent navMeshAgent;
    private int currentPatrolIndex = 0;
    private bool isChasing = false;
    private float attackTimer = 0f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Start patrolling immediately
        Patrol();
    }

    void Update()
    {
        // Check if the player is within detection radius
        if (Vector3.Distance(transform.position, player.position) < detectionRadius)
        {
            isChasing = true;

            // Set the destination to the player's position
            navMeshAgent.SetDestination(player.position);

            // Attack logic based on enemy type
            if (enemyType == EnemyType.Melee)
            {
                MeleeAttack();
            }
            else if (enemyType == EnemyType.Ranged)
            {
                // Add ranged attack logic here
            }
        }
        else if (isChasing && Vector3.Distance(transform.position, player.position) > chaseRadius)
        {
            // Stop chasing if player is out of chase radius
            isChasing = false;

            // Resume patrolling
            Patrol();
        }

        // Update attack cooldown
        if (attackTimer > 0)
        {
            attackTimer -= Time.deltaTime;
        }
    }

    void Patrol()
    {
        // Check if there are patrol points
        if (patrolPoints.Length > 0)
        {
            // Set the destination to the current patrol point
            navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].position);

            // Set patrolling speed
            navMeshAgent.speed = patrolSpeed;

            // Increment patrol index for the next point
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    void MeleeAttack()
    {
        // Melee attack logic
        if (Vector3.Distance(transform.position, player.position) < attackDistance)
        {
            // Check attack cooldown
            if (attackTimer <= 0)
            {
                // Calculate the direction to the player
                Vector3 directionToPlayer = (player.position - transform.position).normalized;

                // Set the destination a bit away from the player in the calculated direction
                navMeshAgent.SetDestination(player.position - directionToPlayer * 1.5f);

                // Perform melee attack (animation, damage, etc.)
                // You can implement your damage logic here

                // Apply damage to the player
                Health playerHealth = player.GetComponent<Health>();
                if (playerHealth != null)
                {
                    playerHealth.TakeDamage(10); // Adjust the damage value as needed
                }

                // Reset attack cooldown
                attackTimer = attackCooldown;
            }
        }
        else
        {
            // Set chasing speed if not in attack range
            navMeshAgent.speed = chaseSpeed;
        }
    }

}