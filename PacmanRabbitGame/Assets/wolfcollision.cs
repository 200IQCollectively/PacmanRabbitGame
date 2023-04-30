using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wolfcollision : MonoBehaviour
{
    public PlayerScript rabbitcharacter;
    public GameObject wolfparent;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
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
                   Invoke("backtodefaults", 10f);
                }
                if (wolfparent.transform.GetChild(i).tag == "bigeyes" )
                {
                    wolfparent.transform.GetChild(i).gameObject.SetActive(true);
                    // Invoke("backtodefaults", 10);
                }
            }

            
        }

         
           
    }

    
    private void backtodefaults()
    {
         this.GetComponent<aifollow>().enabled = true;
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
