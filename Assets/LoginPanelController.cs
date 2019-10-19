using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LoginPanelController : MonoBehaviour
{
    public Button GoogleButton;
    
    // Start is called before the first frame update
    void Start()
    {
        if (Application.isEditor)
        {
            GoogleButton.interactable = false;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void LoginAnonymousButtonClicked()
    {
       
    }

    public void LoginWithGoogleButtonClicked()
    {
        
    }
}
