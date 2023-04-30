using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveme : MonoBehaviour
{
    private float speed=60f;

    // Update is called once per frame
    void Update()
    {
        transform.Translate(0,0,1*speed*Time.deltaTime);
    }
}
