using UnityEngine;
using UnityEngine.AI;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 10f;
    public float searchDuration = 10f; // How long the enemy searches before giving up
    public float respawnDelay = 3f; // Time delay before respawning
    public Transform[] spawnPoints; // Array of spawn points

    private NavMeshAgent navMeshAgent;
    private bool isSearching = false;
    private float searchTimer = 0f;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

        // Start searching immediately
        SearchForPlayer();
    }

    void Update()
    {
        // If not searching, move to a random location
        if (!isSearching)
        {
            MoveToRandomLocation();
        }
        else
        {
            // If searching, decrement the timer
            searchTimer -= Time.deltaTime;

            if (searchTimer <= 0f)
            {
                // Stop searching after the timer expires
                isSearching = false;
            }
        }
    }

    void SearchForPlayer()
    {
        // Check if the player is within the detection radius
        if (Vector3.Distance(transform.position, player.position) < detectionRadius)
        {
            // Set the destination to the player's position
            navMeshAgent.SetDestination(player.position);
            isSearching = true;
            searchTimer = searchDuration;
        }
    }

    void MoveToRandomLocation()
    {
        // Check if the enemy has reached the destination
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance < 0.5f)
        {
            // Pick a random spawn point
            Transform randomSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];

            // Move to the random spawn point
            navMeshAgent.SetDestination(randomSpawnPoint.position);

            // Schedule respawn after a delay
            Invoke("Respawn", respawnDelay);
        }
    }

    void Respawn()
    {
        // Reset the position to the initial spawn point
        // You may want to modify this based on your specific setup
        Transform initialSpawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
        transform.position = initialSpawnPoint.position;

        // Start searching again
        SearchForPlayer();
    }
}

