using UnityEngine;
using World.Resource;

namespace World.Entities
{
    public class CarbonCreditsManager: MonoBehaviour
    {
        [SerializeField] private ResourceSingleton resourceSingleton;

        public void Update()
        {
            int rate = resourceSingleton.MoneyRate;
            resourceSingleton.Money.CurAmount += rate;
        }
        

    }
}