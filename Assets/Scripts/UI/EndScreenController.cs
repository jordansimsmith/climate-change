using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;

public class EndScreenController : MonoBehaviour
{
    public GameObject winMenuUI;
    public GameObject loseMenuUI;
    private EscapeController escapeController;
    private PostProcessingBehaviour blurComponent;
    
    // Start is called before the first frame update
    void Start()
    {
        escapeController = gameObject.GetComponent<EscapeController>();
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        winMenuUI.SetActive(false);
        blurComponent = mainCamera.GetComponent<PostProcessingBehaviour>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.O))
        {
            EnableWinScreen();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            DisableWinScreen();
        }
        
    }

    public void EnableWinScreen()
    {
        escapeController.enabled = false;
        winMenuUI.SetActive(true);
        blurComponent.enabled = true;
    }
    
    public void DisableWinScreen()
    {
        blurComponent.enabled = false;
        escapeController.enabled = true;
        winMenuUI.SetActive(false);
    }
    
    public void EnableLoseScreen()
    {
        escapeController.enabled = false;
        loseMenuUI.SetActive(true);
        blurComponent.enabled = true;
    }
    
    public void DisableLoseScreen()
    {
        blurComponent.enabled = false;
        escapeController.enabled = true;
        loseMenuUI.SetActive(false);
    }
    
    public void WinContinueButtonOnClick()
    {
        DisableWinScreen();
    }

    public void MainMenuButtonOnClick()
    {
        SceneManager.LoadScene("MainUIScene", LoadSceneMode.Single);
    }
    
}
