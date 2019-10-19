using Persistence.Serializables;

namespace World
{
    public struct DbWorld
    {
        public string authId;
        public string id;
        public string shareCode;
        public SerializableWorld world;

        public DbWorld(SerializableWorld world)
        {
            this.world = world;
            authId = null;
            id = null ;
            shareCode = null;
        }
    }
}