using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;

public class foxcollider : MonoBehaviour
{
    public GameObject wolfparent;
    private GameObject baseplayer;
   
    [SerializeField] GameObject[] player;
      [SerializeField] GameObject[] rabbitplayer;
    [SerializeField] Transform[] tip;
    [SerializeField] Transform[] tipe;
   
    private HealthManager healther;
    //private LevelControlScript onto;
    public GameObject basecarrot;
    
   //public GameObject manager;
    private Animator anime;
    
    public NavMeshAgent navMeshAgents;
 public GameObject Player;
    public float enemydistancerun;
    public rabbitplayer rabbitplay;
    public int range =1;

    void Start()
    {
        
        anime = GetComponent<Animator>();
    }

void Update()
    {
       
    }

    public void killrabbitanimation()

    {
        
       // this.GetComponent<Aifollow>().enabled = false;
        navMeshAgents.GetComponent<NavMeshAgent>().enabled = false;
        anime.SetTrigger("wolfkills");
    
       
    }
    public void setfoxesoff()
    {
        for(int ant=0;ant<player.Length;ant++)
        {
            player[ant].SetActive(false);
        }
    }

public void setfoxeson()
    {
        for(int ant=0;ant<player.Length;ant++)
        {
            player[ant].SetActive(true);
           player[ant].GetComponent<aifollow>().enabled = true;
          player[ant].GetComponent<runaway>().enabled = false;
        }
        
    }

public void foxrunaway()
{
   
    this.GetComponent<aifollow>().enabled = false;
     this.GetComponent<runaway>().enabled = true;
}


public void foxdontrunaway()
{
    
     this.GetComponent<aifollow>().enabled = true;
     this.GetComponent<runaway>().enabled = false;
}

    public void pausekill()

    {
         this.GetComponent<aifollow>().enabled = false;
       // this.GetComponent<Aifollow>().enabled = false;
        navMeshAgents.GetComponent<NavMeshAgent>().enabled = false;
        Invoke("dontpausekill",10f); 
    }

    public void pausegameafter()

    {
         this.GetComponent<aifollow>().enabled = false;
       // this.GetComponent<Aifollow>().enabled = false;
        navMeshAgents.GetComponent<NavMeshAgent>().enabled = false;
       
    }

     public void dontpausekill()

    {
         this.GetComponent<aifollow>().enabled = true;
       // this.GetComponent<Aifollow>().enabled = false;
        navMeshAgents.GetComponent<NavMeshAgent>().enabled = true;
        
    
       
    }

    public void OnTriggerEnter(Collider other)
    {
        if ( other.gameObject.tag == "whitefox")
        {
            PlayerManager.numberOfCoins+=20;
            this.GetComponent<aifollow>().enabled = false;
             //this.GetComponent<AIController>().enabled = false;
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
        else if (other.gameObject.tag == "subplayer")
        {
           HealthManager.health--;
           if(HealthManager.health<=0)
           {
               PlayerManager.isGameOver=true;
                rabbitplay.death();
               
           }
           else
           {
             // DontDestroyOnLoad(basecarrot);
              // SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
              //pausekill();
              this.enabled=false;
              rabbitplay.dead();
              
              
              StartCoroutine(Teleporter());
              
          // setfoxesoff();
               // manager.transform.position=PlayerManager.lastCheckPointPos;
            }

            
      
         
        }
    }

    IEnumerator Teleporter()
    {
        
        
        
        //rabbitplay.death();
       //player.SetActive(false);
      // rabbitplay.death();
        //yield return new WaitForSeconds(.1f);
        // rabbitplay.Run();
       
        
        rabbitplay.notdead();
        yield return new WaitForSeconds(.4f);

        for (int i=0;i<rabbitplayer.Length;i++)
        {
        rabbitplayer[i].transform.position = new Vector3(
            tipe[i].transform.position.x,
            tipe[i].transform.position.y,
            tipe[i].transform.position.z
            );
        } 

         for(int i=0;i<player.Length;i++)
        {
            
        player[i].transform.position = new Vector3(
            tip[i].transform.position.x,
            tip[i].transform.position.y,
            tip[i].transform.position.z
            
        );
        
        }
       setfoxesoff();
         Invoke("setfoxeson",4);
     
        //player.SetActive(true);
        
         //rabbitplay.Idle();
    }

public void interest()
{
    rabbitplay.death();
}
public void callme()
{
      this.GetComponent<aifollow>().enabled = false;
              this.GetComponent<runaway>().enabled = true;
}
public void dontcallme()
{
      this.GetComponent<aifollow>().enabled = true;
              this.GetComponent<runaway>().enabled = false;
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
