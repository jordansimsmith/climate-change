using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine;

public class Placer : MonoBehaviour
{
    private Grid grid;
    private GameObject building;
    private bool hasPlaced = false;
	private bool deleteMode = false;

	private GridWorldController controller;

    private void Awake()
    {
        grid = FindObjectOfType<Grid>();

		controller = gameObject.GetComponent<GridWorldController> ();
    }

 

    private void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

		RaycastHit hitInfo;
        if (building == null)
        {
			if (deleteMode) {

				if (Physics.Raycast (ray, out hitInfo) && Input.GetMouseButtonDown(0)) {
					var finalPosition = grid.GetNearestPointOnGrid (hitInfo.point);
					controller.TryDeleteAtReal (finalPosition.x, finalPosition.z);
				}
			}
            return;
        }

        Transform buildingTransform = building.transform;

        if (Physics.Raycast(ray, out hitInfo))
        {
			GameObject gameTile = hitInfo.collider.gameObject;
            var finalPosition = grid.GetNearestPointOnGrid(hitInfo.point);
			buildingTransform.SetParent (gameTile.transform);
			buildingTransform.position = finalPosition;

            if (Input.GetMouseButtonDown(0))
            {
				var tile = controller.GetTileAtReal(finalPosition.x, finalPosition.z);

				if (tile != GridWorldController.Tile.GRASS) {
					return;
				}
				
				if (controller.AddAtReal (finalPosition.x, finalPosition.z, building)) {
					building = null;
				}
            }
        }
    }

    public void SetObject(GameObject entity)
    {
        building = ((GameObject)Instantiate(entity));
    }

	public void SetDelete(bool delete)
	{
		deleteMode = delete;
	}

	public void FlipDelete()
	{
		deleteMode = !deleteMode;
	}
}
