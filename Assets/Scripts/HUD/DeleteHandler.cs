using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using World;
using World.Entities;

public class DeleteHandler : EventTrigger
{
    private Image _background;
    private static Vector4 defaultAlpha = new Vector4(1, 0.5f, 0.5f, 0.7f);
    private static Vector4 hoverAlpha = new Vector4(1, 0.5f, 0.5f, 0.9f);
    private EntityPlacer placer;

    public void Start()
    {
        this._background = gameObject.GetComponent<Image>();
        this._background.color = defaultAlpha;
        this.placer = FindObjectOfType<EntityPlacer>();
    }

    public override void OnPointerDown(PointerEventData data)
    {
        SetDeleteMode(placer.Mode != EntityPlacerMode.DELETE);
    }

    public override void OnPointerEnter(PointerEventData data)
    {
        this._background.color = hoverAlpha;
    }

    public override void OnPointerExit(PointerEventData data)
    {
        if (placer.Mode == EntityPlacerMode.DELETE)
        {
            return;
        }
        this._background.color = defaultAlpha;
    }

    public void InvalidateDeleteButton()
    {
        _background.color = placer.Mode == EntityPlacerMode.DELETE ? hoverAlpha : defaultAlpha;

    } 
    public void SetDeleteMode(bool isDeleteMode)
    {
        placer.Mode = isDeleteMode ? EntityPlacerMode.DELETE : EntityPlacerMode.NONE;placer.DeleteMode = isDeleteMode;
        InvalidateDeleteButton();
    }

}