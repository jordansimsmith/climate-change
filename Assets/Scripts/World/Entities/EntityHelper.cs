using UnityEngine;
using World.Resource;

namespace World.Entities {
    [CreateAssetMenu]
    public class EntityHelper : ScriptableObject {

        [SerializeField] private ResourceSingleton resources;
        
        public void Construct(EntityState res) {
          resources.Money.CurAmount -= res.cost;
          resources.Food.CurAmount += res.food;
          resources.Environment.CurAmount += res.environment;
          resources.Power.CurAmount += res.power;
          resources.Shelter.CurAmount += res.shelter;
        }

        public void Destruct(EntityState res) {
          resources.Food.CurAmount -= res.food;
          resources.Environment.CurAmount -= res.environment;
          resources.Power.CurAmount -= res.power;
          resources.Shelter.CurAmount -= res.shelter;
        }

        public void SendMoney(int amount) {
            resources.Money.CurAmount += amount;
        }
    }
}