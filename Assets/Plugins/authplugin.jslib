var AuthFunctions =
{
    $impl: {
        openAuthUI: function()
                  {
                    
                    const provider = new firebase.auth.GoogleAuthProvider();
                    provider.addScope("https://www.googleapis.com/auth/userinfo.profile");
                    // Request auth with profile scope (email/profile picture etc..)
                    firebase.auth().signInWithPopup(provider).then(function(result) {
            
                       // Have to duplicate this code because of .jslib syntax and rules.
                      var user = result.user;
                      user.getIdToken().then(function (token) {
                  
                            const successObj = {        
                                idToken: token,
                                isAnonymous: user.isAnonymous,
                                displayName: user.displayName ? user.displayName : "",
                                email: user.email ? user.email : "",
                                photoURL: user.photoURL ? user.photoURL : ""
                            }
                         
                        
                             unityInstance.SendMessage("AuthHandler", "LoginSuccess", JSON.stringify(successObj));
                            
                      });
                     
                    }).catch(function(error) {
                      console.log(error);
     
                      
                      unityInstance.SendMessage("AuthHandler", "LoginError", JSON.stringify(error));
                    });
                  },
        loginAnonymously: function() {
            firebase.auth().signInAnonymously().then(function(result) {
                      // Have to duplicate this code because of .jslib syntax and rules.
                      // Note to self: JSLIB doesn't allow lambdas.
                      var user = result.user;
                      user.getIdToken().then(function (token) {
                  
                            const successObj = {        
                                idToken: token,
                                isAnonymous: user.isAnonymous,
                                displayName: user.displayName ? user.displayName : "",
                                email: user.email ? user.email : "",
                                photoURL: user.photoURL ? user.photoURL : ""
                            }
                         
                        
                             unityInstance.SendMessage("AuthHandler", "LoginSuccess", JSON.stringify(successObj));
                            
                      });
            }).catch(function (error) {
                      console.log(error);
                      unityInstance.SendMessage("AuthHandler", "LoginError", JSON.stringify(error));
            
            });
        },
        linkWithGoogle: function () {
            if (firebase.auth().currentUser == undefined) {
                console.log('no user');
                return;
            }

            // Link an anonymous user with a google account (if the account already exists, user should logout and relogin to that acc.)
            const provider = new firebase.auth.GoogleAuthProvider();
            
            provider.addScope("https://www.googleapis.com/auth/userinfo.profile");
                    
            firebase.auth().currentUser.linkWithPopup(provider).then(function(result) {
                
                       // Have to duplicate this code because of .jslib syntax and rules.
                      var user = result.user;
                      user.getIdToken().then(function (token) {
                  
                            const successObj = {        
                                idToken: token,
                                isAnonymous: user.isAnonymous,
                                displayName: user.displayName ? user.displayName : "",
                                email: user.email ? user.email : "",
                                photoURL: user.photoURL ? user.photoURL : ""
                            }
                         
                        
                             unityInstance.SendMessage("AuthHandler", "LoginSuccess", JSON.stringify(successObj));
                            
                      });
                      
            }).catch(function (error) {
                console.log(error);
                 unityInstance.SendMessage("AuthHandler", "LoginError", JSON.stringify(error));
            });
            
            
        }
    },
 
    OpenAuthUI: function ()
    {
        impl.openAuthUI();
    },
    LoginAnonymously: function() {
        impl.loginAnonymously();
    },
    LinkWithGoogle: function() {
        impl.linkWithGoogle();
    }
};
 
autoAddDeps(AuthFunctions, '$impl');
mergeInto(LibraryManager.library, AuthFunctions);