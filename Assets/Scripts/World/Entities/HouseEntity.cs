using UnityEngine;

namespace World.Entities
{
  public class HouseEntity : Entity
  {
      [SerializeField] private EntityStats stats;
      [SerializeField] private EntityHelper entityHelper;

      public override EntityStats Stats => stats;
      public override EntityType Type => EntityType.House;

      public override void Construct() {
          entityHelper.Construct(stats);
      }

      public override void Destruct() {
          entityHelper.Destruct(stats);
      }


  }


}
