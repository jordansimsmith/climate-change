using System;
using UnityEngine;

namespace World.Entities
{
  public class PowerStationEntity : Entity {
      [SerializeField] private EntityState state;
      [SerializeField] private EntityHelper entityHelper;
      
      private const float WaitTime = 1.0f;
      private float secondTicks = 0;

      public override EntityState State => state;
      public override EntityType Type => EntityType.PowerStation;

      public override void Construct() {
          entityHelper.Construct(state);
      }

      public override void Destruct() {
          entityHelper.Destruct(state);
      }

      public void Update() {
          secondTicks += Time.deltaTime;
          if (secondTicks > WaitTime) {
              entityHelper.SendMoney(state.money);
              secondTicks -= WaitTime;
          }
      }
  }

}
