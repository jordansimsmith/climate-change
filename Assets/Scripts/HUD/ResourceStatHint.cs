using System;
using UnityEngine;
using UnityEngine.UI;
using World.Resource;

namespace HUD
{
    public class ResourceStatHint : MonoBehaviour
    {
        [SerializeField] private Text hint;
        private const String msg = "Supply: {0}\nDemand:{1}";

        public void ShowHint(ResourceStat stat)
        {
            gameObject.SetActive(true);
            hint.text = string.Format(msg, stat.supply, stat.demand);
        }

        public void HideHint()
        {
            gameObject.SetActive(false);
        }
    }
}