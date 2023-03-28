using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent), typeof(AgentLinkMover))]
public class AiLocomotion : MonoBehaviour
{
    public Transform Target;
    public float UpdateSpeed = 0.1f; // how often to calculate path based on targets position.
    private NavMeshAgent Navagent;
    [SerializeField]
    private Animator animator;
    [SerializeField]
    private float health;

   

   



    private void Awake()
    {
        Navagent = GetComponent<NavMeshAgent>();
        animator = GetComponent<Animator>();
             

    }

 


    private void Start()
    {
        StartCoroutine(FollowTarget());

    }


    private void Update()
    {
        animator.SetFloat("Move", Navagent.velocity.magnitude);


    }

    private IEnumerator FollowTarget()
    {
        WaitForSeconds Wait = new WaitForSeconds(UpdateSpeed);

        while(enabled)
        {
            Navagent.SetDestination(Target.transform.position);

            yield return Wait;

        }
    }

}
