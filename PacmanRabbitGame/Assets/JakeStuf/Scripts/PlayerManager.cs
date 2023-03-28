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
        List<GameObject> playerObjects = new List<GameObject>();
        print(players.Count);

        foreach (PlayerInput player in players)
        {

            GameObject playerObject = new GameObject();
            Transform playerParent = players[player.playerIndex].transform;
            playerParent.position = startingPoints[player.playerIndex].position;
            
            print(player.playerIndex);
            
            if (player.playerIndex == 0)
            {
                
                playerObject = Instantiate(PlayerPrefabs[0], playerParent.position,Quaternion.identity);
                
            }
            else
            {
                playerObject = Instantiate(PlayerPrefabs[1], playerParent.position, Quaternion.identity);

            }
            player.transform.SetParent(playerObject.transform);

            playerObjects.Add(playerObject);
        }
        int temp = 0;
        switch (players.Count)
        {

            case 2:
                
                foreach (GameObject playerobj in playerObjects)
                {
                    if(temp == 0)
                    {
                        playerobj.transform.GetComponentInChildren<Camera>().rect = new Rect(0, 0, 0.5f, 1);
                    }
                    else
                    {
                        playerobj.transform.GetComponentInChildren<Camera>().rect = new Rect(0.5f, 0, 0.5f, 1);
                    }
                    temp++;
                }
                break;

            case 3:
                foreach (GameObject playerobj in playerObjects)
                {
                    
                    switch (temp)
                    {
                        case 1:
                            playerobj.transform.GetComponentInChildren<Camera>().rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                            break;
                        case 2:
                            playerobj.transform.GetComponentInChildren<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                            break;
                        case 3:
                            playerobj.transform.GetComponentInChildren<Camera>().rect = new Rect(0, 0, 1, 0.5f);
                            break;
                        default:
                            break;
                    }
                    temp++;
                }
                break;

            case 4:
                foreach (GameObject playerobj in playerObjects)
                {

                    switch (temp)
                    {
                        case 1:
                            playerobj.transform.GetComponentInChildren<Camera>().rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                            break;
                        case 2:
                            playerobj.transform.GetComponentInChildren<Camera>().rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                            break;
                        case 3:
                            playerobj.transform.GetComponentInChildren<Camera>().rect = new Rect(0, 0, 0.5f, 0.5f);
                            break;
                        case 4:
                            playerobj.transform.GetComponentInChildren<Camera>().rect = new Rect(0.5f, 0, 0.5f, 0.5f);
                            break;
                        default:
                            break;
                    }
                    temp++;
                }
                break;

            default:
                break;
        }
    }

    private void OnEnable()
    {
        playerInputManager.onPlayerJoined += addPlayer;
    }

    private void OnDisable()
    {
        playerInputManager.onPlayerJoined -= addPlayer;
    }


    public void addPlayer(PlayerInput player)
    {
        players.Add(player);

        Transform playerParent = player.transform;
        playerParent.position = startingPoints[players.Count - 1].position;
        playerInputManager.playerPrefab = PlayerPrefabs[players.Count-1];
    }
}
