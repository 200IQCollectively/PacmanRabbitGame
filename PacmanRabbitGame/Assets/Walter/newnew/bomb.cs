using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bomb : MonoBehaviour
{
    public ParticleSystem particleObject;
    // Start is called before the first frame update
    void Start()
    {
         particleObject = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void begins()
    {
        particleObject.Play();
    }
     public void ends()
    {
        particleObject.Stop();
    }
}
