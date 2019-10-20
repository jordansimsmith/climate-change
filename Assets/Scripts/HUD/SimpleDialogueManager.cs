using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Tutorial
{
    public class SimpleDialogueManager : MonoBehaviour
    {
        [SerializeField]
        private Text titleText;
        [SerializeField]
        private Text descriptionText;
        
        public GameObject dialoguePanel;
        
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
        public void SetCurrentDialogue(string[] lines, string dialogueName)
        {
            dialogueIndex = 0;
            dialogueTitle = dialogueName;
            dialogueLines = new List<string>(lines.Length);
            dialogueLines.AddRange(lines);
            
            CreateDialogue();
        }

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

        private void CreateDialogue()
        {
            dialoguePanel.SetActive(true);
            titleText.text = dialogueTitle;
            DisplayNextSentence(dialogueLines[dialogueIndex]);
        }

        private void RemoveCurrentDialogue()
        {
            titleText.text = "";
            descriptionText.text = "";

            dialogueLines = null;
            dialogueIndex = 0;
            dialogueTitle = "";

        }
        
        private void DisplayNextSentence(string description)
        {
            descriptionText.text = "";
            
            StopAllCoroutines();
            StartCoroutine(TypeSentence(description));
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
