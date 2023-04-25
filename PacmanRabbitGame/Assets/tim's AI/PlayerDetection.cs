using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class PlayerDetection : MonoBehaviour
{
    NavMeshAgent navAgent;
    Transform player;
    float distanceThreshold = 20.0f;

    void Start()
    {
        navAgent = GetComponent<NavMeshAgent>();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("AI"))
        {
            float distance = Vector3.Distance(other.transform.position, player.transform.position);
            if (distance < distanceThreshold)
            {
                navAgent.SetDestination(player.position);
                Debug.Log("player" + player.position);

            }
        }
    }
}
