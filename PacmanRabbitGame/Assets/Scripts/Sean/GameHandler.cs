using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private PacmanMazeGen maze;

    private int amountOfCarrots;

    public GameObject fadeObj;

    public PlayerScript player;

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

        player.SetCanMove(false);

        yield return new WaitForSeconds(3f);

        StartCoroutine(maze.DelayMazeGen());

        player.SetCanMove(true);

        fadeObj.SetActive(false);
    }
}
