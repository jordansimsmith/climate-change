using System.Collections;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Net.Configuration;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscapeController : MonoBehaviour
{
    private static bool gameIsPaused = false;

    public GameObject EscapeUIObj;
    // Start is called before the first frame update
    void Start()
    {
        EscapeUIObj.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            gameIsPaused = !gameIsPaused;

            if (gameIsPaused)
            {
                Pause();
            }
            else
            {
                Resume();
            }
        }
    }

    void Pause()
    {
        Time.timeScale = 0f;
        EscapeUIObj.SetActive(true);
    }

    void Resume()
    {
        Time.timeScale = 1f;
        EscapeUIObj.SetActive(false);
    }


    public void BackButtonOnClick()
    {
        SceneManager.LoadScene("MainUIScene", LoadSceneMode.Single);
    }
}
