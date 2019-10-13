using System;
using UnityEngine;
using UnityEngine.UI;
using World.Entities;
using Button = UnityEngine.UI.Button;

namespace HUD
{
    public class EntitySideBarController : MonoBehaviour
    {
        [SerializeField] private Text title;
        [SerializeField] private Text electricity;
        [SerializeField] private Text environment;
        [SerializeField] private Text food;
        [SerializeField] private Text shelter;
        [SerializeField] private Text income;
        [SerializeField] private GameController gameController;

        [SerializeField] private Button upgradeButton;
        [SerializeField] private Button closeUpgradePanelButton;
        private Text upgradeButtonText;
    
        private Entity entity;

        // Start is called before the first frame update
        void Start()
        {
            gameObject.SetActive(false);
            upgradeButtonText = upgradeButton.GetComponentInChildren<Text>();

        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
        public void ShowSideBar(Entity e, bool isUpgrade)
        {
            entity = e;
            gameObject.SetActive(true);
            if (isUpgrade)
            {
                UpdateUpgradeEntityInformation();
            }
            else
            {
                UpdateBuildEntityInformation();
            }
        }

        public void CloseSideBar()
        {
            entity = null;
            gameObject.SetActive(false);
        }

        private void UpdateBuildEntityInformation()
        {
            if (entity == null)
            {
                return;
            }

            title.text = entity.Type.ToString();
            
            EntityStats stats = entity.Stats;
            RefreshEntityStats(stats);
            
            upgradeButton.gameObject.SetActive(false);
            closeUpgradePanelButton.gameObject.SetActive(false);
        }
        
        private void UpdateUpgradeEntityInformation()
        {
            if (entity == null)
            {
                return;
            }
            
            String levelText = "Level " + entity.Level;
            title.text = entity.Type + " (" + levelText + ")";

            EntityStats stats = entity.Stats;
            RefreshEntityStats(stats);

            if (entity.isMaxLevel())
            {
                DisableUpgradeButton();
            }
            else
            {
                EnableUpgradeButton();
            }
            
            upgradeButton.gameObject.SetActive(true);
            closeUpgradePanelButton.gameObject.SetActive(true);
        }

        public void RefreshEntityStats(EntityStats stats)
        {
            electricity.text = "Power: " + stats.power;
            environment.text = "Environment: " + stats.environment;
            food.text = "Food: " + stats.food;
            shelter.text = "Shelter: " + stats.shelter;
            income.text = "Income: " + stats.money;
        }
        
        

        public void UpgradeEntity()
        {
            if (entity.Upgrade())
            {
                UpdateUpgradeEntityInformation();
                if (entity.Type == EntityType.TownHall && entity.Level == entity.maxLevel)
                {
                    gameController.OnGameWin();
                }
            }
        }

        private void EnableUpgradeButton()
        {
            upgradeButtonText.text = "Upgrade (" + entity.GetUpgradeCost() + ")";
            upgradeButton.image.color = new Color(0.556f, 0.851f, 0.718f);
            upgradeButton.enabled = true;
        }

        private void DisableUpgradeButton()
        {
            upgradeButtonText.text = "Upgrade";
            upgradeButton.image.color = Color.gray;
            upgradeButton.enabled = false;
            
        }
    }
}
