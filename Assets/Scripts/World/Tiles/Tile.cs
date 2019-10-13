using System;
using HUD;
using UnityEngine;
using World.Entities;

namespace World.Tiles
{
    public class Tile : MonoBehaviour
    {
        [SerializeField]
        private TileType tileType;
        
        public TileType TileType
        {
            get => tileType;
            set => tileType = value;
        }

        private Entity entity;
        private EntitySideBarController sideBarController;

        public void Awake()
        {
            sideBarController = FindObjectOfType<EntitySideBarController>();
        }

        public Entity Entity
        {
            get => entity;
            set
            {
                if (entity != null)
                {
                    entity.Destruct();
                    Destroy(entity.gameObject);
                }

                if (value != null)
                {
                    value.transform.SetParent(gameObject.transform, false);
                    entity = value;
                    entity.Construct();
                }
                else
                {
                    entity = null;
                }
            }
        }

        public void OnMouseDown()
        {
            if (entity != null)
            {
                sideBarController.ShowSideBar(entity);
            }
        }

        public static Vector3 Size { get; } = new Vector3(10f, 2.5f, 10f);
    }
}