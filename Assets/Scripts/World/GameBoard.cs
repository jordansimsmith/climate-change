using UnityEngine;
using UnityEngine.AI;
using World.Entities;
using World.Resource;
using World.Tiles;

namespace World
{
    public class GameBoard : MonoBehaviour
    {
        [SerializeField] private int boardSize = 20;
        [SerializeField] private TileFactory tileFactory = default;
        [SerializeField] private EntityFactory entityFactory = default;
        [SerializeField] private ResourceSingleton resources;

        public NavMeshSurface surface;

        private Tile[,] tiles = default;

        private void Awake()
        {
            TileType[,] layout = GetComponent<WorldGenerator>().Generate(boardSize);
            tiles = new Tile[boardSize, boardSize];

            for (int x = 0; x < boardSize; x++)
            {
                for (int z = 0; z < boardSize; z++)
                {
                    Tile tile = tileFactory.Get(layout[x, z]);
                    var tileTransform = tile.transform;
                    tileTransform.position = new Vector3(x * Tile.Size.x, -Tile.Size.y, z * Tile.Size.z);
                    tileTransform.SetParent(gameObject.transform);
                    tiles[x, z] = tile;
                }
            }

            // bake nav mesh
            if (surface != null)
            {
                surface.BuildNavMesh();
            }

            resources.MoneyRate = 0;
            resources.Environment.MinAmount = -100;
            resources.Environment.CurAmount = 0;
            resources.Power.MinAmount = -100;
            resources.Power.CurAmount = 0;
            resources.Food.MinAmount = -100;
            resources.Food.CurAmount = 0;
            resources.Shelter.MinAmount = -100;
            resources.Shelter.CurAmount = 0;

            // Generate some random trees
            for (int x = 0; x < boardSize; x++)
            {
                for (int z = 0; z < boardSize; z++)
                {
                    if (tiles[x, z].TileType == TileType.Grass && Random.value > 0.5)
                    {
                        tiles[x, z].Entity = entityFactory.Get(EntityType.Forest);
                    }
                }
            }

            resources.Money = 1000;
        }


        public bool IsEntityTypeOnBoard(EntityType type)
        {
            foreach (var tile in tiles)
            {
                if (tile.Entity != null && tile.Entity.Type == type)
                {
                    return true;
                }
            }

            return false;
        }
    }
}