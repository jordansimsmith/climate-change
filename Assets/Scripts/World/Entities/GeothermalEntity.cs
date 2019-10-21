using UnityEngine;

namespace World.Entities {
    public class GeothermalEntity : Entity{
        
      [SerializeField] private EntityUpgradeInfo upgradeInfo;
      public override EntityUpgradeInfo UpgradeInfo => upgradeInfo;
      public override EntityType Type => EntityType.Geothermal;
        
    }
}