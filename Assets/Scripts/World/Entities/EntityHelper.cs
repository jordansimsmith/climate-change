using UnityEngine;
using World.Resource;

namespace World.Entities {
    [CreateAssetMenu]
    public class EntityHelper : ScriptableObject {

        [SerializeField] private ResourceSingleton resources;
        
        public void Construct(EntityStats res) {
          resources.Money -= res.cost;
        }

        public void Destruct(EntityStats res) {
        }

        public void increaseMoneyRate(int amount) {
            resources.MoneyRate += amount;
        }
        
        public void decreaseMoneyRate(int amount) {
            resources.MoneyRate -= amount;
        }
    }
}