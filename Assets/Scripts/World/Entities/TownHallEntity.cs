using UnityEngine;

namespace World.Entities
{
  public class TownHallEntity : Entity
  {
      [SerializeField] private EntityStats stats;
      [SerializeField] private EntityHelper entityHelper;

      public override EntityStats Stats => stats;
      public override EntityType Type => EntityType.TownHall;

      public override void Construct() {
          entityHelper.Construct(stats);
      }

      public override void Destruct() {
          entityHelper.Destruct(stats);
      }
  }

}
