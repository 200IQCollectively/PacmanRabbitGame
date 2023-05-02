
/*using System.Collections;
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
       // GetComponents();
      // scoreText.text = PlayerPrefs.GetInt("Score").ToString();
         score= PlayerPrefs.GetInt("Scor");
    }
  

    public int GetScore()
    {
        return score;
    }

    public void SetScore()
    {
        
        score =score+1;
        scoreText.text=score.ToString();
     
  PlayerPrefs.SetInt("Scor",score);

      
    }

    private void GetComponents()
    {
        canvas = GameObject.Find("Canvas");
        scoreText = canvas.transform.Find("ScoreText").GetComponent<TextMeshProUGUI>();
    }
}
*/