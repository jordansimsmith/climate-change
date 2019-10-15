using System.Collections;
using System.Collections.Generic;
using Persistence;
using Persistence.Serializables;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    private PersistenceManager persistenceManager;
  
    public GameObject worldsPanel;
    // Start is called before the first frame update
    void Start()
    {
        persistenceManager = FindObjectOfType<PersistenceManager>();
    }

    public void PlayButtonOnClick()
    {
        worldsPanel.SetActive(true);
    }

    public void ExitButtonOnClick()
    {
        Application.Quit();
    }
}
