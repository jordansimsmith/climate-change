using System.Collections;
using System.Collections.Generic;
using Persistence;
using Persistence.Serializables;
using UnityEngine;
using UnityEngine.UI;

public class WorldsPanelController : MonoBehaviour
{
    
    [SerializeField] private Transform contentPanel;
    [SerializeField] private GameObject worldItemPrefab;
    [SerializeField] private InputField newWorldInput;
    [SerializeField] private Button createButton;


    private WorldManager worldManager;
    
    void Start()
    {
        worldManager = GetComponent<WorldManager>();
        AddItems();
    }
    private void AddItems()
    {
        foreach (SerializableWorld world in worldManager.Worlds)
        {
            AddItem(world);
        }
    }

    private void AddItem(SerializableWorld world)
    {
        GameObject newItem = Instantiate(worldItemPrefab);

        // set parent
        newItem.transform.SetParent(contentPanel, false);


        // initialise
        WorldItem worldItem = newItem.GetComponent<WorldItem>();
        worldItem.Initialise(world);
    }

    public void NewWorldTextChanged()
    {
        if (newWorldInput.text.Trim().Equals(""))
        {
            createButton.interactable = false;
        }
        else
        {
            createButton.interactable = true;
        }
    }

    public void CreateButtonClicked()
    {
        SerializableWorld newWorld = worldManager.CreateWorld(newWorldInput.text);
        AddItem(newWorld);
    }

   

    // Update is called once per frame
    void Update()
    {
        
    }
}
