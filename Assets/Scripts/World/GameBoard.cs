using System;
using UnityEngine;
using UnityEngine.AI;
using World.Entities;
using World.Resource;
using World.Tiles;
using Random = UnityEngine.Random;

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

        public Tile[,] Tiles => tiles;

        private void Awake()
        {
            TileType[,] layout = GetComponent<WorldGenerator>().Generate(boardSize);
            tiles = new Tile[boardSize, boardSize];

            for (int x = 0; x < boardSize; x++)
            {
                for (int z = 0; z < boardSize; z++)
                {
                    CreateTileAt(x, z, layout[x, z]);
                }
            }

            RebakeNavMesh();

            resources.MoneyRate = 0;

            // Generate some random trees
            for (int x = 0; x < boardSize; x++)
            {
                for (int z = 0; z < boardSize; z++)
                {
                    if (tiles[x, z].TileType == TileType.Grass && Random.value > 0.6)
                    {
                        tiles[x, z].Entity = entityFactory.Get(EntityType.Forest);
                    }
                }
            }

            resources.Money = 1000;
        }

        public void CreateTileAt(int x, int z, TileType type)
        {
            Tile tile = tileFactory.Get(type);
            var tileTransform = tile.transform;
            tileTransform.position = new Vector3(x * Tile.Size.x, -Tile.Size.y, z * Tile.Size.z);
            tileTransform.SetParent(gameObject.transform);
            tiles[x, z] = tile;
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

        public int CountEntityTypeOnBoard(EntityType type)
        {
            var i = 0;

            foreach (var tile in tiles)
            {
                if (tile.Entity != null && tile.Entity.Type == type)
                {
                    i++;
                }
            }

            return i;
        }

        public Tile GetRandomTile(TileType type)
        {
            Tuple<int, int> randomTilePos = GetRandomTilePosition(type);

            return tiles[randomTilePos.Item1, randomTilePos.Item2];
        }

        public Tuple<int, int> GetRandomTilePosition(TileType type)
        {
            int randomX, randomZ;
            do
            {
                randomX = Random.Range(0, boardSize);
                randomZ = Random.Range(0, boardSize);
            } while (tiles[randomX, randomZ].TileType != type);

            return Tuple.Create(randomX, randomZ);
        }

        public void RebakeNavMesh()
        {
            // bake nav mesh
            if (surface != null)
            {
                surface.BuildNavMesh();
            }
        }


        // Gets resource counts from board demand/supply
        public EntityStatsTuple GetOnBoardResourceCount()
        {
            EntityStats netDemand = new EntityStats();
            EntityStats netSupply = new EntityStats();
            foreach (var tile in tiles)
            {
                if (tile.Entity != null)
                {
                    EntityStats stats = tile.Entity.Stats;
                    if (stats.food > 0)
                    {
                        netSupply.food += stats.food;
                    }
                    else
                    {
                        netDemand.food -= stats.food;
                    }

                    if (stats.environment > 0)
                    {
                        netSupply.environment += stats.environment;
                    }
                    else
                    {
                        netDemand.environment -= stats.environment;
                    }

                    if (stats.shelter > 0)
                    {
                        netSupply.shelter += stats.shelter;
                    }
                    else
                    {
                        netDemand.shelter -= stats.shelter;
                    }

                    if (stats.power > 0)
                    {
                        netSupply.power += stats.power;
                    }
                    else
                    {
                        netDemand.power -= stats.power;
                    }
                }
            }

            EntityStatsTuple tuple = new EntityStatsTuple();
            tuple.supply = netSupply;
            tuple.demand = netDemand;

            return tuple;
        }
    }
}