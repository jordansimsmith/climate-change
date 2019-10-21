using UnityEngine;

namespace World.Tiles
{
    /// <summary>
    /// Runtime retrieval of tile prefabs (can be instantiated or not)
    /// </summary>
    [CreateAssetMenu]
    public class TileFactory : ScriptableObject
    {
        [SerializeField] private Tile grassPrefab;
        [SerializeField] private Tile sandPrefab;
        [SerializeField] private Tile waterPrefab;
        [SerializeField] private Tile mountainPrefab;

        public Tile Get (TileType tileType)
        {
            return Instantiate(GetPrefab(tileType));
        }
        
        public Tile GetPrefab (TileType tileType)
        {
            switch (tileType)
            {
                case TileType.Grass:
                    return grassPrefab;
                case TileType.Sand:
                    return sandPrefab;
                case TileType.Water:
                    return waterPrefab;
                case TileType.Mountain:
                    return mountainPrefab;
                default:
                    Debug.Log ("Unknown tile type");
                    return null;
            }
        }

    }
}