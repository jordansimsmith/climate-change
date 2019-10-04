using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using World;

namespace World
{
    public class EntityPlacer : MonoBehaviour
    {

        [SerializeField] private EntityFactory factory;
        private Entity entity;
        private GameBoard gameBoard;

        void Awake()
        {
            gameBoard = FindObjectOfType<GameBoard>();
        }
        
        public void spawn(EntityType type)
        {
            entity = factory.Get(type);
        }

        private void Update()
        {
            if (entity == null)
            {
                return;
            }
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            RaycastHit hitInfo;

            Transform buildingTransform = entity.transform;

            if (Physics.Raycast(ray, out hitInfo))
            {
                GameObject gameTile = hitInfo.collider.gameObject;
                Vector3 finalPosition = getNearestPointOnGrid(hitInfo.point);
                buildingTransform.SetParent(gameTile.transform);
                buildingTransform.position = finalPosition;

                if (Input.GetMouseButtonDown(0))
                {
                    // this method is absolutely fucked
                    Tile tile = gameBoard.GetTileAt(finalPosition.x, finalPosition.z);
                    
                    // this code places it but commented out for now
                    tile.Entity = entity;
                    entity = null;
                }
            }
        }
    
        // Probably should be in the GameBoard class...
        private Vector3 getNearestPointOnGrid(Vector3 position)
        {
            Transform buildingTransform = entity.transform;
            position = position - buildingTransform.position;
            float size = Tile.Size.x;

            int xCount = Mathf.FloorToInt(position.x / size);
            int yCount = Mathf.FloorToInt(position.y / size);
            int zCount = Mathf.FloorToInt(position.z / size);

            Vector3 result = new Vector3(
                (float) xCount * size,
                (float) yCount * size,
                (float) zCount * size);

            Vector3 finalPosition = buildingTransform.position + result;
            return finalPosition;

        }

    
}






}
