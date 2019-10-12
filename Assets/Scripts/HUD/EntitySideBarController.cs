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

        public void ShowSideBar(Entity e)
        {
            Debug.Log("setting entity");
            entity = e;
            gameObject.SetActive(true);
            RefreshEntityInformation();;
        }

        public void CloseSideBar()
        {
            entity = null;
            gameObject.SetActive(false);
        }

        private void RefreshEntityInformation()
        {
            if (entity == null)
            {
                return;
            }
            String levelText = "Level " + entity.Level;
            title.text = entity.Type + " (" + levelText + ")";

            EntityStats stats = entity.Stats;

            electricity.text = "Power: " + stats.power;
            environment.text = "Environment: " + stats.environment;
            food.text = "Food: " + stats.food;
            shelter.text = "Shelter: " + stats.shelter;
            income.text = "Income: " + stats.money;
        }

        public void UpgradeEntity()
        {
            entity.Upgrade();
            RefreshEntityInformation();
        }
    }
}
