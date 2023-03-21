using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private List<GameObject> PlayerPrefabs = new List<GameObject>();
    private List<PlayerInput> players = new List<PlayerInput>();
    [SerializeField]
    private List<Transform> startingPoints;

    private PlayerInputManager playerInputManager;

    // Start is called before the first frame update
    void Awake()
    {
        playerInputManager = FindObjectOfType<PlayerInputManager>();
        playerInputManager.playerPrefab = PlayerPrefabs[0];
        players = PlayerConfigManager.Instance.players;
        playerInputManager.playerPrefab = PlayerPrefabs[players.Count];
        
        foreach (PlayerInput player in players)
        {
            Transform playerParent = players[player.playerIndex].transform;
            playerParent.position = startingPoints[player.playerIndex].position;
            playerInputManager.playerPrefab = PlayerPrefabs[player.playerIndex];
        }
    }

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += addPlayer;
    }
   
    
    public void addPlayer(PlayerInput player)
    {
        players.Add(player);

        Transform playerParent = player.transform;
        playerParent.position = startingPoints[players.Count - 1].position;
        playerInputManager.playerPrefab = PlayerPrefabs[players.Count-1];
    }
}
