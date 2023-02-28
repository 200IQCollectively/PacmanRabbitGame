using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class whitefoxcollider : MonoBehaviour
{
    public foxbodyhide foxbody;
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "enemies" )
        {
            foxbody.hide();

        }
    }
}
