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

        void Awake()
        {
            if (!created)
            {
                DontDestroyOnLoad(gameObject);
                created = true;
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
            
            string path = Path.Combine(Application.persistentDataPath, "worlds");
            Directory.CreateDirectory (path);
            path = Path.Combine(path, world.GetHashCode() + ".json");
            
            File.WriteAllText(path, serializedObject);
        }

    

        public SerializableWorld SelectedWorld
        {
            get => selectedWorld;
            set => selectedWorld = value;
        }
    }
}