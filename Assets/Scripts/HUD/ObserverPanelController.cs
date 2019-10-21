using System.Collections;
using System.Collections.Generic;
using Persistence;
using UnityEngine;
using UnityEngine.UI;

public class ObserverPanelController : MonoBehaviour
{

    public Text worldText;

    public Text shareText;
    public GameObject tornadoPrefab;

    private PersistenceManager persistenceManager;
    // Start is called before the first frame update
    void Start()
    {
        persistenceManager = FindObjectOfType<PersistenceManager>();
        InvalidateUI();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TornadoButtonClicked()
    { 
        Instantiate(tornadoPrefab);
    }

    private void InvalidateUI()
    {
        if (!persistenceManager || persistenceManager.SelectedWorld == null)
        {
            return;
        }
        worldText.text = "World: " + persistenceManager.SelectedWorld.world.Name;
        shareText.text = "Sharing Code: " + persistenceManager.SelectedWorld.shareCode;
    }
    
    
    
    
}
