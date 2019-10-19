using UnityEngine;

namespace World.Entities
{
  public class HouseEntity : Entity
  {
      [SerializeField] private EntityUpgradeInfo upgradeInfo;

      public override EntityUpgradeInfo UpgradeInfo => upgradeInfo;

      public override EntityType Type => EntityType.House;


  }


}
