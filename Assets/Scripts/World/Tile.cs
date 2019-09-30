using UnityEngine;

namespace World {
    public class Tile : MonoBehaviour {
        [SerializeField] private TileType tileType;
        public TileType TileType => tileType;

        private Entity entity;
        public Entity Entity {
            get => entity;
            set {
                value.transform.SetParent(gameObject.transform, false);
                entity = value;
            }
        }

        public static Vector3 Size { get; } = new Vector3(10f, 2.5f, 10f);
        
    }
}
