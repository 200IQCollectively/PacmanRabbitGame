using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSetupController : MonoBehaviour
{
    private int PlayerIndex;
    [SerializeField]
    private GameObject readyPanel1;
    [SerializeField]
    private GameObject readyPanel2;
    [SerializeField]
    private GameObject readyPanel3;
    [SerializeField]
    private GameObject readyPanel4;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetPlayerIndex(int pi)
    {
        PlayerIndex = pi;
    }

    public void ReadyPlayer()
    {
        PlayerConfigManager.Instance.ReadyPlayer(PlayerIndex);
        readyPanel1.gameObject.SetActive(true);
    }

}
