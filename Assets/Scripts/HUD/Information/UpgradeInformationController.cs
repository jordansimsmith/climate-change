using System;
using UnityEngine;
using UnityEngine.UI;
using World.Entities;

namespace HUD
{
    public class UpgradeInformationController : EntityInformationController
    {
        [SerializeField] private Text level;
        [SerializeField] private Button upgradeButton;
        [SerializeField] private Button closeUpgradePanelButton;
        private Text upgradeButtonText;
        
        private static UpgradeInformationController instance;
        public static UpgradeInformationController Instance => instance;

        void Awake()
        {
            instance = this;
            gameObject.SetActive(false);
            upgradeButtonText = upgradeButton.GetComponentInChildren<Text>();

        }
        public override void UpdateInformation()
        {
            if (entity == null)
            {
                return;
            }

            String levelText = "Level: " + entity.Level;
            level.text = levelText;
            title.text = entity.Type.ToString();
            RefreshEntityStats();

            if (entity.IsMaxLevel())
            {
                DisableUpgradeButton();
            }
            else
            {
                EnableUpgradeButton();
            }
        }
        
        public void UpgradeEntity()
        {
            if (entity.Upgrade())
            {
                UpdateInformation();
                if (entity.Type == EntityType.TownHall && entity.IsMaxLevel())
                {
                    gameController.OnGameWin();
                }
            }
        }

        public bool isUpgradeInformationOpen()
        {
            return gameObject.activeSelf;
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