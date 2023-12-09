using UnityEngine;
using UnityEngine.AI;
using System.Collections;

public class EnemyAI : MonoBehaviour
{
    public Transform player;
    public float detectionRadius = 10f;
    public float searchDuration = 10f; // How long the enemy searches before giving up
    public float respawnDelay = 3f;    // Time delay before respawning

    private NavMeshAgent navMeshAgent;
    private bool isSearching = false;
    private float searchTimer = 0f;

    private GameManager gameManager;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        gameManager = FindObjectOfType<GameManager>();

        // Start searching immediately
        SearchForPlayer();
    }

    void Update()
    {
        // If not searching, move to a random search location
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

    internal void SearchForPlayer()
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
            // Die and respawn after a delay
            DieAndRespawn();
        }
    }

    void DieAndRespawn()
    {
        // Handle death logic, e.g., play death animation, disable components, etc.

        // Start the respawn coroutine
        StartCoroutine(RespawnAfterDelay());
    }

    IEnumerator RespawnAfterDelay()
    {
        // Wait for the specified delay before respawning
        yield return new WaitForSeconds(respawnDelay);

        // Respawn the enemy using the GameManager
        gameManager.RespawnEnemy();
    }
}
