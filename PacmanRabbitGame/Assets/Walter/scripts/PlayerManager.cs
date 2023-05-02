using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using Cinemachine;

public class PlayerManager : MonoBehaviour
{
    public static bool isGameOver;
public GameObject gameOverScreen;
    public GameObject pauseMenuScreen;
    public GameObject GamecompleteScreen;
public GameObject advanceScreen;
    public static Vector3 lastCheckPointPos=new Vector3(-0.75389f,-0.4f,-0.77275f);

    public TextMeshProUGUI coinsText;

   // public CinemachineFreeLook VCam;
    private rabbitplayer plays;
    //public GameObject[] playerPrefabs;
    int characterIndex;
    public static int numberOfCoins;
    int sceneIndex, levelPassed;
    private void Start()
    {
 	sceneIndex = SceneManager.GetActiveScene ().buildIndex;
		levelPassed = PlayerPrefs.GetInt ("LevelPassed");
    }
     private void Awake()
    {
      //  characterIndex =  PlayerPrefs.GetInt("SelectedCharacter", 0);
       // GameObject player =  Instantiate(playerPrefabs[characterIndex], lastCheckPointPos, Quaternion.identity);
       // GameObject player =  Instantiate( lastCheckPointPos, Quaternion.id);
      // VCam.m_Follow = player.transform;
     //rabbitplayer.cam.Transform=player.transform;
     //GameObject.FindGameObjectWithTag("Player").transform.position=PlayerManager.lastCheckPointPos;
        numberOfCoins = PlayerPrefs.GetInt("NumberOfCoins", 0);
        isGameOver = false;

    }
    

    // Update is called once per frame
    void Update()
    {
        coinsText.text = numberOfCoins.ToString() ;
         if (isGameOver)
        {
            gameOverScreen.SetActive(true);
        }

    }

     public void ReplayLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        PlayerPrefs.DeleteKey("NumberOfCoins");
    }

    public void PauseGame()
    {
        Time.timeScale = 0;
        pauseMenuScreen.SetActive(true);
    }

    public void ResumeGame()
    {
        Time.timeScale = 1;
        pauseMenuScreen.SetActive(false);
    }
    public void GoToMenu()
    {
        SceneManager.LoadScene("FIRST");
    }
    public void Advancement()
    {
        advanceScreen.SetActive(true);
    }
public void Gamecompleted()
    {
        GamecompleteScreen.SetActive(true);
    }

    public void Advance()
    {
        
      
         SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
          Time.timeScale = 1;
          
    }
    
}
