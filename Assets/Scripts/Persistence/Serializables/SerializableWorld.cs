using System;
using World.Resource;
using World.Tiles;

namespace Persistence.Serializables
{
    [Serializable]
    public class SerializableWorld
    {
            
        public SerializableWorld()
        {
        }
            
        public SerializableWorld(Tile[,] tiles, ResourceSingleton resources)
        {
            ResourceData = resources;
            WorldData = new SerializableTile[tiles.GetLength(0), tiles.GetLength(1)];
                
            for (int i = 0; i < tiles.GetLength(0); i++)
            {
                for (int j = 0; j < tiles.GetLength(1); j++)
                {
                    Tile tile = tiles[i, j];
                    WorldData[i, j] =  new SerializableTile(tile);
                       
                }
            }
                
        }

        public SerializableTile[,] WorldData { get; set; }
        public ResourceSingleton ResourceData { get; set; }

     
    }


}