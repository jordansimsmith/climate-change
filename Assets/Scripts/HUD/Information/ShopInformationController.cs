using UnityEngine;
using UnityEngine.UI;

namespace HUD
{
    public class ShopInformationController : EntityInformationController
    {
        [SerializeField] private Text cost;

        private void Start()
        {
            // hide initially
            gameObject.SetActive(false);
        }

        public override void UpdateInformation()
        {
            if (entity == null)
            {
                return;
            }

            // set cost
            title.text = entity.Type.ToString();
            cost.text = "Cost: " + entity.Stats.cost;
            RefreshEntityStats();
        }
    }
}