using UnityEngine;

namespace World.Entities
{
    public class FarmEntity :Entity {
        [SerializeField] private EntityUpgradeInformation upgradeInformation;

        public override EntityUpgradeInformation UpgradeInformation => upgradeInformation;
        public override  EntityType Type => EntityType.Farm;
        
    }
}