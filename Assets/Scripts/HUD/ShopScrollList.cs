using System.Collections.Generic;
using UnityEngine;
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

    // Start is called before the first frame update
    void Start()
    {
        // populate list
        AddItems();
    }

    private void AddItems()
    {
        foreach (Item item in shopItems)
        {
            // insantiate shop item
            GameObject newItem = Instantiate(shopItemPrefab);
            
            // set parent
            newItem.transform.SetParent(contentPanel);
            
            ShopItem shopItem = newItem.GetComponent<ShopItem>();
            shopItem.Setup(item);
        }
    }
}