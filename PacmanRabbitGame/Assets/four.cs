using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class four : MonoBehaviour
{
    public Transform target;
    public float lineOfSightDistance = 10f;
    public float forwardSpeed = 5f;
    public float turnSpeed = 90f;

    private NavMeshAgent navMeshAgent;
    private Animator animator;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        // Check if the player is within line of sight
        Vector3 directionToTarget = target.position - transform.position;
        if (Vector3.Dot(transform.forward, directionToTarget.normalized) > 0.5f && 
            directionToTarget.magnitude <= lineOfSightDistance)
        {
            // Set the NavMeshAgent destination to the player's position
            navMeshAgent.SetDestination(target.position);
            // Set the NavMeshAgent speed to forward speed
            navMeshAgent.speed = forwardSpeed;
            // Rotate the NavMeshAgent towards the player
            Vector3 newDirection = Vector3.RotateTowards(transform.forward, directionToTarget, turnSpeed * Time.deltaTime, 0.0f);
            transform.rotation = Quaternion.LookRotation(newDirection);
            // Stop the NavMeshAgent animation
            animator.SetFloat("Speed", 0f);
        }
        else
        {
            // Stop the NavMeshAgent
            navMeshAgent.SetDestination(transform.position);
            // Stop the NavMeshAgent animation
            animator.SetFloat("Speed", 0f);
        }
    }
}