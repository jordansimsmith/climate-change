using UnityEngine;

namespace World.Entities
{
  public class TownHallEntity : Entity
  {
      [SerializeField] private EntityUpgradeInformation upgradeInformation;

      public override EntityUpgradeInformation UpgradeInformation => upgradeInformation;
      public override EntityType Type => EntityType.TownHall;

  }

}
