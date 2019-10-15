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

    [SerializeField]
    private WorldManager worldManager;

    private List<GameObject> worldItems = new List<GameObject>();
    
    void Start()
    {
        PopulateWorldsList();
    }
    
    public void PopulateWorldsList()
    {
        foreach (GameObject worldItem in worldItems)
        {
            Destroy(worldItem);
        }

        worldItems = new List<GameObject>();
        var worlds = worldManager.LoadWorldsFromDisk();
        foreach (SerializableWorld world in worlds)
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
        worldItems.Add(newItem);
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
