using System;
using DefaultNamespace;
using Newtonsoft.Json;
using Persistence;
using Persistence.Serializables;
using UnityEngine;
using UnityEngine.AI;
using World.Entities;
using World.Resource;
using World.Tiles;
using Random = UnityEngine.Random;

namespace World
{
    /// <summary>
    /// GameBoard stores a 2D array containing the board of tiles.
    /// </summary>
    public class GameBoard : MonoBehaviour
    {
        [SerializeField] private int boardSize = 20;
        [SerializeField] private TileFactory tileFactory = default;
        [SerializeField] private EntityFactory entityFactory = default;
        [SerializeField] private ResourceSingleton resources;
        private PersistenceManager persistenceManager;

        public NavMeshSurface surface;

        private Tile[,] tiles = default;
        public Tile[,] Tiles => tiles;

        private void Awake()
        {
            // Deserialize world using persistence manager
            persistenceManager = FindObjectOfType<PersistenceManager>();

            if (persistenceManager.SelectedWorld == null)
            {
                BuildWorldFromSerialized(JsonConvert.DeserializeObject<SerializableWorld>(serialisedWorld));
            }
            else
            {
                Debug.Log("BUILT FROM SELECTED");
                resources.Reset();
                BuildWorldFromSerialized(persistenceManager.SelectedWorld.world);
            }

        }

        private void BuildWorldFromSerialized(SerializableWorld world)
        {
            boardSize = world.WorldData.GetLength(0);
            tiles = new Tile[boardSize, boardSize];

            for (int x = 0; x < boardSize; x++)
            {
                for (int z = 0; z < boardSize; z++)
                {
                    SerializableTile serialTile = world.WorldData[x, z];
                    Tile tile = CreateTileAt(x, z, serialTile.TileType);
                    if (serialTile.Entity != null)
                    {
                        tile.Entity = entityFactory.Get(serialTile.Entity.EntityType);
                        tile.Entity.Level = serialTile.Entity.Level;
                    }
                }
            }

            RebakeNavMesh();

            resources.Money = world.ResourceData.Money;
            resources.Population = world.ResourceData.Population;
        }

        public int GetTownHallLevel() {
            foreach (var tile in tiles) {
                var entity = tile.Entity;
                if (entity != null && entity.Type == EntityType.TownHall) {
                    return entity.Level;
                }
            }
            return 1;
        }

