using UnityEngine;
using UnityEngine.AI;

public class moveaway : MonoBehaviour
{
    public Transform player;
    public float fleeDistance = 10f;
    private NavMeshAgent agent;

    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void Update()
    {
        if (Vector3.Distance(transform.position, player.position) < fleeDistance)
        {
            Vector3 dirToPlayer = transform.position - player.position;
            Vector3 newPos = transform.position + dirToPlayer.normalized * fleeDistance;
            agent.SetDestination(newPos);
        }
    }
}