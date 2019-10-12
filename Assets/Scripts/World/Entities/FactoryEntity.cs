using UnityEngine;

namespace World.Entities
{
    public class FactoryEntity : Entity {
        [SerializeField] private EntityStats stats;
        [SerializeField] private EntityUpgradeCosts upgradeCosts;
        public override EntityUpgradeCosts UpgradeCosts => upgradeCosts;

        private int level { get; set; } = 1;
        public override  EntityStats Stats => stats;
        public override  EntityType Type => EntityType.Factory;
        
        public override void Construct() {
            entityHelper.Construct(stats);
            entityHelper.increaseMoneyRate(stats.money);
        }

        public override void Destruct() {
            entityHelper.Destruct(stats);
            entityHelper.decreaseMoneyRate(stats.money);
        }

    }
}