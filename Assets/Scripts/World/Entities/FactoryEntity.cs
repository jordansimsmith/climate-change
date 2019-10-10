using UnityEngine;

namespace World.Entities
{
    public class FactoryEntity : Entity {
        [SerializeField] private EntityStats stats;
        [SerializeField] private EntityHelper entityHelper;
        private int level { get; set; } = 1;
        public override  EntityStats Stats => stats;
        public override  EntityType Type => EntityType.Factory;
        
        public override void Construct() {
            entityHelper.Construct(stats);
            entityHelper.increaseMoneyRate(stats.money);

            Transform t = GetComponent<Transform>();
            Vector3 position = t.localScale;
            position.y = 1.5f;
            t.localScale = position;
        }

        public override void Destruct() {
            entityHelper.Destruct(stats);
            entityHelper.decreaseMoneyRate(stats.money);
        }

    }
}