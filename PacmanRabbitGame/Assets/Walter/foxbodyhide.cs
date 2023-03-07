using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class foxbodyhide : MonoBehaviour
{
    public GameObject gamer;

    public void hide()
    {
        gamer.SetActive(false); 
    }
    public void unhide()
    {
        gamer.SetActive(true);
    }


}
