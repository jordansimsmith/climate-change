using System.IO;
using Newtonsoft.Json;
using Persistence.Serializables;
using UnityEngine;
using World;
using World.Resource;
using World.Tiles;

namespace Persistence
{
    public class PersistenceManager : MonoBehaviour
    {

        private static JsonSerializerSettings serializationSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Ignore
        };

        private static bool created = false;
        private SerializableWorld selectedWorld;

        private string worldsDirectoryPath;

        void Awake()
        {
            if (!created)
            {
                DontDestroyOnLoad(gameObject);
                created = true;
                // Ensure Worlds directory is created when game loads.
                worldsDirectoryPath = Path.Combine(Application.persistentDataPath, "worlds");
                Directory.CreateDirectory (worldsDirectoryPath);
            }
            else
            {
                // Navigated back to main menu so we can reset "selected world"
                SelectedWorld = null;
                Destroy(gameObject);
            }
        }

        public void SaveGameState(SerializableWorld world)
        {

            string serializedObject = JsonConvert.SerializeObject(world, serializationSettings);
            Debug.Log(serializedObject);
            
      
           
            string path = Path.Combine(WorldsDirectoryPath, world.GetHashedId() + ".json");
            
            File.WriteAllText(path, serializedObject);
        }
        
        public void DeleteWorld(SerializableWorld world)
        {
            string path = Path.Combine(worldsDirectoryPath, world.GetHashedId() + ".json");
            Debug.Log("Try delete "+path);
            File.Delete(path);
        }

    

        public SerializableWorld SelectedWorld
        {
            get => selectedWorld;
            set => selectedWorld = value;
        }

        public string WorldsDirectoryPath => worldsDirectoryPath;
    }
}