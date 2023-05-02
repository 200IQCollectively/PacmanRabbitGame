using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class carrot : MonoBehaviour
{
   // private scripting cript;
     int a=1;
   // public ScoreScript scorer;
   void Start()
    {
      //  scorer = GetComponent<ScoreScript>();
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.gameObject.tag == "Player" || other.gameObject.tag == "whitefox")
        {
             PlayerManager.numberOfCoins++;
          //  AudioManager.instance.Play("Coins");
            PlayerPrefs.SetInt("NumberOfCoins", PlayerManager.numberOfCoins);
            Destroy(gameObject);

        }
    }
}
