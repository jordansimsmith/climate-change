using System;
using UnityEngine;
using UnityEngine.UI;
using World.Resource;

namespace HUD
{
    public class CarbonCreditsPanelController: MonoBehaviour
    {
        public Text carbonCreditsValue;
        private static CarbonCreditsPanelController _instance;

        private float tempTime;
        private float period = 1f;

        public float Period
        {
            get => period;
            set => period = value;
        }

        [SerializeField]
        private ResourceSingleton resourceSingleton;
        
        public static CarbonCreditsPanelController Instance => _instance;

        public void Awake()
        {
            _instance = this;
        }

        public void Update()
        {
            Resource money = resourceSingleton.Money;
            tempTime += Time.deltaTime;

            // always render money value
            carbonCreditsValue.text = money.CurAmount.ToString();
            
            // update money with rate every second;
            if (tempTime > Period)
            {
                UpdateCarbonCredits(money);
            }

        }

        private void UpdateCarbonCredits(Resource money)
        {
            tempTime = 0;
            int rate = resourceSingleton.MoneyRate;
            money.CurAmount += rate;
            carbonCreditsValue.text = money.CurAmount.ToString();
        }
        
    }
}