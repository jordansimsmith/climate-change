using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI.MainMenu
{
    public class LoginPanelController: MonoBehaviour
    {
        public Button GoogleButton;
        public Button AnonymousButton;
        public GameObject MenuPanel;
        public GameObject AuthHero;
        
        private AuthHandler authHandler;
        // Start is called before the first frame update
        void Start()
        {
            if (Application.isEditor)
            {
                GoogleButton.interactable = false;
            }
            authHandler = FindObjectOfType<AuthHandler>();

            InvalidateUI();
        }


        private void InvalidateUI()
        {
            if (authHandler.CurrentAuth != null)
            {
                GoogleButton.interactable = true;
                AnonymousButton.interactable = true;
                MenuPanel.SetActive(true);
                 
                AuthHero.SetActive(true);
                AuthHero.GetComponent<AuthHeroController>().Initialise(authHandler.CurrentAuth);
                gameObject.SetActive(false);
               
            }
            else
            {
                GoogleButton.interactable = true;
                AnonymousButton.interactable = true;
                MenuPanel.SetActive(false);
                AuthHero.SetActive(false);
            }
        }

        public void TriggerLogout()
        {
            authHandler.Logout();
            InvalidateUI();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
        
        

        
        public void LoginAnonymousButtonClicked()
        {
            GoogleButton.interactable = false;
            AnonymousButton.interactable = false;
            
            Text buttonText = AnonymousButton.GetComponentInChildren<Text>();
            
            string oldButtonText = buttonText.text;
            buttonText.text = "Logging in...";
            authHandler.LoginAnonymousUser((auth) =>
            {
                buttonText.text = oldButtonText;
                APIService.Instance.access_token = auth.IdToken;
               InvalidateUI();
            }, (error) =>
            {
                GoogleButton.interactable = true;
                AnonymousButton.interactable = true;
                buttonText.text = "Login Failed. Try Again?";
            });
        }

        public void LoginWithGoogleButtonClicked()
        {
            GoogleButton.interactable = false;
            AnonymousButton.interactable = false;
    
            Text buttonText = GoogleButton.GetComponentInChildren<Text>();
            
            string oldButtonText = buttonText.text;
            buttonText.text = "Logging in...";
            authHandler.DoGoogleLogin((auth) =>
            {
                buttonText.text = oldButtonText;
                APIService.Instance.access_token = auth.IdToken;
                
                InvalidateUI();
                
            }, (error) =>
            {
                GoogleButton.interactable = true;
                AnonymousButton.interactable = true;
                buttonText.text = "Login Failed. Try Again?";
            });
        }
    }
}