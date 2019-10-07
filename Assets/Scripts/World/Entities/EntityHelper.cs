using UnityEngine;
using World.Resource;

namespace World.Entities {
    [CreateAssetMenu]
    public class EntityHelper : ScriptableObject {

        [SerializeField] private ResourceSingleton resources;
        
        public void Construct(EntityStats res) {
          resources.Money -= res.cost;
          resources.Food.CurAmount += res.food;
          resources.Environment.CurAmount += res.environment;
          resources.Power.CurAmount += res.power;
          resources.Shelter.CurAmount += res.shelter;
        }

        public void Destruct(EntityStats res) {
          resources.Food.CurAmount -= res.food;
          resources.Environment.CurAmount -= res.environment;
          resources.Power.CurAmount -= res.power;
          resources.Shelter.CurAmount -= res.shelter;
        }

        public void increaseMoneyRate(int amount) {
            resources.MoneyRate += amount;
        }
        
        public void decreaseMoneyRate(int amount) {
            resources.MoneyRate -= amount;
        }
    }
}