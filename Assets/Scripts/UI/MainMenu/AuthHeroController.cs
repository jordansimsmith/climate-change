using System.Collections;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace DefaultNamespace.UI.MainMenu
{
    public class AuthHeroController : MonoBehaviour
    {
        public Text emailText;
        public Texture2D anonymousImage;
        public RawImage profilePic;

        public Button linkWithGoogle;


        public void LinkWithGoogleClicked()
        {
            linkWithGoogle.interactable = false;
            linkWithGoogle.GetComponentInChildren<Text>().text = "Linking...";
            FindObjectOfType<AuthHandler>().AttemptLinkWithGoogle((credentials) =>
                {
                    linkWithGoogle.GetComponentInChildren<Text>().text = "LINK WITH GOOGLE";
                    Initialise(credentials);
                },
                (error) =>
                {
                    string errorMsg = "";

                    if (error.Contains("auth/credential-already-in-use"))
                    {
                        errorMsg = "Account Exists (Logout)";
                    }
                    else
                    {
                        errorMsg = "Failed Link";
                    }

                    linkWithGoogle.interactable = true;
                    linkWithGoogle.GetComponentInChildren<Text>().text = errorMsg;
                });
        }

        public void Initialise(AuthHandler.FirebaseCredentials credentials)
        {
            if (credentials.IsAnonymous)
            {
                emailText.text = "Anonymous User";
                profilePic.texture = anonymousImage;
                linkWithGoogle.gameObject.SetActive(true);
                linkWithGoogle.interactable = true;
            }
            else
            {
                emailText.text = credentials.Email;
                StartCoroutine(DownloadAndSetImage(credentials.PhotoURL));
                linkWithGoogle.gameObject.SetActive(false);
            }
        }

        IEnumerator DownloadAndSetImage(string MediaUrl)
        {
            UnityWebRequest request = UnityWebRequestTexture.GetTexture(MediaUrl);
            yield return request.SendWebRequest();
            if (request.isNetworkError || request.isHttpError)
                Debug.Log(request.error);
            else
                profilePic.texture = ((DownloadHandlerTexture) request.downloadHandler).texture;
        }
    }
}