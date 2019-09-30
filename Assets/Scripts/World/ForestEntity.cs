using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace World {
    public class ForestEntity : Entity {
        [SerializeField] private Transform[] treeTransforms;

        private void Start() {
            foreach (var tree in treeTransforms) {
                tree.localPosition = new Vector3(Random.Range(0, Tile.Size.x), 0, Random.Range(0, Tile.Size.z));
            }
        }
    }
}