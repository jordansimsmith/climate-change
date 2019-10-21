using System;
using UnityEngine;
using UnityEngine.UI;
using World;
using World.Entities;

namespace HUD
{
    public class UpgradeInformationController : EntityInformationController
    {
        [SerializeField] private Text level;
        [SerializeField] private Button upgradeButton;
        [SerializeField] private Button closeUpgradePanelButton;
        [SerializeField] private Button[] researchButtons;
        [SerializeField] private Text description;
        [SerializeField] private RectTransform contentPanelRectTransform;
        [SerializeField] private GameBoard gameBoard;
        [SerializeField] private GameObject upgradeToolTip;
        
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

            // Set the level  
            String levelText = "Level: " + entity.Level;
            level.text = levelText;
            
            // Set the title 
            title.text = entity.Type.ToString();
            
            // Set the description 
            var des = entity.UpgradeInfo.Description;
            if (des == null) {
                des = "Climate change is bad";
            }
            description.text = des;
            
            // Set the research option buttons
            int index = 0;
            foreach (var research in entity.UpgradeInfo.GetLevel(entity.Level).ResearchOptions) {
                if (index > 2) {
                    Debug.Log("Not enough space to fit more than 3 research options");
                    break;
                }
                
                // Get the next button and make active
                var researchButton = researchButtons[index];
                researchButton.gameObject.SetActive(true);
                researchButton.GetComponentInChildren<Text>().text = research.Name;
                
                // If research already done, button disabled
                researchButton.interactable = !research.isResearched;
                
                // Callback
                researchButton.onClick.RemoveAllListeners();
                if (!research.isResearched) {
                    researchButton.onClick.AddListener(() => {
                        if (entity.Research(research)) {
                            UpdateInformation();
                        }
                    });
                }
                
                index++;
            }

            // Disable the buttons with no attached research
            for (int i = index; i < researchButtons.Length; i++) {
                researchButtons[i].gameObject.SetActive(false);
            }
            
            // Hotfix for text size
            LayoutRebuilder.ForceRebuildLayoutImmediate(contentPanelRectTransform);
            
            RefreshEntityStats();

            if (entity.IsMaxLevel())
            {
                DisableUpgradeButton("Max Level");
            } else if (IsEntityLocked())
            {
                DisableUpgradeButton("Upgrade");
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


        private bool IsEntityLocked()
        {
            return entity.Type != EntityType.TownHall && entity.Level >= gameBoard.GetTownHallLevel();
        }

        public bool IsUpgradeInformationOpen()
        {
            return gameObject.activeSelf;
        }
        
        private void EnableUpgradeButton()
        {
            upgradeButtonText.text = "Upgrade (" + entity.GetUpgradeCost() + ")";
            upgradeButton.interactable = true;
        }

        private void DisableUpgradeButton(string text)
        {
            upgradeButtonText.text = text;
            upgradeButton.interactable = false;
        }

        public void OnMouseEnterButton()
        {
            if (!entity.IsMaxLevel() && IsEntityLocked())
            {
                upgradeToolTip.SetActive(true);   
            }
        }

        public void OnMouseExitButton()
        {
            upgradeToolTip.SetActive(false);
        }
    }
}