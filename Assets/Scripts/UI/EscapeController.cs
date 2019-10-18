using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;
using World;

public class EscapeController : MonoBehaviour
{
    [SerializeField] private GameObject[] uiElements;
    [SerializeField] private GameObject loader;
    [SerializeField] private GameObject escapeUIObj;

    private PostProcessingBehaviour blurComponent;
    private List<GameObject> elementsOff;
    private bool gameIsPaused = false;

    private GameBoard board;

    // Start is called before the first frame update
    void Start()
    {
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        escapeUIObj.SetActive(false);
        blurComponent = mainCamera.GetComponent<PostProcessingBehaviour>();
        board = FindObjectOfType<GameBoard>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!gameIsPaused)
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
        elementsOff = new List<GameObject>();
        foreach (GameObject obj in uiElements)
        {
            if (obj.activeSelf)
            {
                obj.SetActive(false);
                elementsOff.Add(obj);
            }
        }

        blurComponent.enabled = true;
        gameIsPaused = true;
        Time.timeScale = 0f;
        escapeUIObj.SetActive(true);
    }

    void Resume()
    {
        foreach (GameObject obj in elementsOff)
        {
            obj.SetActive(true);
        }

        elementsOff = new List<GameObject>();
        blurComponent.enabled = false;
        gameIsPaused = false;
        Time.timeScale = 1f;
        escapeUIObj.SetActive(false);
    }

    public void SaveButtonOnClick()
    {
        board.SaveGameState();
    }

    public void BackButtonOnClick()
    {
        Resume();
        SceneManager.LoadScene("MainUIScene", LoadSceneMode.Single);
        loader.SetActive(true);
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            if (scene.name.Equals("TestScene"))
            {
                loader.SetActive(false);
            }
        };
    }

    public void ResumeButtonOnClick()
    {
        Resume();
    }
}