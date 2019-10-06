using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;

public class EscapeController : MonoBehaviour
{
    private bool gameIsPaused = false;

    public GameObject escapeUIObj;
    private PostProcessingBehaviour blurComponent;
    // Start is called before the first frame update
    void Start()
    {
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        escapeUIObj.SetActive(false);
        blurComponent = mainCamera.GetComponent<PostProcessingBehaviour>();
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
        blurComponent.enabled = true;
        gameIsPaused = true;
        Time.timeScale = 0f;
        escapeUIObj.SetActive(true);
    }

    void Resume()
    {
        blurComponent.enabled = false;
        gameIsPaused = false;
        Time.timeScale = 1f;
        escapeUIObj.SetActive(false);
    }


    public void BackButtonOnClick()
    {
        SceneManager.LoadScene("MainUIScene", LoadSceneMode.Single);
    }

    public void ResumeButtonOnClick()
    {
        Resume();
    }
}
