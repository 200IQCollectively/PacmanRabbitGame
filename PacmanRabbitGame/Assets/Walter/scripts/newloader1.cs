using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class newloader1 : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
       if( Input.GetKeyDown(KeyCode.Space))
            {
            SceneManager.LoadScene("FIRST", LoadSceneMode.Single);
        }
    }

    
}
