using UnityEngine;

namespace World.Entities {
    public class SolarPanelEntity : Entity{
      [SerializeField] private EntityUpgradeInfo upgradeInfo;
      public override EntityUpgradeInfo UpgradeInfo => upgradeInfo;
      public override EntityType Type => EntityType.SolarPanel;
    }
}