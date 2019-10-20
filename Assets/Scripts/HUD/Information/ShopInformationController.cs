using UnityEngine;
using UnityEngine.UI;

namespace HUD
{
    public class ShopInformationController : EntityInformationController
    {
        [SerializeField] private Text cost;
        private void Start()
        {
            gameObject.SetActive(false);
        }

        public override void UpdateInformation()
        {
            if (entity == null)
            {
                return;
            }

            cost.text = "Cost: " + entity.Stats.cost;
            title.text = entity.Type.ToString();
            cost.text = "Cost: " + entity.Stats.cost;
            RefreshEntityStats();
        }
    }
}