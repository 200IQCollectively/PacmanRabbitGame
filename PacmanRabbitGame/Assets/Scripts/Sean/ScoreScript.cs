using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreScript : MonoBehaviour
{
    private GameObject canvas;
    private TextMeshProUGUI scoreText;

    private int score = 0;

    // Start is called before the first frame update
    private void Start()
    {
        GetComponents();

        scoreText.text = "Score: " + score;
    }

    public int GetScore()
    {
        return score;
    }

    public void SetScore(int increase)
    {
        score += increase;

        scoreText.text = "Score: " + score;
    }

    private void GetComponents()
    {
        canvas = GameObject.Find("MainCanvas");
        scoreText = canvas.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
    }
}
