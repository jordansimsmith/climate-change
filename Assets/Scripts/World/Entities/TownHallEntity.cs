using UnityEngine;

namespace World.Entities
{
  public class TownHallEntity : Entity
  {
      [SerializeField] private EntityUpgradeCosts upgradeCosts;
      [SerializeField] private EntityUpgradeInformation upgradeInformation;

      public override EntityUpgradeInformation UpgradeInformation => upgradeInformation;
      public override EntityUpgradeCosts UpgradeCosts => upgradeCosts;

      public override EntityType Type => EntityType.TownHall;

      public override void Construct() {
          entityHelper.Construct(Stats);
      }

      public override void Destruct() {
          entityHelper.Destruct(Stats);
      }
  }

}
