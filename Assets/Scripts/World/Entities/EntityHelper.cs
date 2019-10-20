using System;
using HUD;
using UnityEngine;
using World.Resource;

namespace World.Entities {
    [CreateAssetMenu]
    public class EntityHelper : ScriptableObject {
        [SerializeField] private ResourceSingleton resources;
        [SerializeField] private GameObject outlineCubePrefab;
        [SerializeField] private Material redTransparentMat;
        [SerializeField] private Material greenTransparentMat;
        
        private EntityPlacer entityPlacer;
        private GameBoard board;
        
//        public int townhallLevel = 1;
        

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
            resources.Money -= resources.Money >= cost ? cost : 0;
            return true;
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