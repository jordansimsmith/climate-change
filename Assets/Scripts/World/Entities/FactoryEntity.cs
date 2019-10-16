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

        public override bool Upgrade() {
            if (!base.Upgrade()) return false;
            entityHelper.DecreaseMoneyRate(base.GetEntityStats(Level - 1).cost);
            entityHelper.IncreaseMoneyRate(Stats.money);
            return true;
        }
    }
}