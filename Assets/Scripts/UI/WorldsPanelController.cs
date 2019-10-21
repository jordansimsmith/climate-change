using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using World;

public class WorldsPanelController : MonoBehaviour
{
    [SerializeField] private Transform contentPanel;
    [SerializeField] private GameObject worldItemPrefab;
    [SerializeField] private InputField newWorldInput;
    [SerializeField] private Button createButton;

    [SerializeField] private WorldManager worldManager;
    [SerializeField] private GameObject loader;

    private void ClearWorlds()
    {
        WorldItem[] worldItems = GetComponentsInChildren<WorldItem>();

        foreach (WorldItem item in worldItems)
        {
            Destroy(item.gameObject);
        }
    }

    public void PopulateWorldsList(System.Action onFilled)
    {
        worldManager.FetchWorlds((worlds) =>
        {
            FillList(worlds);
            onFilled();
        });
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
        ServerWorld serverWorld = worldManager.CreateWorld(newWorldInput.text);
        AddItem(serverWorld);
        newWorldInput.text = "";
    }

    public void CloseButtonClicked()
    {
        ClearWorlds();
        gameObject.SetActive(false);
    }
}