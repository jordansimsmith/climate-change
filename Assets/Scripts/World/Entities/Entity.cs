using UnityEngine;
using World.Resource;

namespace World.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] public EntityHelper entityHelper;
        public virtual EntityType Type { get; }
        public virtual EntityStats Stats { get; }
        public virtual EntityUpgradeCosts UpgradeCosts { get; }

        public int Level { get; set; } = 1;
        // max level can be overwritten
        public virtual int MaxLevel { get; } = 3;

        
        public abstract void Construct();
        public abstract void Destruct();

        // upgrade method can be overwritten to provide upgrade criteria i.e electricity must be > X
        public virtual void Upgrade()    
        {
            if (Level + 1 > MaxLevel)
            {
                Debug.Log("reached level cap");
            }
            else
            {
                Debug.Log(GetUpgradeCost());
                Level++;
            }
        }

        public int GetUpgradeCost()
        {
            switch (Level)
            {
                case 1:
                    return UpgradeCosts.levelOne;
                case 2:
                    return UpgradeCosts.levelTwo;
                case 3:
                    return UpgradeCosts.levelThree;
                default:
                    return 0;
            }
            
        }
    }
}