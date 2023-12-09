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
    public float attackCooldown = 2f;
    public float coverDistance = 5f;
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
                RangedAttack();
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

            // Increment patrol index for the next point
            currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
        }
    }

    void MeleeAttack()
    {
        // Melee attack logic
        if (Vector3.Distance(transform.position, player.position) < navMeshAgent.stoppingDistance)
        {
            // Check attack cooldown
            if (attackTimer <= 0)
            {
                // Perform melee attack (animation, damage, etc.)

                // Reset attack cooldown
                attackTimer = attackCooldown;
            }
        }
    }

    void RangedAttack()
    {
        // Ranged attack logic
        // Implement shooting logic here based on your setup
        // For example, you can use a similar shooting logic as the player
        // and instantiate bullets or projectiles towards the player
    }
}
