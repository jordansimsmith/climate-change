using System;
using HUD;
using UnityEngine;
using World.Resource;

namespace World.Entities {
    /// <summary>
    /// Provides helper functionality common among all entities.
    /// This is useful because serialized fields can be reused.
    /// </summary>
    [CreateAssetMenu]
    public class EntityHelper : ScriptableObject {
        [SerializeField] private ResourceSingleton resources;
        [SerializeField] private GameObject outlineCubePrefab;
        [SerializeField] private Material redTransparentMat;
        [SerializeField] private Material greenTransparentMat;
        
        private EntityPlacer entityPlacer;
        private GameBoard board;

        public void Construct(EntityStats res) {
          resources.Money -= res.cost;
        }

        public bool UpgradeIfEnoughMoney(int cost)
        {
            if (resources.Money - cost > 0)
            {
                resources.Money -= cost;
                return true;
            }

            return false;
        }

        public bool IsTownhallLevelEnoughForUpgrade(Entity entity) {
            if (board == null) {
                board = GameObject.FindGameObjectWithTag("InGameBoard").GetComponent<GameBoard>();
            }
            if (entity.Type == EntityType.TownHall) {
                return true;
            }
            return entity.Level < board.GetTownHallLevel();
        }
        

        public bool ResearchIfEnoughMoney(ResearchOption research) {
            if (research.isResearched) return false;
            int cost = research.ResearchDiff.cost;
            if (resources.Money >= cost)
            {
                resources.Money -= cost;
                return true;
            } else
            {
                return false;
            }
        }
        

        public void Destruct(EntityStats res) {
        }

        public GameObject CreateOutlineCube()
        {
            return Instantiate(outlineCubePrefab);
        }

        public void SetOutlineColor(GameObject cube, bool canBePlaced)
        {
            cube.GetComponent<Renderer>().material = canBePlaced
                ? greenTransparentMat
                : redTransparentMat;
        }

        public EntityPlacerMode GetEntityPlacerMode()
        {
            if (entityPlacer == null)
            {
                entityPlacer = FindObjectOfType<EntityPlacer>();
            }
            return entityPlacer.Mode;
        }
    }
}