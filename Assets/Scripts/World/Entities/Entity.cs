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

        // level starts at 0 currently- upgradable 3 times
        public int Level { get; set; }
        public int MaxLevel { get; } = 3;

        
        public abstract void Construct();
        public abstract void Destruct();

        // upgrade method can be overwritten to provide upgrade criteria i.e electricity must be > 
        // base functionality checks base level + cost
        public virtual void Upgrade()    
        {
            if (Level + 1 > MaxLevel)
            {
                Debug.Log("reached level cap");
            }
            else
            {
                int upgradeCost = GetUpgradeCost();
                if (entityHelper.UpgradeIfEnoughMoney(upgradeCost))
                {
                    Level++;
                }
                else
                {
                    Debug.Log("not enough shmoneys");
                }
            }
        }

        private int GetUpgradeCost()
        {
            switch (Level + 1)
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