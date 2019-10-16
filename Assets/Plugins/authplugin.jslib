



var AuthFunctions =
{
    $impl: {
        openAuthUI: function()
        {
          const provider = new firebase.auth.GoogleAuthProvider();
          firebase.auth().signInWithPopup(provider).then(function(result) {
  
            // The signed-in user info.
            var user = result.user;
        
            const successObj = {
              accessToken: result.credential.accessToken,
              idToken: result.credential.idToken
            }

            unityInstance.SendMessage("AuthHandler", "LoginSuccess", JSON.stringify(successObj));

          }).catch(function(error) {
            console.log(error);
            // Handle Errors here.
            var errorCode = error.code;
            var errorMessage = error.message;
            // The email of the user's account used.
            var email = error.email;
            // The firebase.auth.AuthCredential type that was used.
            var credential = error.credential;

            
            unityInstance.SendMessage("AuthHandler", "LoginError", JSON.stringify(error));
          });
        },
    },
 
    OpenAuthUI: function ()
    {
        impl.openAuthUI();
    },
};
 
autoAddDeps(AuthFunctions, '$impl');
mergeInto(LibraryManager.library, AuthFunctions);
