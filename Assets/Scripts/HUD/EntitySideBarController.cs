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
        [SerializeField] private Button upgradeButton;
    
        private Entity entity;

        // Start is called before the first frame update
        void Start()
        {
            gameObject.SetActive(false);

        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public void ShowSideBar(Entity entity)
        {
            this.entity = entity;
            gameObject.SetActive(true);
            RefreshEntityInformation();;
        }

        private void RefreshEntityInformation()
        {
            String levelText = "Level " + entity.Level;
            title.text = entity.Type + " (" + levelText + ")";
        
            EntityStats stats = entity.Stats;

            electricity.text = "Power: " + stats.power;
            environment.text = "Environment: " + stats.environment;
            food.text = "Food: " + stats.food;
            shelter.text = "Shelter: " + stats.shelter;
        }

        public void UpgradeEntity()
        {
            this.entity.Upgrade();
            RefreshEntityInformation();
        }
    }
}
