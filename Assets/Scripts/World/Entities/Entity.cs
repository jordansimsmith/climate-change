using UnityEngine;
using World.Resource;

namespace World.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] public EntityHelper entityHelper;
        public virtual EntityType Type { get; }

        public EntityStats Stats => GetEntityStats();

        public virtual EntityUpgradeInformation UpgradeInformation { get; }
        public virtual EntityUpgradeCosts UpgradeCosts { get; }

        // level starts at 1 currently- upgradable 3 times
        public int Level { get; set; } = 1;
        [SerializeField] public int maxLevel = 3;

        
        public abstract void Construct();
        public abstract void Destruct();

        // upgrade method can be overwritten to provide upgrade criteria i.e electricity must be > 
        // base functionality checks base level + cost
        public virtual void Upgrade()    
        {
            if (Level + 1 > maxLevel)
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

        protected int GetUpgradeCost()
        {
            switch (Level + 1)
            {
                case 2:
                    return UpgradeInformation.levelTwo.cost;
                case 3:
                    return UpgradeInformation.levelThree.cost;
                default:
                    return UpgradeInformation.levelOne.cost;
            }
            
        }


        private EntityStats GetEntityStats()
        {
            switch (Level)
            {
                case 1:
                    return UpgradeInformation.levelOne;
                case 2:
                    Debug.Log("level two");
                    return UpgradeInformation.levelTwo;
                case 3:
                    return UpgradeInformation.levelThree;

            }

            return UpgradeInformation.levelOne;
        }
    }
    
    
}