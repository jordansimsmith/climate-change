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
        [SerializeField]
        private Button continueButton;
        
        public GameObject dialoguePanel;
        
        private List<string> dialogueLines = new List<string>();
        private int dialogueIndex;
        private string dialogueTitle;

        
        private string fullDescriptionText;
        
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
        public void AddNewDialogue(string[] lines, string dialogueName)
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
            }
        }

        public void CreateDialogue()
        {
            titleText.text = dialogueTitle;
            DisplayNextSentence(dialogueLines[dialogueIndex]);
            dialoguePanel.SetActive(true);
            
        }
        
        public void DisplayNextSentence(string description)
        {
            descriptionText.text = "";
            
            StopAllCoroutines();
            StartCoroutine(TypeSentence(description));
            Debug.Log(description);

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
