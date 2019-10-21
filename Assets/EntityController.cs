using HUD;
using UnityEngine;
using World.Entities;

public class EntityController : MonoBehaviour
{
    [SerializeField] private EntityFactory entityFactory;

    private EntityInformationController informationController;

    void Awake()
    {
        informationController = FindObjectOfType<ShopInformationController>();
    }

    public void OnHover(EntityType entityType)
    {
        informationController.ShowInformation(entityFactory.GetPrefab(entityType));
    }

    public void OnHoverExit()
    {
        informationController.CloseInformation();
    }
}