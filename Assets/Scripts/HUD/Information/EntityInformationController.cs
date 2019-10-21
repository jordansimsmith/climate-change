using UnityEngine;
using UnityEngine.UI;
using World.Entities;
using World.Tiles;

namespace HUD
{
    /// <summary>
    /// Template pattern for all HUD's that display entity information can implement
    /// Provides basic built in functionality and allows hooks for custom display functionality
    /// </summary>
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

        // template method that handles setting the entity and displaying panel
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

        // hook method for individual UI formatting
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