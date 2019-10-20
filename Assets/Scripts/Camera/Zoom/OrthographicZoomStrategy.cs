using UnityEngine;

public class OrthographicZoomStrategy : IZoomStrategy
{
    public OrthographicZoomStrategy(Camera cam, float startingZoom)
    {
        cam.orthographicSize = startingZoom;
    }

    public void ZoomIn(Camera cam, float delta, float nearZoomLimit)
    {
        // minimum zoom
        if (cam.orthographicSize <= nearZoomLimit)
        {
            return;
        }
        
        // zoom in
        cam.orthographicSize = Mathf.Max(cam.orthographicSize - delta, nearZoomLimit);
    }

    public void ZoomOut(Camera cam, float delta, float farZoomLimit)
    {
        // maximum zoom
        if (cam.orthographicSize >= farZoomLimit)
        {
            return;
        }
        
        // zoom out
        cam.orthographicSize = Mathf.Min(cam.orthographicSize + delta, farZoomLimit);
    } 
}
