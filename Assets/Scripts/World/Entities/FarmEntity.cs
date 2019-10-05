using UnityEngine;

namespace World.Entities
{
    public class FarmEntity :Entity {
        [SerializeField] private EntityState state;
        [SerializeField] private EntityHelper entityHelper;

        public override  EntityState State => state;
        public override  EntityType Type => EntityType.Farm;
        
        public override void Construct() {
            entityHelper.Construct(state);
        }

        public override void Destruct() {
            entityHelper.Destruct(state);
        }
    }
}