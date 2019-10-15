using System.Collections;
using System.Collections.Generic;
using Persistence;
using Persistence.Serializables;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{

    private PersistenceManager persistenceManager;
    // Start is called before the first frame update
    void Start()
    {
        persistenceManager = FindObjectOfType<PersistenceManager>();
    }

    public void PlayButtonOnClick()
    {
        SerializableWorld selectedWorld = persistenceManager.LoadSerializedGameState();
        persistenceManager.SelectedWorld = selectedWorld;
        SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
    }

    public void ExitButtonOnClick()
    {
        Application.Quit();
    }
}
