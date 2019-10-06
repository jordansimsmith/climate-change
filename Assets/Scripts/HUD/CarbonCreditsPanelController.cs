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

        [SerializeField]
        private ResourceSingleton resourceSingleton;
        
        public static CarbonCreditsPanelController Instance => _instance;

        public void Awake()
        {
            _instance = this;
        }

        public void Update()
        {
            Resource resource = resourceSingleton.Money;
            carbonCreditsValue.text = resource.CurAmount.ToString();
        }
    }
}