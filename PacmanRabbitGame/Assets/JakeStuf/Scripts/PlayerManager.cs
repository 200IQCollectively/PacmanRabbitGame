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
        playerInputManager = this.GetComponent<PlayerInputManager>();
        
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
        playerInputManager.playerPrefab = PlayerPrefabs[players.Count];
    }
}
