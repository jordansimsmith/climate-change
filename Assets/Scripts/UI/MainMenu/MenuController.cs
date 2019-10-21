using DefaultNamespace.UI.MainMenu;
using Persistence;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    private PersistenceManager persistenceManager;

    public GameObject worldsPanel;

    public GameObject viewWorldPanel;

    public GameObject playGameButton;
    public GameObject loginPanel;

    // Start is called before the first frame update
    void Start()
    {
        worldsPanel.SetActive(false);
        viewWorldPanel.SetActive(false);
        persistenceManager = FindObjectOfType<PersistenceManager>();
    }

    public void PlayButtonOnClick()
    {
        playGameButton.GetComponentInChildren<Text>().text = "Retrieving...";
        worldsPanel.GetComponent<WorldsPanelController>().PopulateWorldsList(() =>
        {
            viewWorldPanel.SetActive(false);
            worldsPanel.SetActive(true);
            playGameButton.GetComponentInChildren<Text>().text = "Play Game";
        });
    }

    public void OpenViewWorldModal()
    {
        worldsPanel.SetActive(false);
        viewWorldPanel.SetActive(true);
    }

    public void ExitButtonOnClick()
    {
        loginPanel.SetActive(true);
        loginPanel.GetComponent<LoginPanelController>().TriggerLogout();
    }
}