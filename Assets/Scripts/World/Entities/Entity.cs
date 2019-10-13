using UnityEngine;

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
                box.transform.SetParent(transform, false);
            }

            entityHelper.setOutlineColor(box, canBePlaced);
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
    }
}