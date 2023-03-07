using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;


public class aifollow : MonoBehaviour
{
    private NavMeshAgent Mob;
    public GameObject Player;
    public float mobdistancerun = 4.0f;

   void Start()
   {
       Mob=GetComponent<NavMeshAgent>();
   }

    void Update()
    {
        float distance = Vector3.Distance(transform.position,Player.transform.position);
    
    if(distance<mobdistancerun)
    {
        Vector3 dirToPlayer=transform.position - Player.transform.position;
        Vector3 newPos=transform.position - dirToPlayer;
        Mob.SetDestination(newPos);

    }
}
}
