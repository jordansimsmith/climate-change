using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Persistence;
using Persistence.Serializables;
using UnityEngine;
using UnityEngine.UI;
using World;

public class WorldsPanelController : MonoBehaviour
{
    
    [SerializeField] private Transform contentPanel;
    [SerializeField] private GameObject worldItemPrefab;
    [SerializeField] private InputField newWorldInput;
    [SerializeField] private Button createButton;

    [SerializeField]
    private WorldManager worldManager;


 
    
    void Start()
    {
        PopulateWorldsList();
        
    }

    void ClearWorlds()
    {
        WorldItem[] worldItems = GetComponentsInChildren<WorldItem>();

        foreach (WorldItem item in worldItems)
        {
            Destroy(item.gameObject);
        }
    }
    
    public void PopulateWorldsList()
    {
        if (worldManager.ServerWorlds != null)
        {
            FillList(worldManager.ServerWorlds);
        }
        else
        {
            worldManager.FetchWorlds(FillList);
        }
        
        
        
    }

    private void FillList(List<ServerWorld> worlds)
    {
        ClearWorlds();
        foreach (ServerWorld world in worlds)
        {
            AddItem(world);
        }
    }

    private void AddItem(ServerWorld world)
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
        ServerWorld serverWorld = worldManager.CreateWorld(newWorldInput.text);
        AddItem(serverWorld);
        newWorldInput.text = "";
    }

    public void CloseButtonClicked()
    {
        ClearWorlds();
        gameObject.SetActive(false);
    }


 
    // Update is called once per frame
    void Update()
    {
        
    }
}
