using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TeleportPlayer : MonoBehaviour
{
    public Transform teleportTarget;

    private void Start()
    {
        if (name == "LeftHole")
        {
            teleportTarget = GameObject.Find("OutsideFloor").transform.Find("OLeftHole").transform.Find("TeleportPoint").transform;
        }

        if (name == "RightHole")
        {
            teleportTarget = GameObject.Find("OutsideFloor").transform.Find("ORightHole").transform.Find("TeleportPoint").transform;
        }
    }
}
