using UnityEngine;
using UnityEngine.AI;

public class follower : MonoBehaviour
{
    public Transform player;
    private NavMeshAgent agent;

    private void Start()
    {
        // Get a reference to the NavMeshAgent component
        agent = GetComponent<NavMeshAgent>();

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
        }
    }
}