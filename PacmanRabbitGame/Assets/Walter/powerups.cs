using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class powerups : MonoBehaviour
{
    bool anth = true;
    public rabbitplayer engage;
    bool rant = false;
    public parent parental;
    public changematerial[] changer;
 

    

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player"|| other.gameObject.tag == "whitefox")
        {

            gameObject.SetActive(false);
            parental.rabbitonly(rant);
            parental.explosioneffect(anth);
            parental.whitewolfonly(anth);
            parental.explosioneffect(rant);
            engage.speed=200;
           

            for(int x=0;x<changer.Length;x++)
            {
            changer[x].changematerialtexture(0);
            }
          
           Invoke("backtodefaults",10);
        }
    }


public void backtodefaults()
{
    Invoke("rest",0.1f);
  parental.whitewolfonly(rant);
            parental.explosioneffect(anth);
             parental.rabbitonly(anth);
Invoke("restdone",0.1f);
             engage.speed=70;
             parental.explosioneffect(rant);
   for(int x=0;x<changer.Length;x++)
            {
            changer[x].changematerialtexture(1);
            }
   }
   public void rest()
   {
       engage.controller.enabled=false;
   }
   public void restdone()
   {
       engage.controller.enabled=true;
   }


   
}
