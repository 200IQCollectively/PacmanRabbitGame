using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatforms : MonoBehaviour
{
    public Vector3 startPoint;
    public Vector3 endPoint;
    public float speed = 5;

    // Update is called once per frame
    void Update()
    {
        PlatformsMove();
    }

    private void PlatformsMove()
    {
        gameObject.transform.position = Vector3.MoveTowards(gameObject.transform.position, endPoint, speed * Time.deltaTime);

        if(gameObject.transform.position == endPoint)
        {
            gameObject.transform.position = startPoint;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player")
        {
            other.gameObject.transform.SetParent(gameObject.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            other.gameObject.transform.SetParent(null);
        }
    }
}
