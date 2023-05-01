using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{
    [SerializeField]
    private GameObject ReadyText1;
    [SerializeField]
    private GameObject ReadyText2;
    [SerializeField]
    private GameObject ReadyText3;
    [SerializeField]
    private GameObject ReadyText4;
    

    public void SetPlayerReadyText(int index)
    {
        switch (index)
        {
            case 0:
                ReadyText1.GetComponent<Text>().text = "Ready";

                break;
            case 1:
                ReadyText2.GetComponent<Text>().text = "Ready";
                break;
            case 2:
                ReadyText3.GetComponent<Text>().text = "Ready";
                break;
            case 3:
                ReadyText4.GetComponent<Text>().text = "Ready";
                break;
            default:
                break;
            
        }
    }
}
