using UnityEngine;

namespace World.Entities
{
    public class FarmEntity :Entity {
        [SerializeField] private EntityUpgradeInfo upgradeInfo;

        public override EntityUpgradeInfo UpgradeInfo => upgradeInfo;
        public override  EntityType Type => EntityType.Farm;
        
    }
}