using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Transform teleportTarget;



    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Player" && !player.GetComponent<PlayerScript>().GetHasTeleported())
        {
            Debug.Log("sup");
            player.GetComponent<PlayerScript>().SetHasTeleported(true);
            player.transform.position = teleportTarget.transform.position;
        }

    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {

            literaltimewaste();

        }
    }

    IEnumerator literaltimewaste()
    {
        yield return new WaitForSeconds(5f);

        player.GetComponent<PlayerScript>().SetHasTeleported(false);
    }
}
