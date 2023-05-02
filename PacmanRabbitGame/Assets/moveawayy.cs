using UnityEngine;
using UnityEngine.AI;

public class moveawayy : MonoBehaviour
{
    public Transform player;
    public float moveSpeed = 5f;
    public float moveDistance = 10f;

    private NavMeshAgent navAgent;
    private bool isMovingAway = false;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        // check if player is within move distance
        if (Vector3.Distance(transform.position, player.position) <= moveDistance)
        {
            // set destination in opposite direction of player
            Vector3 moveDirection = transform.position - player.position;
            Vector3 targetPosition = transform.position + moveDirection.normalized * moveDistance;

            // move towards target position
            if (!isMovingAway)
            {
                navAgent.SetDestination(targetPosition);
                isMovingAway = true;
            }
        }
        else
        {
            // stop moving
            navAgent.SetDestination(transform.position);
            isMovingAway = false;
        }

        // rotate to face movement direction
        Vector3 movement = navAgent.velocity.normalized;
        if (movement.magnitude > 0)
        {
            transform.rotation = Quaternion.LookRotation(movement);
        }
    }
}