using System.Collections;
using System.Collections.Generic;
using HUD;
using UnityEngine;
using UnityEngine.UI;
using World.Entities;

public class EntityController : MonoBehaviour
{
    [SerializeField] private EntityFactory entityFactory;

    private EntityInformationController informationController;

    // Start is called before the first frame update
    void Awake()
    {
        informationController = FindObjectOfType<ShopInformationController>();
    }

    public void OnHover(EntityType entityType)
    {
        int cost = entityFactory.GetCost(entityType);
        informationController.ShowInformation(null, entityFactory.GetPrefab(entityType));
    }

    public void OnHoverExit()
    {
        informationController.CloseInformation();
    }
}