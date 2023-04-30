using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class runaway : MonoBehaviour
{
    private NavMeshAgent _agent;
    public GameObject Player;
    public float enemydistancerun=4.0f;
    void Start()
    {
        _agent=GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        float distance=Vector3.Distance(transform.position,Player.transform.position);
        if(distance<enemydistancerun)
        {
            Vector3 dirToPlayer=transform.position-Player.transform.position;
        Vector3 newPos=transform.position + dirToPlayer;
        _agent.SetDestination(newPos);
    }
}}
