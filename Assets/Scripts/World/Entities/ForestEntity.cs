using UnityEngine;
using Random = UnityEngine.Random;

namespace World.Entities
{
    public class ForestEntity : Entity
    {
        [SerializeField] private Transform[] treeTransforms;
        [SerializeField] private EntityUpgradeInformation upgradeInformation;

        public override EntityUpgradeInformation UpgradeInformation => upgradeInformation;
        public override EntityType Type => EntityType.Forest;
        private void Start() {
            foreach (var tree in treeTransforms) {
                tree.localPosition =
                    new Vector3(Random.Range(0, Tiles.Tile.Size.x), tree.localPosition.y, Random.Range(0, Tiles.Tile.Size.z));
            }
        }
    }
}