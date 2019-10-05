using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void PlayButtonOnClick()
    {
        SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
    }

    public void ExitButtonOnClick()
    {
        Application.Quit();
    }
}
