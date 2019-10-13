using UnityEngine.EventSystems;
using World.Entities;

public class ShopItemEventTrigger : EventTrigger
{
    private EntityController entityController;
    private ShopItem shopItem;

    private void Awake()
    {
        entityController = FindObjectOfType<EntityController>();
        shopItem = GetComponentInChildren<ShopItem>();
    }

    public override void OnPointerEnter(PointerEventData eventData)
    {
        EntityType type = shopItem.Item.entity.Type;
        entityController.onHover(type);
    }

    public override void OnPointerExit(PointerEventData eventData)
    {
        entityController.onHoverExit();
    }
}