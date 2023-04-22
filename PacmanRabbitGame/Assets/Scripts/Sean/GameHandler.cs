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

    //Leaderboard stuff
    private int[] leaderboardScore = new int[10];
    private string[] leaderboardNames = new string[10];

    // Start is called before the first frame update
    void Start()
    {
        maze = GameObject.Find("MazePlane").GetComponent<PacmanMazeGen>();

        StartCoroutine(maze.DelayMazeGen());
    }

    public void SetCarrotAmount(int amount)
    {
        amountOfCarrots += amount;

        if(amountOfCarrots <= 0)
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

    IEnumerator NextLevel()
    {
        fadeObj.SetActive(true);

        fadeObj.transform.Find("Text").GetComponent<TextMeshProUGUI>().text = "Level " + maze.GetLevel() + " Complete!";

        player.SetCanMove(false);

        maze.SetLevel();

        yield return new WaitForSeconds(4f);

        StartCoroutine(maze.DelayMazeGen());

        player.SetCanMove(true);

        fadeObj.SetActive(false);
    }

    public void AddHighScore(string name, int score)
    {
        
    }

    private void DisplayLeaderboard()
    {

    }
}
