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

        private void Start()
        {
            _background = gameObject.GetComponent<Image>();
            _background.color = defaultAlpha;
            placer = FindObjectOfType<EntityPlacer>();
            controller = FindObjectOfType<ContentPanelController>();
        }

        public override void OnPointerDown(PointerEventData data)
        {
            SetReclaimMode(placer.Mode != EntityPlacerMode.RECLAIM);
        }

        public override void OnPointerEnter(PointerEventData data)
        {
            _background.color = hoverAlpha;
        }

        public override void OnPointerExit(PointerEventData data)
        {
            if (placer.Mode == EntityPlacerMode.RECLAIM)
            {
                return;
            }

            _background.color = defaultAlpha;
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