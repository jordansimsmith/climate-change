using UnityEngine;

namespace World.Entities
{
  public class TownHallEntity : Entity
  {
      [SerializeField] private EntityUpgradeInfo upgradeInfo;

      public override EntityUpgradeInfo UpgradeInfo => upgradeInfo;
      public override EntityType Type => EntityType.TownHall;

  }

}
