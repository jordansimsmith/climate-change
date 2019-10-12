using UnityEngine;
using World.Resource;

namespace World.Entities
{
    public abstract class Entity : MonoBehaviour {
        public virtual EntityType Type { get; }
        public virtual EntityStats Stats { get; }

        public int Level { get; set; } = 1;
        public virtual int MaxLevel { get; } = 3;
        
        public abstract void Construct();
        public abstract void Destruct();

        public void Upgrade()    
        {
            if (Level + 1 > MaxLevel)
            {
                Debug.Log("reached level cap");
            }
            else
            {
                Level++;
            }
        }
    }
}