using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using World;
using World.Entities;

namespace HUD
{
    public class ReclaimHandler : EventTrigger
    {
        private Image _background;
        private static Vector4 defaultAlpha = new Vector4(1, 0.5f, 0.5f, 0.7f);
        private static Vector4 hoverAlpha = new Vector4(1, 0.5f, 0.5f, 1.0f);
        private ContentPanelController controller;
        private EntityPlacer placer;
        public void Start()
        {
            this._background = gameObject.GetComponent<Image>();
            this._background.color = defaultAlpha;
            this.placer = FindObjectOfType<EntityPlacer>();
            this.controller = FindObjectOfType<ContentPanelController>();
        }

        public override void OnPointerDown(PointerEventData data)
        {
            SetReclaimMode(placer.Mode != EntityPlacerMode.RECLAIM);
        }
        public override void OnPointerEnter(PointerEventData data)
        {
            this._background.color = hoverAlpha;
        }

        public override void OnPointerExit(PointerEventData data)
        {
            if (placer.Mode == EntityPlacerMode.RECLAIM)
            {
                return;
            }
            this._background.color = defaultAlpha;
        }

        public void InvalidateReclaimButton()
        {
            _background.color = placer.Mode == EntityPlacerMode.RECLAIM ? hoverAlpha : defaultAlpha;
        }

        public void SetReclaimMode(bool isReclaimMode)
        {
            placer.Mode = isReclaimMode ? EntityPlacerMode.RECLAIM : EntityPlacerMode.NONE;
            Debug.Log("placer mode is " + placer.Mode);
            controller.InvalidateUI();
        }
        

    }
}