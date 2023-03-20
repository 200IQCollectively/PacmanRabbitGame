using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    private PacmanMazeGen maze;

    private int amountOfCarrots;

    public GameObject fadeObj;

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

    IEnumerator NextLevel()
    {
        fadeObj.SetActive(true);

        yield return new WaitForSeconds(3f);

        StartCoroutine(maze.DelayMazeGen());

        fadeObj.SetActive(false);
    }
}
