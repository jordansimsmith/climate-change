using UnityEngine;
using World.Entities;

namespace World.Tiles
{
    public class Tile : MonoBehaviour
    {
        [SerializeField] private TileType tileType;
        public TileType TileType => tileType;

        private Entity entity;
        public Entity Entity
        {
            get => entity;
            set
            {
                if (entity != null)
                {
                    entity.Destruct();
                    Destroy (entity.gameObject);
                }
                value.transform.SetParent (gameObject.transform, false);
                entity = value;
                entity.Construct();
            }
        }

        public static Vector3 Size { get; } = new Vector3 (10f, 2.5f, 10f);

    }
}