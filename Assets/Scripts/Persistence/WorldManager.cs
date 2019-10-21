using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.IO;
using DefaultNamespace;
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


    private List<ServerWorld> serverWorlds;

 
    void Start()
    {
        persistenceManager = FindObjectOfType<PersistenceManager>();
  
        serverWorlds = null;
        
    }

    void Awake()
    {
      
    }
    
    
    public void FetchWorlds(Action<List<ServerWorld>> callback = null)
    {
        
        APIService.Instance.GetWorlds((dbWorlds) =>
        {
            Debug.Log("Received worlds");
            serverWorlds = dbWorlds;

            if (callback != null)
                callback(dbWorlds);
        });
        
    }

    public List<ServerWorld> ServerWorlds
    {
        get => serverWorlds;
        set => serverWorlds = value;
    }
    

    private SerializableWorld generateSerializableWorld(string name, int boardSize = 20)
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
        newWorld.CreationTime = DateTime.UtcNow.ToString();

        return newWorld;
    }
    public ServerWorld CreateWorld(string name, int boardSize = 20)
    {
    
        SerializableWorld newWorld = generateSerializableWorld(name);
        
        
        ServerWorld serverWorld = new ServerWorld(newWorld);
        
        APIService.Instance.CreateWorld(serverWorld, worldId => { serverWorld.id = worldId; });

        if (ServerWorlds == null)
        {
            ServerWorlds = new List<ServerWorld>();
        }
        
        
        ServerWorlds.Add(serverWorld);

        return serverWorld;

    }

  
}
