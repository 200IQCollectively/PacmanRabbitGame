using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameHandler : MonoBehaviour
{
    private PacmanMazeGen maze;

    private int amountOfCarrots;

    public GameObject fadeObj;

    public PlayerScript player;

    private int livesRemaning = 3;

    PlayerManager_JM playerManager;

    public bool isMultiplayer = false;

    //Leaderboard stuff
    private GameObject leaderboard;
    private GameObject leaderboardEntries;
    public GameObject entry;

    // Start is called before the first frame update
    void Start()
    {
        maze = GameObject.Find("MazePlane").GetComponent<PacmanMazeGen>();
        playerManager = FindObjectOfType<PlayerManager_JM>();

        leaderboard = GameObject.Find("MainCanvas").transform.Find("Leaderboard").gameObject;
        leaderboardEntries = leaderboard.transform.Find("LeaderboardEntries").gameObject;

        StartCoroutine(maze.DelayMazeGen());

        if (playerManager.GetPlayerCount() > 1)
        {
            isMultiplayer = true;
        }

        for (int i = 0; i < 10; i++)
        {
            AddHighScore(0, "DEEZNUTZ");
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
            player.OpenMenu();
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

        fadeObj.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Level " + maze.GetLevel() + " Complete!";

        //player.SetCanMove(false);

        maze.SetLevel();

        yield return new WaitForSeconds(4f);

        StartCoroutine(maze.DelayMazeGen());

        //player.SetCanMove(true);

        fadeObj.SetActive(false);
    }

    public void AddHighScore(int score, string name)
    {
        var obj = Instantiate(entry, transform.position, Quaternion.identity);
        obj.transform.SetParent(leaderboardEntries.transform);
        //obj.GetComponent<Entry>().SetNamePositionScore(score, name);
    }

    public void EndGame()
    {
        leaderboard.SetActive(true);
    }
}
