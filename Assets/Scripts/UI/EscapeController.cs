using System.Collections.Generic;
using DefaultNamespace;
using UnityEngine;
using UnityEngine.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using World;
using World.Tiles;

public class EscapeController : MonoBehaviour
{
    [SerializeField] private GameObject[] uiElements;
    [SerializeField] private GameObject loader;
    [SerializeField] private GameObject escapeUIObj;

    private List<GameObject> elementsOff;
    public Text shareText;
    public Button shareButton;
    private PostProcessingBehaviour blurComponent;
    private bool gameIsPaused = false;
    public bool GameIsPaused => gameIsPaused;
    private GameBoard board;

    // Start is called before the first frame update
    private void Start()
    {
        GameObject mainCamera = GameObject.FindGameObjectWithTag("MainCamera");
        escapeUIObj.SetActive(false);
        blurComponent = mainCamera.GetComponent<PostProcessingBehaviour>();
        board = FindObjectOfType<GameBoard>();
        InvalidateSharingUI();
    }

    // Update is called once per frame
    private void Update()
    {
        // handle pause/resume on escape
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
        Tile.highlightEnabled = false;
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
        Tile.highlightEnabled = true;
    }

    public void SaveButtonOnClick()
    {
        board.SaveActiveWorld();
    }

    public void InvalidateSharingUI()
    {
        if (board.ActiveWorld == null)
        {
            return;
        }

        if (board.ActiveWorld.shareCode != null)
        {
            shareText.gameObject.SetActive(true);
            shareText.text = "Code: " + board.ActiveWorld.shareCode;
            if (shareButton)
                shareButton.GetComponentInChildren<Text>().text = "Stop Sharing";
        }
        else
        {
            shareText.gameObject.SetActive(false);
            if (shareButton)
                shareButton.GetComponentInChildren<Text>().text = "Share World";
        }
    }

    public void ShareButtonOnClick()
    {
        if (board.ActiveWorld.shareCode == null)
        {
            APIService.Instance.CreateShareCode(board.ActiveWorld.id, (shareCode) =>
            {
                Debug.Log(shareCode);
                board.ActiveWorld.shareCode = shareCode;
                InvalidateSharingUI();
            });
        }
        else
        {
            APIService.Instance.DeleteShareCode(board.ActiveWorld.id);
            board.ActiveWorld.shareCode = null;
            InvalidateSharingUI(); // assume it worked.
        }
    }

    public void BackButtonOnClick()
    {
        // load main scene
        Resume();
        board.ActiveWorld = null;
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