using System;
using System.Linq;
using UnityEngine;
using World.Resource;

namespace World.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] public EntityHelper entityHelper;
        public virtual EntityType Type { get; }
        
        public virtual EntityUpgradeInformation UpgradeInformation { get; }

        public EntityStats Stats => GetEntityStats();

        // level starts at 1 currently- upgradable 3 times
        public int Level { get; set; } = 1;
        [SerializeField] public int maxLevel = 3;


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
                
                // Get the max height of this gameobject
                var renderers = GetComponentsInChildren<Renderer>();
                var y = renderers.Select(r => r.bounds.size.y).Concat(new[] {10f}).Max();

                // Set the position and scale to match this gameobject
                box.transform.localScale = new Vector3(10, y, 10);
                box.transform.SetParent(transform, false);
            }
            
            // Change the color
            var color = canBePlaced
                ? new Color(255, 56, 0, 100)
                : new Color(0, 255, 87, 100);

            box.GetComponent<Renderer>().material.color = color;
        }

        public void HideOutline()
        {
            if (box == null) return;
            Destroy(box);
            box = null;
        }
        
        
        protected int GetUpgradeCost()
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


        private EntityStats GetEntityStats()
        {
            switch (Level)
            {
                case 1:
                    return UpgradeInformation.levelOne;
                case 2:
                    Debug.Log("level two");
                    return UpgradeInformation.levelTwo;
                case 3:
                    return UpgradeInformation.levelThree;

            }
            return UpgradeInformation.levelOne;
        }
    }
    
    
}