using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TeleportPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject player;
    [SerializeField]
    private Transform teleportTarget;

    public TextMeshProUGUI popup;

    //private void OnTriggerEnter(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
            
    //        if (Input.GetKeyDown(KeyCode.E))
    //        {
    //            popup.text = "Press (E) to enter hole";
    //            other.gameObject.transform.position = teleportTarget.position;
    //            Debug.Log("made it");

    //        }
    //        Debug.Log("made it");
    //    }
    //}

    //private void OnTriggerExit(Collider other)
    //{
    //    if (other.tag == "Player")
    //    {
    //        popup.text = "";
    //    }
    //}

    public void TpPlayer(GameObject p)
    {
        teleport(p);
    }

    IEnumerator teleport(GameObject p)
    {
        p.SetActive(false);

        yield return new WaitForSeconds(0.001f);

        p.transform.position = new Vector3 (teleportTarget.position.x, teleportTarget.position.y, teleportTarget.position.z);

        yield return new WaitForSeconds(0.001f);

        p.SetActive(true);
    }
}
