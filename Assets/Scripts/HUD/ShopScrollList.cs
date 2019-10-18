using System.Collections.Generic;
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

    private ShopItem townHall;

    // Start is called before the first frame update
    void Start()
    {
        // populate list
        AddItems();
        
        // check whether there is a town hall
        InvokeRepeating("CheckForTownHall", 0f, 1f);
    }

    public void DisableTownHall()
    {
        // only one town hall should be constructed
        if (townHall != null)
        {
            // hide town hall
            townHall.gameObject.SetActive(false);
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

            // maintain reference to town hall menu item
            if (item.entity.Type.Equals(EntityType.TownHall))
            {
                townHall = shopItem;
            }
        }
    }
}