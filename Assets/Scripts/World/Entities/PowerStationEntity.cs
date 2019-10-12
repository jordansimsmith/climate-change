using System;
using UnityEngine;

namespace World.Entities
{
  public class PowerStationEntity : Entity {
      [SerializeField] private EntityUpgradeCosts upgradeCosts;
      [SerializeField] private EntityUpgradeInformation upgradeInformation;

      public override EntityUpgradeInformation UpgradeInformation => upgradeInformation;
      public override EntityUpgradeCosts UpgradeCosts => upgradeCosts;
      
      public override EntityType Type => EntityType.PowerStation;

      public override void Construct() {
          entityHelper.Construct(Stats);
      }

      public override void Destruct() {
          entityHelper.Destruct(Stats);
      }

  }

}
