using UnityEngine;

namespace World.Entities
{
    public class FarmEntity :Entity {
        [SerializeField] private EntityUpgradeCosts upgradeCosts;
        [SerializeField] private EntityUpgradeInformation upgradeInformation;

        public override EntityUpgradeInformation UpgradeInformation => upgradeInformation;
        public override EntityUpgradeCosts UpgradeCosts => upgradeCosts;
        public override  EntityType Type => EntityType.Farm;
        
    }
}