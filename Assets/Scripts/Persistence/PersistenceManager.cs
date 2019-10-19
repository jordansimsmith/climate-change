using System.IO;
using DefaultNamespace;
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
        private ServerWorld selectedWorld;

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

        public void SaveGameState(ServerWorld world)
        {

            string serializedObject = JsonConvert.SerializeObject(world, serializationSettings);
            Debug.Log(serializedObject);
            
            APIService.Instance.UpdateWorld(world, world.id);
        }
        
        public void DeleteWorld(ServerWorld serverWorld)
        {

            Debug.Log("Try delete "+serverWorld.world.Name);
            APIService.Instance.DeleteWorld(serverWorld.id);
        }

    

        public ServerWorld SelectedWorld
        {
            get => selectedWorld;
            set => selectedWorld = value;
        }

        public string WorldsDirectoryPath => worldsDirectoryPath;
    }
}