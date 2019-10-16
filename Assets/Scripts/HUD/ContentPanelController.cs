using UnityEngine;
using UnityEngine.UI;
using World.Entities;

namespace HUD
{
    public class ContentPanelController : MonoBehaviour
    {
        [SerializeField] private Button reclaimButton;
        [SerializeField] private Button deleteButton;

        public void InvalidateUI()
        {
           reclaimButton.GetComponent<ReclaimHandler>().InvalidateReclaimButton();
           deleteButton.GetComponent<DeleteHandler>().InvalidateDeleteButton();
        }
        
    }
}