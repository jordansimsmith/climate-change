using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Analytics;
using UnityEngine.EventSystems;
using EventTrigger = UnityEngine.Analytics.EventTrigger;

public class DraggableUIPanel : MonoBehaviour, IDragHandler
{
    [SerializeField] private Transform FrameToDrag;

    private Vector3 startDragOffset;

    public void OnDrag(PointerEventData eventData)
    {
        if (eventData.dragging)
        {
            FrameToDrag.position = Input.mousePosition -  startDragOffset;
        }
    }
    
    public void OnBeginDrag()
    {
        startDragOffset = Input.mousePosition - FrameToDrag.position;
        
    }


}
