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
        
        
        
    }
    public void SpawnPlayers()
    {
        
        List<GameObject> playerObjects = new List<GameObject>();
        print(startingPoints.Count);
        foreach (PlayerInput player in players)
        {

            GameObject playerObject = new GameObject();
            Transform playerParent = players[player.playerIndex].transform;


            print(player.playerIndex);

            if (player.playerIndex == 0)
            {
                
                playerParent.position = startingPoints[0].position;
                playerObject = Instantiate(PlayerPrefabs[0], playerParent.position, Quaternion.identity);
                Debug.Log("player spawned");
            }
            else
            {
                playerParent.position = startingPoints[1].position;
                playerParent.position = new Vector3(playerParent.position.x, playerParent.position.y + 1, playerParent.position.z);
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
                    if (temp == 0)
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

    public void ResetPlayers()
    {
        foreach (PlayerInput player in players)
        {

            if (player.playerIndex == 0)
            {
                player.transform.position = startingPoints[0].position;
            }
            else
            {
                player.transform.position = startingPoints[1].position;
            }
        }
    }

    public void ReleaseFoxs()
    {
        foreach (PlayerInput player in players)
        {

            if (player.playerIndex != 0)
            {
               
                player.transform.parent.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 2);
               // player.transform.position = new Vector3(player.transform.position.x, player.transform.position.y, player.transform.position.z + 2);
            }
            
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

    public void AddSpawnPosition(Transform position)
    {
        startingPoints.Add(position);
    }

    public int GetPlayerCount()
    {
        return players.Count;
    }

    public IEnumerator DelaySpawning()
    {

        yield return new WaitForSeconds(0.5f);

    }

}
