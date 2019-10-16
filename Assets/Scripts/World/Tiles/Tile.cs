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

        public Vector2Int Pos { get; set; }
        public TileType TileType
        {
            get => tileType;
            set => tileType = value;
        }

        public int ReclaimCost
        {
            get
            {
                if (TileType == TileType.Water)
                {
                    return 20000;
                }
                if (TileType == TileType.Sand)
                {
                    return 10000;
                }
                
                return Int32.MaxValue;
            }
        }
        

        private Entity entity;
        private EntityInformationController informationController;

        public void Awake()
        {
            informationController = FindObjectOfType<UpgradeInformationController>();
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

        public static Vector3 Size { get; } = new Vector3(10f, 2.5f, 10f);
    }
}