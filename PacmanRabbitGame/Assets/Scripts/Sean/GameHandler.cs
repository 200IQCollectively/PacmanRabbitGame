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

        maze.GenerateMazeLayout();
    }

    public void SetCarrotAmount(int amount)
    {
        amountOfCarrots += amount;

        if(amountOfCarrots <= 0)
        {
            StartCoroutine(NextLevel());
        }
    }

    IEnumerator NextLevel()
    {
        fadeObj.SetActive(true);

        yield return new WaitForSeconds(3f);

        maze.GenerateMazeLayout();

        fadeObj.SetActive(false);
    }
}
