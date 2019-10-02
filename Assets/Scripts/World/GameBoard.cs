using UnityEngine;

namespace World
{
    public class GameBoard : MonoBehaviour
    {

        [SerializeField] private int boardSize = 20;
        [SerializeField] private TileFactory tileFactory = default;
        [SerializeField] private EntityFactory entityFactory = default;

        private Tile[, ] tiles = default;

        private void Awake ()
        {
            TileType[, ] layout = GetComponent<WorldGenerator> ().Generate (boardSize);
            tiles = new Tile[boardSize, boardSize];

            for (int x = 0; x < boardSize; x++)
            {
                for (int z = 0; z < boardSize; z++)
                {
                    Tile tile = tileFactory.Get (layout[x, z]);
                    var tileTransform = tile.transform;
                    tileTransform.position = new Vector3 (x * Tile.Size.x, -Tile.Size.y, z * Tile.Size.z);
                    tileTransform.SetParent (gameObject.transform);
                    tiles[x, z] = tile;
                }
            }

            // Generate some random houses
            for (int x = 0; x < boardSize; x++)
            {
                for (int z = 0; z < boardSize; z++)
                {

                    float rand = Random.value;
                    if (tiles[x, z].TileType == TileType.Grass)
                    {
                        if (rand < 0.2)
                        {
                            tiles[x, z].Entity = entityFactory.Get (EntityType.Factory);
                        }
                        else if (rand < 0.4)
                        {
                            tiles[x, z].Entity = entityFactory.Get (EntityType.Farm);
                        }
                        else if (rand < 0.6)
                        {
                            tiles[x, z].Entity = entityFactory.Get (EntityType.PowerStation);
                        }
                        else if (rand < 0.8)
                        {
                            tiles[x, z].Entity = entityFactory.Get (EntityType.House);
                        }
                        else
                        {
                            tiles[x, z].Entity = entityFactory.Get (EntityType.TownHall);
                        }
                    }

                }
            }

            // Generate some random trees
            for (int x = 0; x < boardSize; x++)
            {
                for (int z = 0; z < boardSize; z++)
                {
                    if (tiles[x, z].TileType == TileType.Grass && Random.value > 0.5)
                    {
                        tiles[x, z].Entity = entityFactory.Get (EntityType.Forest);
                    }
                }
            }
        }
    }
}