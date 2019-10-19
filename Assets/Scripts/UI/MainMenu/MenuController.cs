using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Persistence;
using Persistence.Serializables;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    private PersistenceManager persistenceManager;
  
    public GameObject worldsPanel;

    public GameObject viewWorldPanel;
    // Start is called before the first frame update
    void Start()
    {
        worldsPanel.SetActive(false);
        viewWorldPanel.SetActive(false);
        persistenceManager = FindObjectOfType<PersistenceManager>();
    }

    public void PlayButtonOnClick()
    {
        viewWorldPanel.SetActive(false);
        AuthHandler handler = FindObjectOfType<AuthHandler>();
        if (APIService.Instance.access_token != null)
        {
            worldsPanel.SetActive(true);
            worldsPanel.GetComponent<WorldsPanelController>().PopulateWorldsList();
        }
        else
        {
            
            handler.OpenUI((auth) =>
            {
                APIService.Instance.access_token = auth.FirebaseToken;
                Debug.Log("Logged in "+auth.FirebaseToken);
                worldsPanel.GetComponent<WorldsPanelController>().PopulateWorldsList();

                worldsPanel.SetActive(true);
            });
        }
      
    }

    public void OpenViewWorldModal()
    {
        worldsPanel.SetActive(false);
        viewWorldPanel.SetActive(true);
    }

    public void ExitButtonOnClick()
    {
        Application.Quit();
    }
}
