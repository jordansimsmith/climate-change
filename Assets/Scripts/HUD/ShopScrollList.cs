using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using World;
using World.Entities;

[System.Serializable]
public class Item
{
    public Entity entity;
    public Sprite sprite;
}

public class ShopScrollList : MonoBehaviour
{
    [SerializeField] private List<Item> shopItems;
    [SerializeField] private Transform contentPanel;
    [SerializeField] private GameObject shopItemPrefab;
    [SerializeField] private GameBoard gameBoard;
    [SerializeField] private EntityHelper entityHelper;
    [SerializeField] private EntityType[] phaseTwoEntities;
    
    private ShopItem townHall;
    private List<ShopItem> phaseTwoEntityItems = new List<ShopItem>();
    

    // Start is called before the first frame update
    void Start()
    {
        // populate list
        AddItems();
        
        // check whether there is a town hall
        InvokeRepeating("CheckForTownHall", 0f, 1f);
        InvokeRepeating("UnlockNextEntities", 0f, 1f);
    }

    private void UnlockNextEntities() {
        Debug.Log("Townhall level " + entityHelper.townhallLevel);
        if (entityHelper.townhallLevel > 1) {
            Debug.Log("More entities unlocked");
            foreach (var item in phaseTwoEntityItems) {
                item.Interactable(true);
            }
        }
    }

    private void CheckForTownHall()
    {
        // is there a town hall placed?
        bool townHallExists = gameBoard.IsEntityTypeOnBoard(EntityType.TownHall);

        // show/hide town hall build button
        townHall.gameObject.SetActive(!townHallExists);
    }

    private void AddItems()
    {
        foreach (Item item in shopItems)
        {
            // insantiate shop item
            GameObject newItem = Instantiate(shopItemPrefab);
            
            // set parent
            newItem.transform.SetParent(contentPanel, false);
            
            // attach event trigger
            newItem.AddComponent<ShopItemEventTrigger>();
            
            // initialise
            ShopItem shopItem = newItem.GetComponent<ShopItem>();
            shopItem.Setup(item);

            if (phaseTwoEntities.Contains(item.entity.Type)) {
                shopItem.Interactable(false);
                phaseTwoEntityItems.Add(shopItem);
            }

            // maintain reference to town hall menu item
            if (item.entity.Type.Equals(EntityType.TownHall))
            {
                townHall = shopItem;
            }
        }
    }
}