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

        public void SaveGameState(Tile[,] tiles, ResourceSingleton resources)
        {
            SerializableWorld world = new SerializableWorld(tiles, resources);
            
            var serializeObject = JsonConvert.SerializeObject(world, serializationSettings);
            Debug.Log(serializeObject);
            
            File.WriteAllText(Application.persistentDataPath+"/world.json", serializeObject);
            LoadSerializedGameState();
        }

        public SerializableWorld LoadSerializedGameState()
        {
           string serializedWorld =  File.ReadAllText(Application.persistentDataPath + "/world.json");

           return JsonConvert.DeserializeObject<SerializableWorld>(serializedWorld);
        }

        public SerializableWorld SelectedWorld
        {
            get => selectedWorld;
            set => selectedWorld = value;
        }
    }
}