using System;
using UnityEngine;

namespace World.Entities
{
  public class PowerStationEntity : Entity {
      [SerializeField] private EntityState state;
      [SerializeField] private EntityHelper entityHelper;
      
      public override EntityState State => state;
      public override EntityType Type => EntityType.PowerStation;

      public override void Construct() {
          entityHelper.Construct(state);
      }

      public override void Destruct() {
          entityHelper.Destruct(state);
      }

  }

}
