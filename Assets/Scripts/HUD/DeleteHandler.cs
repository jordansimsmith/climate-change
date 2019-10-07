using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using World;

public class DeleteHandler : EventTrigger
{
    private Image _background;
    private static Vector4 defaultAlpha = new Vector4(1, 0.5f, 0.5f, 0.7f);
    private static Vector4 hoverAlpha = new Vector4(1, 0.5f, 0.5f, 0.9f);

    public void Start()
    {
        this._background = gameObject.GetComponent<Image>();
        this._background.color = defaultAlpha;
    }

    public override void OnPointerDown(PointerEventData data)
    {
        EntityPlacer placer = FindObjectOfType<EntityPlacer>();
        placer.remove();
    }

    public override void OnPointerEnter(PointerEventData data)
    {
        this._background.color = hoverAlpha;
    }

    public override void OnPointerExit(PointerEventData data)
    {
        this._background.color = defaultAlpha;
    }
}