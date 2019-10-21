using UnityEngine;
using UnityEngine.UI;
using World.Entities;
using World.Tiles;

namespace HUD
{
    public abstract class EntityInformationController : MonoBehaviour
    {
        [SerializeField] protected Text title;
        [SerializeField] protected Text electricity;
        [SerializeField] protected Text environment;
        [SerializeField] protected Text food;
        [SerializeField] protected Text shelter;
        [SerializeField] protected Text income;
        [SerializeField] protected GameController gameController;

        protected Entity entity;

        public Entity Entity => entity;

        public void ShowInformation(Entity e)
        {
            entity = e;
            gameObject.SetActive(true);

            UpdateInformation();
        }

        public void CloseInformation()
        {
            entity = null;
            gameObject.SetActive(false);
        }

        public abstract void UpdateInformation();

        protected void RefreshEntityStats()
        {
            // display stats
            EntityStats stats = entity.Stats;
            electricity.text = "Power: " + stats.power;
            environment.text = "Environment: " + stats.environment;
            food.text = "Food: " + stats.food;
            shelter.text = "Shelter: " + stats.shelter;
            income.text = "Income: " + stats.money;
        }
    }
}