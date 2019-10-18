using Persistence.Serializables;
using UnityEngine;
using UnityEngine.UI;

public class WorldsPanelController : MonoBehaviour
{
    [SerializeField] private Transform contentPanel;
    [SerializeField] private GameObject worldItemPrefab;
    [SerializeField] private InputField newWorldInput;
    [SerializeField] private Button createButton;
    [SerializeField] private GameObject loader;

    [SerializeField] private WorldManager worldManager;


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
        ClearWorlds();
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
        worldItem.Initialise(world, loader);
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
        newWorldInput.text = "";
    }

    public void CloseButtonClicked()
    {
        ClearWorlds();
        gameObject.SetActive(false);
    }
}