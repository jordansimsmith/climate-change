using System;
using World.Entities;

namespace Persistence.Serializables
{
    [Serializable]
    public class SerializableEntity
    {
        public SerializableEntity()
        {
        }

        public SerializableEntity(Entity entity)
        {
            EntityType = entity.Type;
            Level = entity.Level;
        }
            
        public EntityType EntityType { get; set; }
        public int Level { get; set;}
    }
}