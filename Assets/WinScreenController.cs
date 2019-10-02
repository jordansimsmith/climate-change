using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;

public class WinScreenController : MonoBehaviour
{
    public GameObject winSceneUI;
    private EscapeController escapeController;
    private PostProcessingBehaviour blurComponent;
    
    // Start is called before the first frame update
    void Start()
    {
        escapeController = gameObject.GetComponent<EscapeController>();
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        winSceneUI.SetActive(false);
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
        winSceneUI.SetActive(true);
        blurComponent.enabled = true;
    }
    
    public void DisableWinScreen()
    {
        blurComponent.enabled = false;
        escapeController.enabled = true;
        winSceneUI.SetActive(false);
    }
    
    public void ContinueButtonOnClick()
    {
        DisableWinScreen();
    }

    public void MainMenuButtonOnClick()
    {
        SceneManager.LoadScene("MainUIScene", LoadSceneMode.Single);
    }
    
}
