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
    private FirebaseAuthLink currentAuth;
    
    [DllImport("__Internal")]
    private static extern void OpenAuthUI();

    private FirebaseAuthProvider authProvider;
    
    // Start is called before the first frame update
    void Start()
    {
        var apiKey = "AIzaSyDkT5sQL8VHw88QsC1gSOOFZB7CwDBkTVs"; // public api key
        authProvider = new FirebaseAuthProvider(new FirebaseConfig(apiKey));
    }

    public void UpdateAuthState(FirebaseAuthLink auth)
    {
        currentAuth = auth;
    }

    public async void LoginAnonymously(System.Action<FirebaseAuth> onLogin)
    {
        FirebaseAuthLink auth = await authProvider.SignInAnonymouslyAsync();
     
        onLogin(auth);
        
        UpdateAuthState(auth);
    }



    public FirebaseAuthLink CurrentAuth => currentAuth;

    

    #if UNITY_WEBGL
    public void OpenUI(System.Action<FirebaseAuth> onLogin) {
        if (Application.isEditor)
        {
            
          
            LoginAnonymously(onLogin);
        }
        else
        {
            OpenAuthUI();
        }
    }
    





    public async void GoogleLoginSuccess(string credentialsJson)
    {
     
        GoogleCredentials credentials = JsonConvert.DeserializeObject<GoogleCredentials>(credentialsJson);
     
        var auth = await authProvider.SignInWithOAuthAsync(FirebaseAuthType.Google, credentials.AccessToken);
        
        UpdateAuthState(auth);
        Debug.Log(auth.User.Email);
    }
    
    public void GoogleLoginError(string error)
    {
        
    }
    
    private class GoogleCredentials
    {
        private string accessToken;
        private string idToken;

        public string AccessToken => accessToken;

        public string IdToken => idToken;
    }

    #else
    public void OpenUI() {
        LoginAnonymously();
    }
    #endif
    
    
    
}
