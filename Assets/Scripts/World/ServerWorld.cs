using Persistence.Serializables;

namespace World
{
    /**
        Serializable World alongside serverside metadata.
     */
    public class ServerWorld
    {
        public string authId;
        public string id;
        public string shareCode;
        public SerializableWorld world;

        public ServerWorld(SerializableWorld world)
        {
            this.world = world;
            authId = null;
            id = null ;
            shareCode = null;
        }
    }
}