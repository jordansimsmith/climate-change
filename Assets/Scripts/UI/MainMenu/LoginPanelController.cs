using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI.MainMenu
{
    public class LoginPanelController: MonoBehaviour
    {
        public Button GoogleButton;
        public Button AnonymousButton;
        public GameObject MenuPanel;

        private AuthHandler authHandler;
        // Start is called before the first frame update
        void Start()
        {
            if (Application.isEditor)
            {
                GoogleButton.interactable = false;
            }
            authHandler = FindObjectOfType<AuthHandler>();

            if (authHandler.CurrentAuth != null)
            {
                MenuPanel.SetActive(true);
                gameObject.SetActive(false);
                
            }
            else
            {
                MenuPanel.SetActive(false);
            }
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        
        public void LoginAnonymousButtonClicked()
        {
            GoogleButton.interactable = false;
            AnonymousButton.interactable = false;
            AnonymousButton.GetComponentInChildren<Text>().text = "Logging in...";
            authHandler.LoginAnonymously((auth) =>
            {
                APIService.Instance.access_token = auth.FirebaseToken;
                MenuPanel.SetActive(true);
                gameObject.SetActive(false);
            });
        }

        public void LoginWithGoogleButtonClicked()
        {
        
        }
    }
}