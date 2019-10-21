using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Threading;
using Firebase.Auth;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Networking.Types;


public class AuthHandler : MonoBehaviour
{
    private static FirebaseCredentials currentAuth;
    
    [DllImport("__Internal")]
    private static extern void OpenAuthUI();
    
    [DllImport("__Internal")]
    private static extern void LoginAnonymously();
    
    [DllImport("__Internal")]
    private static extern void LinkWithGoogle();

    private FirebaseAuthProvider authProvider;

    private System.Action<FirebaseCredentials> onLoginSuccess;
    private System.Action<string> onLoginError;
    
    
    
    private static bool created = false;
    void Awake()
    {
        if (!created)
        {
            DontDestroyOnLoad(gameObject);
            created = true;
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    // Start is called before the first frame update
    void Start()
    {
        var apiKey = "AIzaSyDkT5sQL8VHw88QsC1gSOOFZB7CwDBkTVs"; // public api key
        authProvider = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
    }



    public void Logout()
    {
        CurrentAuth = null;
    }

    public async void LoginAnonymousUser(System.Action<FirebaseCredentials> onLogin, System.Action<string> onError)
    {
        onLoginSuccess = onLogin;
        
        if (Application.isEditor)
        {
            FirebaseAuthLink auth = await authProvider.SignInAnonymouslyAsync();
            CurrentAuth = new FirebaseCredentials(auth.FirebaseToken, true);
            onLoginSuccess(CurrentAuth);
        }
        else
        {
            LoginAnonymously(); // login via JS firebase sdk (authplugin.jslib)
        }
        
    }

    public void AttemptLinkWithGoogle(System.Action<FirebaseCredentials> onLogin, System.Action<string> onError)
    {
        if (Application.isEditor)
        {
            onError("Can't link with google in editor mode.'");
            return;
        }

        onLoginSuccess = onLogin;
        onLoginError = onError;
        
        LinkWithGoogle();
        
    }



    public FirebaseCredentials CurrentAuth
    {
        get => currentAuth; 
        set { currentAuth = value; }
    }

    


    public void DoGoogleLogin(System.Action<FirebaseCredentials> onLogin, System.Action<string> onError) {
        onLoginSuccess = onLogin;
        onLoginError = onError;
        if (Application.isEditor)
        {
            LoginAnonymousUser(onLogin, onError);
        }
        else
        {
            OpenAuthUI();
        }
    }
    





    public async void LoginSuccess(string credentialsJson)
    {
        Debug.Log(credentialsJson);
        Debug.Log("Received");
  
        FirebaseCredentials creds = JsonConvert.DeserializeObject<FirebaseCredentials>(credentialsJson);
        CurrentAuth = creds;
        Debug.Log(creds.IdToken);
        
        onLoginSuccess(CurrentAuth);
    }
    
    public void LoginError(string error)
    {
        Debug.Log(error);
        Debug.Log("error Received");
        onLoginError(error);
    }
    
    public class FirebaseCredentials
    {
        private string idToken;
        private bool isAnonymous;
        private string displayName;
        private string email;
        private string photoURL;

        public FirebaseCredentials(string idToken, bool isAnonymous)
        {
            this.idToken = idToken;
            this.isAnonymous = isAnonymous;
        }

        public string IdToken
        {
            get => idToken;
            set => idToken = value;
        }

        public bool IsAnonymous
        {
            get => isAnonymous;
            set => isAnonymous = value;
        }

        public string DisplayName
        {
            get => displayName;
            set => displayName = value;
        }

        public string Email
        {
            get => email;
            set => email = value;
        }

        public string PhotoURL
        {
            get => photoURL;
            set => photoURL = value;
        }
    }

    
    
    
}
