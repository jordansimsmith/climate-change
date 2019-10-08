using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using World;
using World.Entities;

public class EntityMenu : EventTrigger
{
    private static IDictionary<string, EntityType> entityMap = new Dictionary<string, EntityType>()
    {
        {"Electricity", EntityType.PowerStation},
        {"Ecosystem", EntityType.Forest},
        {"Food", EntityType.Farm},
        {"Shelter", EntityType.House},
        {"TownHall", EntityType.TownHall},
        {"Factory", EntityType.Factory}
    };

    
    private Image _background;
    private static Vector4 defaultAlpha = new Vector4(1, 1, 1, 0.7f);
    private static Vector4 hoverAlpha = new Vector4(1, 1, 1, 0.9f);
    private EntityPlacer placer;
    private EntityController controller;
    private DeleteHandler handler;


    public void Start()
    {
        this._background = gameObject.GetComponent<Image>();
        this._background.color = defaultAlpha;
        placer = FindObjectOfType<EntityPlacer>();
        controller = GetComponentInParent<EntityController>();
        handler = transform.parent.GetComponentInChildren<DeleteHandler>();
    }

    public override void OnPointerDown(PointerEventData data)
    {
        placer.Spawn(entityMap[gameObject.name]);
        handler.InvalidateDeleteButton();
        
        if (gameObject.name == "TownHall")
        {
            GameObject.Find("TownHall").SetActive(false);
        }
    }


    public override void OnPointerEnter(PointerEventData data)
    {
        this._background.color = hoverAlpha;
        controller.onHover(entityMap[gameObject.name]);
    }

    public override void OnPointerExit(PointerEventData data)
    {
        this._background.color = defaultAlpha;
        controller.onHoverExit();
    }
    

}