using UnityEngine;
using World.Resource;

namespace World.Entities {
    [CreateAssetMenu]
    public class EntityHelper : ScriptableObject {

        [SerializeField] private ResourceSingleton resources;

        public void Construct(EntityStats res) {
          resources.Money -= res.cost;
        }

        public bool Upgrade(int cost)
        {
            if (resources.Money - cost > 0)
            {
                resources.Money -= cost;
                return true;
            }
            else
            {
                return false;
            }
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