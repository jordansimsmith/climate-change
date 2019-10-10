using UnityEngine;
using World.Resource;

namespace World.Entities
{
    public abstract class Entity : MonoBehaviour {
        public virtual EntityType Type { get; }
        public virtual EntityStats Stats { get; }

        public virtual int level { get; } 
        public abstract void Construct();
        public abstract void Destruct();
        
        
    }
}