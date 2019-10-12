using UnityEngine;

namespace World.Entities
{
  public class HouseEntity : Entity
  {
      [SerializeField] private EntityUpgradeInformation upgradeInformation;

      public override EntityUpgradeInformation UpgradeInformation => upgradeInformation;

      public override EntityType Type => EntityType.House;


  }


}
