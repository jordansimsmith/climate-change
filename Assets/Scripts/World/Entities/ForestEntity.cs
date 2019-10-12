using UnityEngine;
using Random = UnityEngine.Random;

namespace World.Entities
{
    public class ForestEntity : Entity
    {
        [SerializeField] private Transform[] treeTransforms;
        [SerializeField] private EntityUpgradeCosts upgradeCosts;
        [SerializeField] private EntityUpgradeInformation upgradeInformation;

        public override EntityUpgradeInformation UpgradeInformation => upgradeInformation;
        public override EntityUpgradeCosts UpgradeCosts => upgradeCosts;
        public override EntityType Type => EntityType.Forest;

        public override void Construct() {
            entityHelper.Construct(Stats);
        }

        public override void Destruct() {
            entityHelper.Destruct(Stats);
        }


        private void Start() {
            foreach (var tree in treeTransforms) {
                tree.localPosition =
                    new Vector3(Random.Range(0, Tiles.Tile.Size.x), tree.localPosition.y, Random.Range(0, Tiles.Tile.Size.z));
            }
        }
    }
}