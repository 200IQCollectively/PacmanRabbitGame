using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private PacmanMazeGen maze;

    private int amountOfCarrots;

    public GameObject fadeObj;

    public PlayerScript player;

    private int livesRemaning = 3;

    PlayerManager playerManager;

    public bool isMultiplayer = false;

    // Start is called before the first frame update
    void Start()
    {
        maze = GameObject.Find("MazePlane").GetComponent<PacmanMazeGen>();
        playerManager = FindObjectOfType<PlayerManager>();
        StartCoroutine(maze.DelayMazeGen());
        if (playerManager.GetPlayerCount() > 1)
        {
            isMultiplayer = true;
        }
    }

    public void SetCarrotAmount(int amount)
    {
        amountOfCarrots += amount;

        if (amountOfCarrots <= 0)
        {
            StartCoroutine(NextLevel());
        }
    }

    public int GetCarrotAmount()
    {
        return amountOfCarrots;
    }

    public void SetPlayer(PlayerScript play)
    {
        player = play;
    }

    public void PlayerCaught()
    {
        if (livesRemaning == 0)
        {
            print("Game Over");
        }
        else
        {
            print("resettinggggg");
            playerManager.ResetPlayers();
            livesRemaning--;
        }
    }

    public void ReleaseFoxPlayers()
    {
        print("releassing");
        playerManager.ReleaseFoxs();
    }

    IEnumerator NextLevel()
    {
        fadeObj.SetActive(true);

        player.SetCanMove(false);

        yield return new WaitForSeconds(4f);

        StartCoroutine(maze.DelayMazeGen());

        player.SetCanMove(true);

        fadeObj.SetActive(false);
    }
}
