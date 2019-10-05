using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField]
        private Text titleText;
        [SerializeField]
        private Text descriptionText;
        [SerializeField]
        private Button continueButton;
        

   

        public void StartDialogue(string titleText, string descriptionText)
        {
            Debug.Log("Starting conversation  " +titleText);
            this.titleText.text = titleText;
            ContinueEnabled = false;
          

            DisplayNextSentence(descriptionText);
        }

        public void DisplayNextSentence(string descriptionText)
        {
            StopAllCoroutines();
            StartCoroutine(TypeSentence(descriptionText));
            Debug.Log(descriptionText);

        }

        public bool ContinueEnabled
        {
            get => continueButton.interactable;
            set => continueButton.interactable = value;
        }


        IEnumerator TypeSentence(string sentence)
        {
            descriptionText.text = "";
            foreach (var letter in sentence.ToCharArray())
            {
                descriptionText.text += letter;
                yield return null;
            }
        }

      
    }
}
