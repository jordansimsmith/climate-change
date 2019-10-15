using System;
using HUD;
using UnityEngine;

namespace World.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] protected EntityHelper entityHelper;
        [SerializeField] protected int maxLevel = 3;
        [SerializeField] protected GameObject[] modelForLevel;
        
        public virtual EntityType Type { get; }
        public virtual EntityUpgradeInformation UpgradeInformation { get; }
        
        public EntityStats Stats => GetEntityStats();
        public int Level { get; set; } = 1; // level starts at 1 currently- upgradable 3 times
        public int MaxLevel => maxLevel;

        private EntitySideBarController sideBarController;

        private void Awake() {
            sideBarController = FindObjectOfType<EntitySideBarController>();
        }

        public virtual void Construct()
        {
            entityHelper.Construct(Stats);
        }

        public virtual void Destruct()
        {
            entityHelper.Destruct(Stats);
        }

        // upgrade method can be overwritten to provide upgrade criteria i.e electricity must be > 
        // base functionality checks base level + cost
        public virtual bool Upgrade()
        {
            if (Level + 1 > maxLevel)
            {
                Debug.Log("reached level cap");
                return false;
            }

            int upgradeCost = GetUpgradeCost();
            if (entityHelper.UpgradeIfEnoughMoney(upgradeCost))
            {
                Level++;
                Debug.Log("level went up to " + Level);
                for (int i = 0; i < modelForLevel.Length; i++) {
                    modelForLevel[i].SetActive(i == Level - 1);
                }
                return true;
            }

            Debug.Log("not enough shmoneys");
            return false;
        }

        private GameObject box;

        public void ShowOutline(bool canBePlaced)
        {
            if (box == null)
            {
                // Get an instance of outline cube
                box = entityHelper.CreateOutlineCube();
                box.transform.SetParent(transform, false);
            }

            entityHelper.SetOutlineColor(box, canBePlaced);
        }

        public void HideOutline()
        {
            if (box == null) return;
            Destroy(box);
            box = null;
        }

        public int GetUpgradeCost()
        {
            switch (Level + 1)
            {
                case 2:
                    return UpgradeInformation.levelTwo.cost;
                case 3:
                    return UpgradeInformation.levelThree.cost;
                default:
                    return UpgradeInformation.levelOne.cost;
            }
        }

        public bool IsMaxLevel()
        {
            return Level == maxLevel;
        }

        private EntityStats GetEntityStats()
        {
            switch (Level)
            {
                case 1:
                    return UpgradeInformation.levelOne;
                case 2:
                    return UpgradeInformation.levelTwo;
                case 3:
                    return UpgradeInformation.levelThree;
            }

            return UpgradeInformation.levelOne;
        }
        
        public void OnMouseDown()
        {
            sideBarController.ShowSideBar(this, true);
        }
    }
}