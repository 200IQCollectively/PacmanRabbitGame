using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerConfigManager : MonoBehaviour
{
    private List<PlayerConfiguration> playerConfigs;
    public List<PlayerInput> players = new List<PlayerInput>();
    
    [SerializeField]
    private int MaxPlayers = 4;

    MainMenuController menuController;

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
            menuController = FindObjectOfType<MainMenuController>();
        }
    }

    public void HandlePlayerJoined(PlayerInput pi)
    {
        Debug.Log("Player Joined" + pi.playerIndex);
        pi.transform.SetParent(transform);
        playerConfigs.Add(new PlayerConfiguration(pi));
        players.Add(pi);
        menuController.SetPlayerReadyText(pi.playerIndex);
        
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

