using HUD;
using Tutorial;
using UnityEngine;
using UnityEngine.EventSystems;

namespace World.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] protected EntityHelper entityHelper;
        [SerializeField] protected GameObject[] modelForLevel;

        public virtual EntityType Type { get; }
        public virtual EntityUpgradeInfo UpgradeInfo { get; }

        public EntityStats Stats
        {
            get
            {
                //Helper function to apply updates from research
                void Apply(ref EntityStats stats, EntityStats diff)
                {
                    stats.environment += diff.environment;
                    stats.food += diff.food;
                    stats.money += diff.money;
                    stats.power += diff.power;
                    stats.shelter += diff.shelter;
                }

                // Get the base stats for the current level
                var levelStat = UpgradeInfo.GetLevel(Level).BaseStats;

                // Apply updates to the base stats based on research options
                foreach (var researchOption in UpgradeInfo.GetLevel(Level).ResearchOptions)
                {
                    if (researchOption.isResearched)
                    {
                        Apply(ref levelStat, researchOption.ResearchDiff);
                    }
                }

                return levelStat;
            }
        }

        public int Level { get; set; } = 1; // level starts at 1 currently- upgradable 2 times
        public int MaxLevel => UpgradeInfo.NumberOfLevels;

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
            if (Level + 1 > MaxLevel)
            {
                Debug.Log("reached level cap");
                return false;
            }

            int upgradeCost = GetUpgradeCost();
            if (entityHelper.UpgradeIfEnoughMoney(upgradeCost))
            {
                Level++;

                // Switch out the model when upgrading to next level
                for (int i = 0; i < modelForLevel.Length; i++)
                {
                    modelForLevel[i].SetActive(i == Level - 1);
                }
                
                SimpleDialogueManager.Instance.SetCurrentDialogue(new [] {"Thats a bruh moment", "Bruhhh"}, "Bruh");

                return true;
            }

            Debug.Log("not enough shmoneys");
            return false;
        }

        public virtual bool Research(ResearchOption research)
        {
            if (entityHelper.ResearchIfEnoughMoney(research))
            {
                research.isResearched = true;
                return true;
            }

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
            return UpgradeInfo.GetLevel(Level + 1).BaseStats.cost;
        }

        public bool IsMaxLevel()
        {
            return Level == MaxLevel;
        }

        public void OnMouseDown()
        {
            // disable clicking through ui elements
            if (EventSystem.current.IsPointerOverGameObject())
            {
                return;
            }
            
            if (entityHelper.GetEntityPlacerMode() != EntityPlacerMode.DELETE)
            {
                UpgradeInformationController.Instance.ShowInformation(this);
            }

            if (entityHelper.GetEntityPlacerMode() == EntityPlacerMode.DELETE)
            {
                if (UpgradeInformationController.Instance.IsUpgradeInformationOpen())
                {
                    if (this == UpgradeInformationController.Instance.Entity)
                    {
                        UpgradeInformationController.Instance.CloseInformation();
                    }
                }
            }
        }
    }
}