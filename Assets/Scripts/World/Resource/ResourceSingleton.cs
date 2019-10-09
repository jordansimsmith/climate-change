using UnityEngine;
using World.Entities;

namespace World.Resource {
    [CreateAssetMenu]
    public class ResourceSingleton : ScriptableObject {
        [SerializeField] public EntityStats totalDemand = new EntityStats();
        
        [SerializeField] public EntityStats totalSupply = new EntityStats();

        public int Population;

        public int Money;
        public int MoneyRate;
    }
}