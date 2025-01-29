using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyFollow : MonoBehaviour
{
    public GameObject myTarget;         // The target the enemy will follow
    public GameObject currentTarget;    // The current active target (null if not following)
    public NavMeshAgent myAgent;        // The NavMeshAgent component for movement
    public int range;                   // Maximum distance to start following the target
    public int tetherRange;             // Distance beyond which the enemy stops following and returns to start position
    public Vector3 startPos;            // The starting position of the enemy

    // Start is called before the first frame update
    void Start()
    {
        AudioManager.instance.PlayEventSound("Bones");
        // Ensure the NavMeshAgent is assigned
        if (myAgent == null)
        {
            myAgent = GetComponent<NavMeshAgent>();
        }
        if (myAgent == null)
        {
            Debug.LogError("NavMeshAgent is not assigned and could not be found on the GameObject.");
            enabled = false; // Disable the script if myAgent is not assigned.
            return;
        }

        // Ensure the target is assigned
        if (myTarget == null)
        {
            Debug.LogError("myTarget is not assigned.");
            enabled = false; // Disable the script if myTarget is not assigned.
            return;
        }

        // Record the starting position of the enemy
        startPos = this.transform.position;

        // Invoke CheckDist method every 0.5 seconds
        InvokeRepeating("CheckDist", 0, .5f);
    }

    // Method to check the distance between the enemy and the target
    public void CheckDist()
    {
        float dist = Vector3.Distance(this.transform.position, myTarget.transform.position);

        if (dist < range)
        {
            currentTarget = myTarget;
        }
        else if (dist > tetherRange)
        {
            currentTarget = null;
        }
        else if (dist > range && dist <= tetherRange && currentTarget == null)
        {
            // Allow enemy to keep following the target if it's within tether range
            currentTarget = myTarget;
        }
    }

    // Update is called once per frame
    void Update()
    {
        // Ensure the NavMeshAgent is on a NavMesh before setting the destination
        if (!myAgent.isOnNavMesh)
        {
            Debug.LogWarning("NavMeshAgent is not on a NavMesh. Ensure the agent is placed on a valid NavMesh.");
            return;
        }

        if (currentTarget != null)
        {
            myAgent.SetDestination(currentTarget.transform.position);
        }
        else if (Vector3.Distance(myAgent.destination, startPos) > 0.1f)
        {
            myAgent.SetDestination(startPos);
        }
    }
}
