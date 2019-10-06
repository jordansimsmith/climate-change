using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using World;
using World.Entities;

public class EntityMenu : EventTrigger
{
    private Image _background;
    private static Vector4 defaultAlpha = new Vector4(1, 1, 1, 0.7f);
    private static Vector4 hoverAlpha = new Vector4(1, 1, 1, 0.9f);
    
    

    public void Start() {
        this._background = gameObject.GetComponent<Image>();
        this._background.color = defaultAlpha;
    }

    public override void OnPointerDown(PointerEventData data)
    {
//        EntitySubMenu subMenu = FindObjectsOfType<EntitySubMenu>()[0];
//        subMenu.Toggle(gameObject.name);
        EntityPlacer placer = FindObjectOfType<EntityPlacer>();
        placer.spawn(gameObject.name);
    }


 
    
    public override void OnPointerEnter(PointerEventData data)
    {
//        EntityPlacer placer = FindObjectOfType<EntityPlacer>();
//        placer.spawn(EntityType.PowerStation);
//        
//        
        this._background.color = hoverAlpha;
    }

    public override void OnPointerExit(PointerEventData data)
    {
        this._background.color = defaultAlpha;
    }
}
