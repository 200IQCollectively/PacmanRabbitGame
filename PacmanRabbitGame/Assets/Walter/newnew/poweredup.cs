using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class poweredup : MonoBehaviour
{
    bool anth = true;
    public PlayerScript engage;
    private runaway away;
    public bomb bomber;
    bool rant = false;
    public parent parental;
    public changematerial[] changer;
    public foxcollider[] lider;
    public ParticleSystem particleObject;

    void Start()
    {
       // particleObject = GetComponent<ParticleSystem>();
    }



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "whitefox")
        {
          //  PlayerManager.numberOfCoins += 5;
            gameObject.SetActive(false);
            parental.rabbitonly(rant);
            parental.explosioneffect(anth);
            //bomber.begins();
           parental.whitewolfonly(anth);
           parental.explosioneffect(rant);

           // engage.speed = 100;
            for (int i = 0; i < lider.Length; i++)
            {
                lider[i].foxrunaway();
            }
            for (int x = 0; x < changer.Length; x++)
            {
                changer[x].changematerialtexture(0);
            }

            Invoke("backtodefaults", 5.0f);
        }
    }


    public void backtodefaults()
    {

        Invoke("rest", 0.1f);
        parental.whitewolfonly(rant);
        parental.explosioneffect(rant);
        parental.rabbitonly(anth);
        Invoke("restdone", 0.1f);
       // engage.speed = 60;
        parental.explosioneffect(anth);
        for (int x = 0; x < changer.Length; x++)
        {
            changer[x].changematerialtexture(1);
        }
        for (int i = 0; i < lider.Length; i++)
        {
            lider[i].foxdontrunaway();
        }
    }
    public void rest()
    {
       // engage.controller.enabled = false;
    }
    public void restdone()
    {
        //engage.controller.enabled = true;
    }



}
