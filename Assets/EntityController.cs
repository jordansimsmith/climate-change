using System.Collections;
using System.Collections.Generic;
using HUD;
using UnityEngine;
using UnityEngine.UI;
using World.Entities;

public class EntityController : MonoBehaviour
{
    [SerializeField] private EntityFactory entityFactory;
    [SerializeField] private Text costLabel;

    private EntitySideBarController sideBarController;
    // Start is called before the first frame update
    void Start()
    {
        sideBarController = FindObjectOfType<EntitySideBarController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onHover(EntityType entityType)
    {
        int cost = entityFactory.GetCost(entityType);
        sideBarController.ShowSideBar(entityFactory.GetPrefab(entityType), false);
//        costLabel.enabled = true;
//        costLabel.text = "Cost:" + cost;
    }

    public void onHoverExit()
    {
        sideBarController.CloseSideBar();
//        costLabel.enabled = false;
//        costLabel.text = "";
    }
}
