using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HUD
{
    /**
     * Simple dialogue manager class that controls the dialogue panel (separate from tutorial panel)
     * Handles displaying of simple text prompts and supports multiple pages
     */
    public class SimpleDialogueManager : MonoBehaviour
    {
        [SerializeField]
        private Text titleText;
        [SerializeField]
        private Text descriptionText;
        [SerializeField]
        private GameObject dialoguePanel;
        
        private List<string> dialogueLines = new List<string>();
        private int dialogueIndex;
        private string dialogueTitle;
        
        public static SimpleDialogueManager Instance { get; set;  }

        public void Awake()
        {
            dialoguePanel.SetActive(false);
            if (Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }
        
        /**
         * Sets the current dialogue sequence to be displayed
         * Takes in a list of dialogue and title for the sequence
         */
        public void SetCurrentDialogue(string[] lines, string dialogueName)
        {
            dialogueIndex = 0;
            dialogueTitle = dialogueName;
            dialogueLines = new List<string>(lines.Length);
            dialogueLines.AddRange(lines);
            
            CreateDialogue();
        }

        /**
         * Displays the dialogue panel and initiates the sequence
         */
        private void CreateDialogue()
        {
            dialoguePanel.SetActive(true);
            titleText.text = dialogueTitle;
            DisplayNextSentence(dialogueLines[dialogueIndex]);
        }

        /**
         * Displays the next dialogue line in the sequence (if available) or closes the panel
         */
        public void ContinueDialogue()
        {
            if (dialogueIndex < dialogueLines.Count - 1)
            {
                dialogueIndex++;
                DisplayNextSentence(dialogueLines[dialogueIndex]);
            }
            else
            {
                dialoguePanel.SetActive(false);
                RemoveCurrentDialogue();
            }
        }
        private void RemoveCurrentDialogue()
        {
            titleText.text = "";
            descriptionText.text = "";

            dialogueLines = null;
            dialogueIndex = 0;
            dialogueTitle = "";

        }
        
        /**
         * Starts co-routine to display typewriter animation
         */
        private void DisplayNextSentence(string description)
        {
            descriptionText.text = "";
            
            StopAllCoroutines();
            StartCoroutine(TypeSentence(description));
        }

        /**
         * Autocompletes the current dialogue line in the sequence 
         */
        public void FinishTyping()
        {
            StopAllCoroutines();
            descriptionText.text = dialogueLines[dialogueIndex];
        }


        IEnumerator TypeSentence(string sentence)
        {
            foreach (var letter in sentence.ToCharArray())
            {
                descriptionText.text += letter;
                yield return null;
            }
        }


    }
}
