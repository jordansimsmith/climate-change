using System.Collections;
using System.Collections.Generic;
using HUD;
using UnityEngine;
using UnityEngine.UI;
using World.Entities;

public class EntityController : MonoBehaviour
{
    [SerializeField] private EntityFactory entityFactory;

    private EntitySideBarController sideBarController;

    // Start is called before the first frame update
    void Start()
    {
        sideBarController = FindObjectOfType<EntitySideBarController>();
    }

    public void onHover(EntityType entityType)
    {
        int cost = entityFactory.GetCost(entityType);
        sideBarController.ShowSideBar(entityFactory.GetPrefab(entityType), false);
    }

    public void onHoverExit()
    {
        sideBarController.CloseSideBar();
    }
}