        public Tile CreateTileAt(int x, int z, TileType type)
        {
            Tile tile = tileFactory.Get(type);
            var tileTransform = tile.transform;
            tileTransform.position = new Vector3(x * Tile.Size.x, -Tile.Size.y, z * Tile.Size.z);
            tileTransform.SetParent(gameObject.transform);
            tiles[x, z] = tile;
            tile.Pos =  new Vector2Int(x, z);
            return tile;
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

        // Converts tile into grass tile
        public void ReclaimTile(Tile oldTile)
        {
            Vector2Int pos = oldTile.Pos;
            Destroy(oldTile.gameObject);
            CreateTileAt(pos.x, pos.y, TileType.Grass);
        }

        private static string serialisedWorld =
            "{\"WorldData\":[[{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1}],[{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1}],[{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1}],[{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":3},{\"TileType\":3},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1},{\"TileType\":3},{\"TileType\":1},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":3},{\"TileType\":1}],[{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1},{\"TileType\":3},{\"TileType\":0},{\"TileType\":3},{\"TileType\":1},{\"TileType\":3},{\"TileType\":3},{\"TileType\":3},{\"TileType\":1},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1}],[{\"TileType\":1},{\"TileType\":1},{\"TileType\":3},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":3},{\"TileType\":0},{\"TileType\":3},{\"TileType\":0},{\"TileType\":3},{\"TileType\":0},{\"TileType\":3},{\"TileType\":0},{\"TileType\":3},{\"TileType\":3},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1}],[{\"TileType\":1},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1},{\"TileType\":3},{\"TileType\":1},{\"TileType\":3},{\"TileType\":2},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":3},{\"TileType\":1},{\"TileType\":3},{\"TileType\":0},{\"TileType\":0},{\"TileType\":0},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1}],[{\"TileType\":1},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1},{\"TileType\":3},{\"TileType\":3},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0},{\"TileType\":0},{\"TileType\":0},{\"TileType\":0},{\"TileType\":3},{\"TileType\":0},{\"TileType\":0},{\"TileType\":0},{\"TileType\":0},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1}],[{\"TileType\":1},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1},{\"TileType\":3},{\"TileType\":0},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0},{\"TileType\":0},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0},{\"TileType\":0},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1}],[{\"TileType\":1},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1},{\"TileType\":3},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":2},{\"TileType\":0},{\"TileType\":0},{\"TileType\":2},{\"TileType\":0},{\"TileType\":0},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":3},{\"TileType\":0},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1}],[{\"TileType\":1},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1},{\"TileType\":3},{\"TileType\":0},{\"TileType\":0},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0},{\"TileType\":0},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":3},{\"TileType\":1},{\"TileType\":3},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1}],[{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":3},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":3},{\"TileType\":0},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0},{\"TileType\":0},{\"TileType\":0},{\"TileType\":3},{\"TileType\":0},{\"TileType\":0},{\"TileType\":3},{\"TileType\":1}],[{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":3},{\"TileType\":3},{\"TileType\":1},{\"TileType\":3},{\"TileType\":0},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0},{\"TileType\":0},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1}],[{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":3},{\"TileType\":1},{\"TileType\":3},{\"TileType\":0},{\"TileType\":0},{\"TileType\":0},{\"TileType\":0},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1}],[{\"TileType\":1},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1},{\"TileType\":3},{\"TileType\":3},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":3},{\"TileType\":0},{\"TileType\":0},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0},{\"TileType\":3},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1}],[{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":3},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":3},{\"TileType\":1},{\"TileType\":3},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0},{\"TileType\":0},{\"TileType\":0},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":3},{\"TileType\":1}],[{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":3},{\"TileType\":3},{\"TileType\":3},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":3},{\"TileType\":3},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1}],[{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":3},{\"TileType\":0,\"Entity\":{\"EntityType\":0,\"Level\":1}},{\"TileType\":3},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1}],[{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":3},{\"TileType\":1},{\"TileType\":3},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1}],[{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1},{\"TileType\":1}]],\"ResourceData\":{\"totalDemand\":{\"cost\":0,\"money\":0,\"food\":35,\"shelter\":35,\"power\":0,\"environment\":0},\"totalSupply\":{\"cost\":0,\"money\":0,\"food\":0,\"shelter\":0,\"power\":0,\"environment\":320},\"Population\":7,\"Money\":1000,\"MoneyRate\":0}}";

        
        // Serializes the game world using persistence manager
        public void SaveActiveWorld()
        {
            SerializableWorld updatedWorld = new SerializableWorld(Tiles, resources);
            updatedWorld.Name = persistenceManager.SelectedWorld.world.Name;
            updatedWorld.CreationTime = persistenceManager.SelectedWorld.world.CreationTime;
            updatedWorld.IsTutorialCompleted = persistenceManager.SelectedWorld.world.IsTutorialCompleted;
                
            
            persistenceManager.SelectedWorld.world = updatedWorld;
            persistenceManager.SaveGameState(persistenceManager.SelectedWorld);
        }

        public ServerWorld ActiveWorld
        {
            get => persistenceManager.SelectedWorld;
            set { persistenceManager.SelectedWorld = value; }
        }

        
        public int GetNumberOfAdjacentGrassTiles(Tile tile)
        {
            int[] coords = {1, 0, -1};
            int numOfGrassTiles = 0;

            for (var i = 0; i < coords.Length; i++)
            {
                for (var j = 0; j < coords.Length; j++)
                {
                    int x = tile.Pos.x;
                    int y = tile.Pos.y;
                    
                    x += coords[i];
                    y += coords[j];
                    
                    // check if out of bounds
                    if (x < 0 || x >= boardSize || y < 0 || y >= boardSize)
                    {
                        continue;
                    }

                    Tile adjacentTile = tiles[x, y];

                    if (adjacentTile.TileType == TileType.Grass)
                    {
                        numOfGrassTiles++;
                    }
                }
            }

            return numOfGrassTiles;

        }
    }
}