using System;
using UnityEngine;

namespace World.Entities
{
  public class PowerStationEntity : Entity {
      [SerializeField] private EntityUpgradeInfo upgradeInfo;

      public override EntityUpgradeInfo UpgradeInfo => upgradeInfo;
      
      public override EntityType Type => EntityType.PowerStation;
  }

}
