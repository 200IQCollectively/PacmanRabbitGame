using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerConfigManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;

    [SerializeField]
    private int MaxPlayers = 4;

    [SerializeField]
    private GameObject ReadyText1;

    [SerializeField]
    private GameObject ReadyText2;
    [SerializeField]
    private GameObject ReadyText3;
    [SerializeField]
    private GameObject ReadyText4;

    public static PlayerConfigManager Instance { get; private set; }

    private void Awake()
    {
        if(Instance != null)
        {
            Debug.Log("trying to create another instance");
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(Instance);
            playerConfigs = new List<PlayerConfiguration>();
        }
    }

    public void ReadyPlayer(int index)
    {
        playerConfigs[index].IsReady = true;
        if(playerConfigs.Count == MaxPlayers && playerConfigs.TrueForAll(p=>p.IsReady))
        {
            SceneManager.LoadScene("SampleScene");
        }
    }

    public void HandlePlayerJoined(PlayerInput pi)
    {
        Debug.Log("Player Joined" + pi.playerIndex);
        pi.transform.SetParent(transform);
        playerConfigs.Add(new PlayerConfiguration(pi));

        switch (pi.playerIndex)
        {
            case 0:
                ReadyText1.GetComponent<Text>().text = "Ready";
                break;
            case 1:
                ReadyText2.GetComponent<Text>().text = "Ready";
                break;
            case 2:
                ReadyText3.GetComponent<Text>().text = "Ready";
                break;
            case 3:
                ReadyText4.GetComponent<Text>().text = "Ready";
                break;
            default:
                break;
        }
    }

}

public class PlayerConfiguration
{
    public PlayerConfiguration(PlayerInput pi)
    {
        PlayerIndex = pi.playerIndex;
        Input = pi;
    }
    public PlayerInput Input { get; set; }

    public int PlayerIndex { get; set; }

    public bool IsReady { get; set; }

}

