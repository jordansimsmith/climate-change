using UnityEngine;

public class PerspectiveZoomStrategy : IZoomStrategy
{
    private Vector3 normalizedCameraPosition;
    private float currentZoomLevel;

    public PerspectiveZoomStrategy(Camera cam, Vector3 offset, float startingZoom)
    {
        normalizedCameraPosition = new Vector3(0f, Mathf.Abs(offset.y), -Mathf.Abs(offset.x)).normalized;
        currentZoomLevel = startingZoom;
        PositionCamera(cam);
    }

    private void PositionCamera(Camera cam)
    {
        cam.transform.localPosition = normalizedCameraPosition * currentZoomLevel;
    }

    public void ZoomIn(Camera cam, float delta, float nearZoomLimit)
    {
        // minimum zoom
        if (currentZoomLevel <= nearZoomLimit)
        {
            return;
        }
        
        // zoom in
        currentZoomLevel = Mathf.Max(currentZoomLevel - delta, nearZoomLimit);
        PositionCamera(cam);
    }

    public void ZoomOut(Camera cam, float delta, float farZoomLimit)
    {
        // maximum zoom
        if (currentZoomLevel >= farZoomLimit)
        {
            return;
        } 
        
        // zoom out
        currentZoomLevel = Mathf.Min(currentZoomLevel + delta, farZoomLimit);
        PositionCamera(cam);
    }
}