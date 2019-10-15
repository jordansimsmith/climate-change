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
        }

        private Entity entity;
        private EntityInformationController informationController;
        private ToggleInformationController toggleInformationController;

        public void Awake()
        {
            informationController = FindObjectOfType<UpgradeInformationController>();
            toggleInformationController = FindObjectOfType<ToggleInformationController>();
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

        public bool IsTileValid()
        {
            return TileType.Equals(TileType.Grass) && entity == null;
        }

        public void OnMouseDown()
        {
            if (Entity == null)
            {
                toggleInformationController.SetTileInformation(this, Entity);
            }
            else
            {
                toggleInformationController.SetUpgradeInformation(this, Entity);
            }
        }
        public static Vector3 Size { get; } = new Vector3(10f, 2.5f, 10f);
    }
}