
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(AgentLinkMover))]
public class AiLocomotion : MonoBehaviour
{
   private NavMeshAgent navAgent;
    Transform player;

    public float UpdateSpeed = 0.1f; // how often to calculate path based on targets position.
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float health;
    private float distanceThreshold = 20.0f;
    public float minRandomDistance = 5f; // Minimum distance the AI must travel before selecting a new random position
    public float maxRandomDistance = 10f;// Minimum distance the AI must travel before selecting a new random position

    private Vector3 targetPosition;




    private void Awake()
    {
        navAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
            
        

    }

 


    private void Start()
    { 
       
        targetPosition = GetRandomNavMeshPosition();
        player = GameObject.FindGameObjectWithTag("Player").transform;

    }


    private void Update()
    {
        if (navAgent.remainingDistance <= navAgent.stoppingDistance)
        {
            targetPosition = GetRandomNavMeshPosition();
        }
       

    }
   

    private Vector3 GetRandomNavMeshPosition()
    {
        // Generate a random point within the max distance from the AI
        Vector3 randomOffset = Random.insideUnitSphere * maxRandomDistance;
        randomOffset.y = 0f;
        Vector3 randomPosition = transform.position + randomOffset;

        // Find the closest point on the NavMesh to the random position
        NavMeshHit hit;
        if (NavMesh.SamplePosition(randomPosition, out hit, maxRandomDistance, NavMesh.AllAreas))
        {
            return hit.position;
        }

        // If we couldn't find a valid position, just return the AI's current position
        return transform.position;
    }


    void FixedUpdate()
    {
        // Move the AI towards the target position
        navAgent.SetDestination(targetPosition);
    }




    public void OnDrawGizmos()
    {
        if (GameObject.Find("TestPlayer(Clone)")!=null)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position, targetPosition);

        }
    }

   
}
