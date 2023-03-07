using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class teleporter : MonoBehaviour
{

    [SerializeField] Transform tp;
    [SerializeField] GameObject player;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(Teleport());
    }

    IEnumerator Teleport()
    {
       player.SetActive(false);
        yield return new WaitForSeconds(0.001f);
        player.transform.position = new Vector3(
            tp.transform.position.x,
            tp.transform.position.y,
            tp.transform.position.z
        );
        yield return new WaitForSeconds(0.001f);
        player.SetActive(true);
    }
}

//private void OnTriggerEnter(Collider other)
//{
//    StartCoroutine(Teleport());
//}

//IEnumerator Teleport()
//{
//    player.SetActive(false);
//    yield return new WaitForSeconds(0.1f);
//    player.transform.position = new Vector3(
//        tp.transform.position.x,
//        tp.transform.position.y,
//        tp.transform.position.z
//    );
//    yield return new WaitForSeconds(0.1f);
//    player.SetActive(true);
//}
//}