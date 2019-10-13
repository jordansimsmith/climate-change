using System;
using UnityEngine;

namespace World.Entities
{
  public class PowerStationEntity : Entity {
      [SerializeField] private EntityUpgradeInformation upgradeInformation;

      public override EntityUpgradeInformation UpgradeInformation => upgradeInformation;
      
      public override EntityType Type => EntityType.PowerStation;
  }

}
