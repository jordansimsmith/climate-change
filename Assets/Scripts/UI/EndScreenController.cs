using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour
{
    [SerializeField] private GameObject winMenuUI;
    [SerializeField] private GameObject loseMenuUI;
    [SerializeField] private GameObject[] uiElements;
    [SerializeField] private GameObject loader;

    private EscapeController escapeController;
    private PostProcessingBehaviour blurComponent;
    private List<GameObject> elementsOff;

    // Start is called before the first frame update
    private void Start()
    {
        escapeController = gameObject.GetComponent<EscapeController>();
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        winMenuUI.SetActive(false);
        blurComponent = mainCamera.GetComponent<PostProcessingBehaviour>();
    }

    public void EnableWinScreen()
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

        escapeController.enabled = false;
        winMenuUI.SetActive(true);
        blurComponent.enabled = true;
    }

    public void DisableWinScreen()
    {
        foreach (GameObject obj in elementsOff)
        {
            obj.SetActive(true);
        }

        elementsOff = new List<GameObject>();
        blurComponent.enabled = false;
        escapeController.enabled = true;
        winMenuUI.SetActive(false);
    }

    public void EnableLoseScreen()
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

        escapeController.enabled = false;
        loseMenuUI.SetActive(true);
        blurComponent.enabled = true;
    }

    public void DisableLoseScreen()
    {
        foreach (GameObject obj in elementsOff)
        {
            obj.SetActive(true);
        }

        elementsOff = new List<GameObject>();
        blurComponent.enabled = false;
        escapeController.enabled = true;
        loseMenuUI.SetActive(false);
    }

    public void WinContinueButtonOnClick()
    {
        foreach (GameObject obj in elementsOff)
        {
            obj.SetActive(true);
        }

        elementsOff = new List<GameObject>();
        DisableWinScreen();
    }

    public void MainMenuButtonOnClick()
    {
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
}