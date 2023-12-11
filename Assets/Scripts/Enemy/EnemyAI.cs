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
    public float chaseSpeed = 4f; // Speed during chasing
    public Transform[] patrolPoints;
    public EnemyType enemyType;
    public Animator animator;
    public Transform playerTransform;

    private NavMeshAgent navMeshAgent;
    private int currentPatrolIndex = 0;
    private bool isChasing = false;
    private float attackTimer = 0f;
    private bool isDead = false;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();

        // Start patrolling immediately
        Patrol();

        // Subscribe to the OnDeath event in the Health script
        Health healthComponent = GetComponent<Health>();
        if (healthComponent != null)
        {
            healthComponent.OnDeath.AddListener(Die);
        }
    }

    void Update()
    {
        if (isDead)
        {
            // Skip normal behavior if dead
            return;
        }

        if (playerTransform != null)
        {
            if(!isChasing)
            {
                Patrol();
            }

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
            // Calculate the distance to the current patrol point
            float distanceToPatrolPoint = Vector3.Distance(transform.position, patrolPoints[currentPatrolIndex].position);

            // If close to the current patrol point, move to the next one
            if (distanceToPatrolPoint < 1f)
            {
                // Increment patrol index for the next point
                currentPatrolIndex = (currentPatrolIndex + 1) % patrolPoints.Length;
            }

            // Set the destination to the current patrol point
            navMeshAgent.SetDestination(patrolPoints[currentPatrolIndex].position);

            // Set patrolling speed
            navMeshAgent.speed = patrolSpeed;
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

                // Stop the NavMeshAgent
                navMeshAgent.isStopped = true;

                // Set the destination a bit away from the player in the calculated direction
                navMeshAgent.SetDestination(player.position - directionToPlayer * 1.5f);

                // Perform melee attack (animation, damage, etc.)
                animator.SetTrigger("MeleeAttack");

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

            // Resume NavMeshAgent
            navMeshAgent.isStopped = false;
        }
    }

    public void Die()
    {
        isDead = true;

        navMeshAgent.isStopped = true;

        // Trigger death animation
        animator.SetTrigger("Die");

        Destroy(gameObject, 10f);

        EnemyManager.Instance.EnemyDefeated();
    }
}
