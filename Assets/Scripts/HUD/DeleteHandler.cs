using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using World;

public class DeleteHandler : EventTrigger
{
    private Image _background;
    private static Vector4 defaultAlpha = new Vector4(1, 0.5f, 0.5f, 0.7f);
    private static Vector4 hoverAlpha = new Vector4(1, 0.5f, 0.5f, 0.9f);
    private bool isDeleteMode;
    private EntityPlacer placer;

    public void Start()
    {
        this._background = gameObject.GetComponent<Image>();
        this._background.color = defaultAlpha;
        this.placer = FindObjectOfType<EntityPlacer>();
    }

    public override void OnPointerDown(PointerEventData data)
    {
        SetDeleteMode(!isDeleteMode);
    }

    public override void OnPointerEnter(PointerEventData data)
    {
        this._background.color = hoverAlpha;
    }

    public override void OnPointerExit(PointerEventData data)
    {
        if (isDeleteMode)
        {
            return;    
        }
        this._background.color = defaultAlpha;
    }

    public void SetDeleteMode(bool isDeleteMode)
    {
        UpdateDeleteIconColour(isDeleteMode);
        placer.SetDeleteMode(isDeleteMode);
    }

    public void UpdateDeleteIconColour(bool isDeleteMode)
    {
        this.isDeleteMode = isDeleteMode;
        this._background.color = isDeleteMode ? hoverAlpha : defaultAlpha;
    }
}