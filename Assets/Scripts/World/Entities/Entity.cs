using System;
using HUD;
using UnityEngine;
using World.Tiles;
using UnityEngine.EventSystems;

namespace World.Entities
{
    public abstract class Entity : MonoBehaviour
    {
        [SerializeField] protected EntityHelper entityHelper;
        [SerializeField] protected GameObject[] modelForLevel;

        public virtual EntityType Type { get; }
        public virtual EntityUpgradeInfo UpgradeInfo { get; }

        void Start()
        {
            RefreshModelForLevel();
        }

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

            if (!entityHelper.IsTownhallLevelEnoughForUpgrade(this))
            {
                return false;
            }

            int upgradeCost = GetUpgradeCost();
            if (entityHelper.UpgradeIfEnoughMoney(upgradeCost))
            {
                Level++;

                if (Type == EntityType.TownHall && Level == 2)
                {
                    string[] auditDialogue = EwanAuditGenerator.Instance.GenerateAuditText();
                    SimpleDialogueManager.Instance.SetCurrentDialogue(auditDialogue, "Ewan    ");
                }

                // Switch out the model when upgrading to next level
                RefreshModelForLevel();

                return true;
            } else {
                // Display out of money warning
                var HUD = GameObject.Find("HUD");
                if (HUD)    {
                    var warning = HUD.transform.Find("OutOfMoneyWarning");
                    if (warning)    {
                        warning.position = Input.mousePosition;
                        warning.gameObject.SetActive(true);
                        Invoke("HideCostWarning", 4);
                    }
                }
            }

            Debug.Log("not enough shmoneys");
            return false;
        }

        public void RefreshModelForLevel()
        {
            for (int i = 0; i < modelForLevel.Length; i++)
            {
                if (i == Level - 1)
                {
                    modelForLevel[i].SetActive(true);
                    var renderers = modelForLevel[i].GetComponentsInChildren<Renderer>();
                    float maxY = 0;
                    foreach (var renderer in renderers)
                    {
                        maxY = Math.Max(maxY, renderer.bounds.size.y);
                    }
                    var collider = GetComponent<BoxCollider>();
                    collider.size = new Vector3(collider.size.x, maxY, collider.size.z);
                }
                else
                {
                    modelForLevel[i].SetActive(false);
                }
            }
           
        }

        public virtual bool Research(ResearchOption research)
        {
            if (entityHelper.ResearchIfEnoughMoney(research))
            {
                research.isResearched = true;
                return true;
            } else {
                // Display out of money warning
                var HUD = GameObject.Find("HUD");
                if (HUD)    {
                    var warning = HUD.transform.Find("OutOfMoneyWarning");
                    if (warning)    {
                        warning.position = Input.mousePosition;
                        warning.gameObject.SetActive(true);
                        Invoke("HideCostWarning", 4);
                    }
                }
            }

            return false;
        }

        public void HideCostWarning()
        {
            // Hide out of money warning
            var warning = GameObject.Find("OutOfMoneyWarning");
            if (warning)    {
                warning.SetActive(false);
            }
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

        private void OnMouseEnter()
        {
            var parent = transform.parent;
            if (parent == null)
            {
                return;
            }

            parent.GetComponent<Tile>().ShowHighlight();
        }

        private void OnMouseExit()
        {
            var parent = transform.parent;
            if (parent == null)
            {
                return;
            }

            parent.GetComponent<Tile>().HideHighlight();
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
                if (Type != EntityType.TownHall)
                {
                    var parent = transform.parent;
                    var tile = parent.gameObject.GetComponent<Tile>();
                    tile.Entity = null;
                    tile.HideHighlight();
                }
                
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