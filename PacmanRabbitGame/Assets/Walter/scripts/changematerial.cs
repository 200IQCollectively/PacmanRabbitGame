using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class changematerial : MonoBehaviour
{
    public Material[] material;
    Renderer rend;
    void Start()
    {
        rend=GetComponent<Renderer>();
        rend.enabled=true;
        rend.sharedMaterial=material[1];
    }

public void changematerialtexture (int a)
{
   int x=a;
    rend.sharedMaterial=material[x];
}


}
