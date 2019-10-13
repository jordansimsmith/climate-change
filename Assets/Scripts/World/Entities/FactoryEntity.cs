using UnityEngine;

namespace World.Entities
{
    public class FactoryEntity : Entity {
        [SerializeField] private EntityUpgradeInformation upgradeInformation;

        public override EntityUpgradeInformation UpgradeInformation => upgradeInformation;
        public override  EntityType Type => EntityType.Factory;
        
        public override void Construct() {
            entityHelper.Construct(Stats);
            entityHelper.increaseMoneyRate(Stats.money);
        }
        

        public override void Destruct() {
            entityHelper.Destruct(Stats);
            entityHelper.decreaseMoneyRate(Stats.money);
        }

        public override void Upgrade()
        {
            if (Level + 1 > maxLevel)
            {
                Debug.Log("reached level cap");
                return;
            }

            int upgradeCost = GetUpgradeCost();
            if (entityHelper.UpgradeIfEnoughMoney(upgradeCost))
            {
                entityHelper.decreaseMoneyRate(Stats.money);
                Level++;
                entityHelper.increaseMoneyRate(Stats.money);
            }
            else
            {
                Debug.Log("not enough shmoneys");
            }
            
        }
    }
}