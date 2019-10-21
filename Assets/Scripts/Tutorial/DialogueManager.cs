using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class DialogueManager : MonoBehaviour
    {
        [SerializeField] private Text titleText;
        [SerializeField] private Text descriptionText;
        [SerializeField] private Button continueButton;

        private string fullDescriptionText;

        public void StartDialogue(string titleText, string descriptionText)
        {
            // populate dialogue panel
            Debug.Log("Starting conversation  " + titleText);
            this.titleText.text = titleText;
            fullDescriptionText = descriptionText;
            ContinueInteractable = false;

            DisplayNextSentence(descriptionText);
        }

        public void AppendDialogue(string textToAppend)
        {
            descriptionText.text = fullDescriptionText;
            StopAllCoroutines();
            StartCoroutine(TypeSentence("\n" + textToAppend));
        }

        public void DisplayNextSentence(string description)
        {
            descriptionText.text = "";

            StopAllCoroutines();
            StartCoroutine(TypeSentence(description));
            Debug.Log(description);
        }

        public bool ContinueInteractable
        {
            get => continueButton.interactable;
            set => continueButton.interactable = value;
        }


        IEnumerator TypeSentence(string sentence)
        {
            // animate word typing in dialogue box
            foreach (var letter in sentence.ToCharArray())
            {
                descriptionText.text += letter;
                yield return null;
            }
        }

        public void FinishTyping()
        {
            // show the full description if skipped
            StopAllCoroutines();
            descriptionText.text = fullDescriptionText;
        }
    }
}