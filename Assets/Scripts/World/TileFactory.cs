using UnityEngine;

namespace World
{
    [CreateAssetMenu]
    public class TileFactory : ScriptableObject
    {
        [SerializeField] private Tile grassPrefab;
        [SerializeField] private Tile sandPrefab;
        [SerializeField] private Tile waterPrefab;
        [SerializeField] private Tile mountainPrefab;

        public Tile Get (TileType tileType)
        {
            switch (tileType)
            {
                case TileType.Grass:
                    return Instantiate (grassPrefab);
                case TileType.Sand:
                    return Instantiate (sandPrefab);
                case TileType.Water:
                    return Instantiate (waterPrefab);
                case TileType.Mountain:
                    return Instantiate (mountainPrefab);
                default:
                    Debug.Log ("Unknown tile type");
                    return null;
            }
        }

    }
}