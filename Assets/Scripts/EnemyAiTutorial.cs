using UnityEngine;
using UnityEngine.AI;

public class EnemyAiTutorial : MonoBehaviour
{
    public NavMeshAgent agent;
    public Transform player;
    private Animator animator;
    public LayerMask Ground, Player;

    // Patroling
    public Vector3 walkPoint;
    private bool walkPointSet = false;
    public float walkPointRange;

    // Attacking
    public float timeBetweenAttacks = 0.5f;
    // States
    public float sightRange, attackRange;
    public bool playerInSightRange, playerInAttackRange;

    public PlayerState playerstate;
    private BoxCollider boxCollider;
    private Rigidbody rigidBody;

    private void Awake()
    {
        player = GameObject.Find("Player").transform;
        agent = GetComponent<NavMeshAgent>();
        playerstate = GameObject.Find("Player").GetComponent<PlayerState>(); // Get PlayerState
        animator = GetComponent<Animator>(); // Initialize animator
        boxCollider = GetComponentInChildren<BoxCollider>(); // Initialize boxCollider
        rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        // Check for sight and attack range
        playerInSightRange = Physics.CheckSphere(transform.position, sightRange, Player);
        playerInAttackRange = Physics.CheckSphere(transform.position, attackRange, Player);

        if (playerstate != null && playerstate.isAlive)
        {
            if (!playerInSightRange && !playerInAttackRange)
                Patroling();
            if (playerInSightRange && !playerInAttackRange)
                ChasePlayer();
            if (playerInAttackRange && playerInSightRange)
                FightPlayer();
        }
        else if (playerstate != null && !playerstate.isAlive)
        {
            // Player is dead, return to patrol
            playerInSightRange = false;
            playerInAttackRange = false;
            Patroling();
        }
    }

    private void Patroling()
    {
        if (!walkPointSet) 
            SearchWalkPoint();

        if (walkPointSet)
        {
            agent.SetDestination(walkPoint);
            Vector3 distanceToWalkPoint = transform.position - walkPoint;

            // Walkpoint reached
            if (distanceToWalkPoint.sqrMagnitude < 1f)
                walkPointSet = false;
        }
    }

    private void SearchWalkPoint()
    {
        float randomZ = Random.Range(-walkPointRange, walkPointRange);
        float randomX = Random.Range(-walkPointRange, walkPointRange);

        walkPoint = new Vector3(transform.position.x + randomX, transform.position.y, transform.position.z + randomZ);

        if (Physics.Raycast(walkPoint, -transform.up, 50f, Ground))
        {
            NavMeshHit hit;
            if (NavMesh.SamplePosition(walkPoint, out hit, 1.0f, NavMesh.AllAreas))
            {
                walkPoint = hit.position;
                walkPointSet = true;
            }
        }
    }

    private void ChasePlayer()
    {
        // Set player as destination if the agent is not already chasing
        if (agent.destination != player.position)
        {
            agent.SetDestination(player.position);
        }
    }

    private void FightPlayer()
    {
        agent.SetDestination(transform.position); // Stop moving when attacking
        // Trigger attack animation if not already playing
        if (!animator.GetCurrentAnimatorStateInfo(0).IsName("1H_Melee_Attack_Slice_Horizontal"))
        {
            rigidBody.constraints = RigidbodyConstraints.FreezeAll;
            animator.SetTrigger("Attack");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        // Check if the enemy hit the player
        PlayerState playerState = other.GetComponent<PlayerState>();
        if (playerState != null)
        {
            Debug.Log("Enemy Attacking Player!");
            playerState.TakeDamage(1); // Inflict damage to the player
        }
    }

    private void DestroyEnemy()
    {
        Destroy(gameObject); // Destroys the enemy object
    }

    private void OnDrawGizmosSelected()
    {
        // Draw attack and sight range for debugging
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, attackRange);
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, sightRange);
    }

    // These methods should be called through animation events
    void EnableAttack()
    {
        boxCollider.enabled = true; // Enable hitbox during attack
    }

    void DisableAttack()
    {
        boxCollider.enabled = false; // Disable hitbox after attack
    }
    void Unfreeze()
    {
        rigidBody.constraints = RigidbodyConstraints.None;
    }
}
