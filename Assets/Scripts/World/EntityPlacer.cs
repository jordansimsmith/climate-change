using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using World;

public class EntityPlacer : MonoBehaviour
{

    [SerializeField] private EntityFactory factory;
    private Entity entity;
    public void spawn(EntityType type)
    {
        entity = factory.Get(type);
    }

    private void Update()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

        RaycastHit hitInfo;
   

        Transform buildingTransform = entity.transform;

        if (Physics.Raycast(ray, out hitInfo))
        {
            GameObject gameTile = hitInfo.collider.gameObject;
            //var finalPosition = grid.GetNearestPointOnGrid(hitInfo.point);

            var position = hitInfo.point;
            var size = Tile.Size.x;
            
           // position -= transform.position;
            
            int xCount = Mathf.FloorToInt(position.x / 10);
           // int yCount = Mathf.FloorToInt(position.y / size);
            int zCount = Mathf.FloorToInt(position.z / 10);

            Vector3 result = new Vector3(
                (float) xCount * 10,
                0,
                (float) zCount) * 10;

            Vector3 finalPosition = result;

            //finalPosition += transform.position;
            
           // buildingTransform.SetParent(gameTile.transform);
            buildingTransform.position = finalPosition;


        }
    }






}
