using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuButtonStuff : MonoBehaviour
{
    public void ReturnToMenu()
    {
        SceneManager.LoadScene(0);
        Debug.Log("Load");
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("QUit");
    }
}
