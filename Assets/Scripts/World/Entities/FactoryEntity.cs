using UnityEngine;

namespace World.Entities
{
    public class FactoryEntity : Entity {
        [SerializeField] private EntityUpgradeInformation upgradeInformation;

        public override EntityUpgradeInformation UpgradeInformation => upgradeInformation;
        public override  EntityType Type => EntityType.Factory;
        
        public override void Construct() {
            base.Construct();
            entityHelper.IncreaseMoneyRate(Stats.money);
        }
        

        public override void Destruct() {
            base.Destruct();
            entityHelper.DecreaseMoneyRate(Stats.money);
        }

        public override bool Upgrade()
        {
            if (Level + 1 > maxLevel)
            {
                Debug.Log("reached level cap");
                return false;
            }

            int upgradeCost = GetUpgradeCost();
            if (entityHelper.UpgradeIfEnoughMoney(upgradeCost))
            {
                entityHelper.DecreaseMoneyRate(Stats.money);
                Level++;
                entityHelper.IncreaseMoneyRate(Stats.money);
                return true;
            }

            Debug.Log("not enough shmoneys");
            return false;
        }
    }
}