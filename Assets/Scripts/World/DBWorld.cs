using Persistence.Serializables;

namespace World
{
    public struct DbWorld
    {
        public string authId;
        public string id;
        public string shareCode;
        public SerializableWorld world;
    }
}