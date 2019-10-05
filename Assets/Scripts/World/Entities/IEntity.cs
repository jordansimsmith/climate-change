using System.Runtime.Remoting.Messaging;
using World.Resource;

namespace World.Entities {
    public interface IEntity {
        EntityState Resources { get; }
        EntityType Type { get; }
        void Construct();
        void Destruct();
    }
}