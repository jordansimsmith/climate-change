using HUD;
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
    private ContentPanelController controller;

    public void Start()
    {
        _background = gameObject.GetComponent<Image>();
        _background.color = defaultAlpha;
        placer = FindObjectOfType<EntityPlacer>();
        controller = FindObjectOfType<ContentPanelController>();
    }

    public override void OnPointerDown(PointerEventData data)
    {
        SetDeleteMode(placer.Mode != EntityPlacerMode.DELETE);
    }

    public override void OnPointerEnter(PointerEventData data)
    {
        _background.color = hoverAlpha;
    }

    public override void OnPointerExit(PointerEventData data)
    {
        if (placer.Mode == EntityPlacerMode.DELETE)
        {
            return;
        }

        _background.color = defaultAlpha;
    }

    public void InvalidateDeleteButton()
    {
        _background.color = placer.Mode == EntityPlacerMode.DELETE ? hoverAlpha : defaultAlpha;
    }

    public void SetDeleteMode(bool isDeleteMode)
    {
        placer.Mode = isDeleteMode ? EntityPlacerMode.DELETE : EntityPlacerMode.NONE;
        controller.InvalidateUI();
    }
}