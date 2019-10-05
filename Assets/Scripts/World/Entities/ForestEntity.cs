using UnityEngine;
using Random = UnityEngine.Random;

namespace World.Entities
{
    public class ForestEntity : Entity
    {
        [SerializeField] private EntityState state;
        [SerializeField] private EntityHelper entityHelper;
        [SerializeField] private Transform[] treeTransforms;
        public override EntityState State => state;
        public override EntityType Type => EntityType.Forest;

        public override void Construct() {
            entityHelper.Construct(state);
        }

        public override void Destruct() {
            entityHelper.Destruct(state);
        }


        private void Start() {
            foreach (var tree in treeTransforms) {
                tree.localPosition =
                    new Vector3(Random.Range(0, Tiles.Tile.Size.x), 0, Random.Range(0, Tiles.Tile.Size.z));
            }
        }
    }
}