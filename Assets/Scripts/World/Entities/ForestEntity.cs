using UnityEngine;
using Random = UnityEngine.Random;

namespace World.Entities
{
    public class ForestEntity : Entity
    {
        [SerializeField] private EntityStats stats;
        [SerializeField] private Transform[] treeTransforms;
        [SerializeField] private EntityUpgradeCosts upgradeCosts;
        public override EntityUpgradeCosts UpgradeCosts => upgradeCosts;
        public override EntityStats Stats => stats;
        public override EntityType Type => EntityType.Forest;

        public override void Construct() {
            entityHelper.Construct(stats);
        }

        public override void Destruct() {
            entityHelper.Destruct(stats);
        }


        private void Start() {
            foreach (var tree in treeTransforms) {
                tree.localPosition =
                    new Vector3(Random.Range(0, Tiles.Tile.Size.x), tree.localPosition.y, Random.Range(0, Tiles.Tile.Size.z));
            }
        }
    }
}