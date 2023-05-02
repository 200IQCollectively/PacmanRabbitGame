using UnityEngine;
using UnityEngine.AI;

public class lower : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;
    private Animator animator;
    private bool isMoving;

    private void Start()
    {
        // Get a reference to the NavMeshAgent component
        agent = GetComponent<NavMeshAgent>();
 // agent.obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
        // Get a reference to the Animator component
        animator = GetComponent<Animator>();

        // Get a reference to the player character
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void Update()
    {
        // Calculate the direction vector from the AI character to the player character
        Vector3 direction = (player.position - transform.position).normalized;

        // Calculate the dot product between the forward vector of the AI character and the direction vector to the player
        float dotProduct = Vector3.Dot(transform.forward, direction);

        // Only move if the direction is in front of the AI character
        if (dotProduct > 0)
        {
            // Set the destination of the NavMeshAgent to the player's position
            agent.SetDestination(player.position);
            isMoving = true;
        }
        else
        {
            isMoving = false;
        }

        // Set the animator's speed based on the agent's velocity
        //float speed = agent.velocity.magnitude;
        //animator.SetFloat("Speed", speed);

        // Add some randomness to the agent's movement by turning slightly
        agent.angularSpeed = Random.Range(80f, 120f);
    }

public void stopmoving()
{
    agent.speed=0;
}


    private void OnAnimatorMove()
    {
        // Use root motion to control the agent's movement based on its animation
        if (isMoving)
        {
            transform.position = animator.rootPosition;
            transform.rotation = animator.rootRotation;
        }
    }
}