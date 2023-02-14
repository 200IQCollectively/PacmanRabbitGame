using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class AItargetPlayer : MonoBehaviour
{

    public Transform TargetPostion;

    public NavMeshAgent agent;

    

    // Start is called before the first frame update
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
        
    }

    // Update is called once per frame
    void Update()
    {
        agent.SetDestination(TargetPostion.position);
        Ray ray = new Ray(transform.position, transform.forward);

    }
}
