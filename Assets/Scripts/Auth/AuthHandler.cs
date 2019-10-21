using System.Runtime.InteropServices;
using Firebase.Auth;
using Newtonsoft.Json;
using UnityEngine;

public class AuthHandler : MonoBehaviour
{
    private static FirebaseCredentials currentAuth;

    /**
        Functios are imported from authplugin.jslib during a WebGL Deployment.
        Interacts with Firebase SDK so Auth in WebGL can be handled/hijacked by the JavaScript SDK
     */
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

    private static string apiKey ="AIzaSyDkT5sQL8VHw88QsC1gSOOFZB7CwDBkTVs";
    // Start is called before the first frame update
    void Start()
    {
        authProvider = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
    }


    public void Logout()
    {
        CurrentAuth = null;
    }

    public async void LoginAnonymousUser(System.Action<FirebaseCredentials> onLogin, System.Action<string> onError)
    {
        onLoginSuccess = onLogin;

        if (Application.isEditor) // Use unofficial firebase.NET SDK (which only works in editor/standalone.)
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


    public void DoGoogleLogin(System.Action<FirebaseCredentials> onLogin, System.Action<string> onError)
    {
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

        FirebaseCredentials creds = JsonConvert.DeserializeObject<FirebaseCredentials>(credentialsJson);
        CurrentAuth = creds;

        onLoginSuccess(CurrentAuth);
    }

    public void LoginError(string error)
    {
        Debug.Log(error);
        onLoginError(error);
    }

    /**
        Credentials struct
     */
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