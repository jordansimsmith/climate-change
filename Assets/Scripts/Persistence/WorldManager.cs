using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using Newtonsoft.Json;
using Persistence;
using Persistence.Serializables;
using UnityEngine;
using World;
using World.Entities;
using World.Resource;
using World.Tiles;
using Random = UnityEngine.Random;

public class WorldManager : MonoBehaviour
{


    private PersistenceManager persistenceManager;
    [SerializeField]
    private WorldGenerator worldGenerator;
 
    void Start()
    {
        persistenceManager = FindObjectOfType<PersistenceManager>();
    }

    void Awake()
    {
      
    }

    public List<SerializableWorld> LoadWorldsFromDisk()
    {
        string[] filePaths = Directory.GetFiles(Path.Combine(Application.persistentDataPath, "worlds"));
        List<SerializableWorld> worlds = new List<SerializableWorld>();
        foreach (string path in filePaths)
        {
            string worldJson = File.ReadAllText(path);
            SerializableWorld world = JsonConvert.DeserializeObject<SerializableWorld>(worldJson);
            worlds.Add(world);
        }

        return worlds;
    }
    

    public SerializableWorld CreateWorld(string name, int boardSize = 20)
    {
        TileType[,] layout = GetComponent<WorldGenerator>().Generate(boardSize);
        SerializableTile[,] tiles = new SerializableTile[boardSize, boardSize];

        for (int i = 0; i < boardSize; i++)
        {
            for (int j = 0; j < boardSize; j++)
            {
                SerializableTile tile = new SerializableTile();
                tile.TileType = layout[i, j];

                if (tile.TileType == TileType.Grass && Random.value > 0.6)
                {
                    tile.Entity = new SerializableEntity();
                    tile.Entity.EntityType = EntityType.Forest;
                    tile.Entity.Level = 1;
                }

                tiles[i, j] = tile;
            }
        }
             
        SerializableResource initialResource = new SerializableResource();
        initialResource.Money = 1000;


        SerializableWorld newWorld = new SerializableWorld();
        newWorld.ResourceData = initialResource;
        newWorld.WorldData = tiles;
        newWorld.Name = name;
        newWorld.CreationTime = DateTime.Now.ToLongTimeString();
        
        persistenceManager.SaveGameState(newWorld);
        
        return newWorld;

    }

  
}
