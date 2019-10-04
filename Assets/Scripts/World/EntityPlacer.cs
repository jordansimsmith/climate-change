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
            enabled = true;
        }

        private void Update()
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            Transform buildingTransform = entity.transform;
            if (Physics.Raycast(ray, out var hitInfo, Mathf.Infinity, 1 << 10 ))
            {
                Debug.DrawLine (Camera.main.transform.position, hitInfo.point,  Color.red);
                GameObject gameTile = hitInfo.collider.gameObject;
                buildingTransform.SetParent(gameTile.transform, false);

                if (Input.GetMouseButtonDown(0)) {
                    gameTile.GetComponent<Tile>().Entity = entity;
                    enabled = false;
                }
            }
        }
    


    
}






}
