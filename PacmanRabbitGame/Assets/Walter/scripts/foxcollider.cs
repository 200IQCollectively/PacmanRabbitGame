using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class foxcollider : MonoBehaviour
{
    public GameObject wolfparent;
    private Animator anime;
    public NavMeshAgent navMeshAgents;

    public rabbitplayer rabbitplay;

    void Start()
    {
        
        anime = GetComponent<Animator>();
    }

    public void killrabbitanimation()

    {
         //this.GetComponent<AIController>().enabled = false;
       // this.GetComponent<Aifollow>().enabled = false;
        navMeshAgents.GetComponent<NavMeshAgent>().enabled = false;
        anime.SetTrigger("wolfkills");
       
    }

    public void OnTriggerEnter(Collider other)
    {
        if ( other.gameObject.tag == "whitefox")
        {
            for (int i = 0; i < wolfparent.transform.childCount; i++)
            {
                if(wolfparent.transform.GetChild(i).tag == "foxbody"|| wolfparent.transform.GetChild(i).tag == "enemyeyes")
                {
                    wolfparent.transform.GetChild(i).gameObject.SetActive(false);
                    this.GetComponent<CapsuleCollider>().enabled = false;
                   Invoke("backtodefaults", 10);
                }
                if (wolfparent.transform.GetChild(i).tag == "bigeyes" )
                {
                    wolfparent.transform.GetChild(i).gameObject.SetActive(true);
                    // Invoke("backtodefaults", 10);
                }
            }

            
        }
        else if (other.gameObject.tag == "subplayer")
        {
            rabbitplay.death();
        }
    }
    private void backtodefaults()
    {
        for (int i = 0; i < wolfparent.transform.childCount; i++)
        {
            if (wolfparent.transform.GetChild(i).tag == "foxbody" || wolfparent.transform.GetChild(i).tag == "enemyeyes")
            {
                wolfparent.transform.GetChild(i).gameObject.SetActive(true);
                this.GetComponent<CapsuleCollider>().enabled = true;
            }
            if (wolfparent.transform.GetChild(i).tag == "bigeyes")
            {
                wolfparent.transform.GetChild(i).gameObject.SetActive(false);
                // Invoke("backtodefaults", 10);
            }
        }

    }

}
