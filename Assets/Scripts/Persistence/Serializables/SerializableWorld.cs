using System;
using System.Security.Cryptography;
using System.Text;
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
            ResourceData = new SerializableResource(resources);
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
        
        public string Name { get; set; }
        
        public string CreationTime { get; set; }

        public SerializableTile[,] WorldData { get; set; }
        public SerializableResource ResourceData { get; set; }

        public string GetHashedId()
        {
            return GetHashString(Name + CreationTime);
        }
        
        public static byte[] GetHash(string inputString)
        {
            HashAlgorithm algorithm = SHA256.Create();
            return algorithm.ComputeHash(Encoding.UTF8.GetBytes(inputString));
        }

        public static string GetHashString(string inputString)
        {
            StringBuilder sb = new StringBuilder();
            foreach (byte b in GetHash(inputString))
                sb.Append(b.ToString("X2"));

            return sb.ToString();
        }
    }


}