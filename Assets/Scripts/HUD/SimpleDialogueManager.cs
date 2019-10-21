using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace HUD
{
    /// <summary>
    /// Simple dialogue manager class that controls the dialogue panel (separate from tutorial panel)
    /// Handles displaying of simple text prompts and supports multiple pages
    /// </summary>
    public class SimpleDialogueManager : MonoBehaviour
    {
        [SerializeField] private Text titleText;
        [SerializeField] private Text descriptionText;
        [SerializeField] private GameObject dialoguePanel;

        private Dialogue currentDialogue;
        private Queue<Dialogue> queue = new Queue<Dialogue>();

        private int dialogueIndex;

        public static SimpleDialogueManager Instance { get; set; }

        private void Awake()
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
         * Takes in a dialogue sequence and title
         * If there is nothing currently playing, the dialogue is started immediately
         * Otherwise, the dialogue is added to the queue 
         */
        public void SetCurrentDialogue(string[] lines, string dialogueName)
        {
            List<string> dialogueLines = new List<string>(lines.Length);
            dialogueLines.AddRange(lines);

            Dialogue dialogue = new Dialogue(dialogueLines, dialogueName);

            // queue empty, just start playing dialogue
            if (currentDialogue == null)
            {
                CreateDialogue(dialogue);
            }
            else
            {
                // otherwise, add dialogue to queue
                queue.Enqueue(dialogue);
            }
        }

        /**
         * Sets the current dialogue and starts the sequence 
         */
        private void CreateDialogue(Dialogue dialgoue)
        {
            currentDialogue = dialgoue;
            dialogueIndex = 0;
            dialoguePanel.SetActive(true);
            titleText.text = currentDialogue.dialogueTitle;
            DisplayNextSentence(currentDialogue.dialogue[dialogueIndex]);
        }

        /**
         * Displays the next line in the current dialogue (if available)
         * If the dialogue finishes, and there are other dialogues in the queue, it starts the next one automatically
         * Otherwise, the dialogue panel is closed
         */
        public void ContinueDialogue()
        {
            List<string> dialogueLines = currentDialogue.dialogue;
            if (dialogueIndex < dialogueLines.Count - 1)
            {
                dialogueIndex++;
                DisplayNextSentence(dialogueLines[dialogueIndex]);
            }
            else
            {
                RemoveCurrentDialogue();

                if (queue.Count > 0)
                {
                    // play next dialogue if available
                    CreateDialogue(queue.Dequeue());
                }
            }
        }

        /**
         * Removes the current dialogue that is being displayed 
         */
        private void RemoveCurrentDialogue()
        {
            dialoguePanel.SetActive(false);

            titleText.text = "";
            descriptionText.text = "";

            currentDialogue = null;
            dialogueIndex = 0;
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
            descriptionText.text = currentDialogue.dialogue[dialogueIndex];
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

    public class Dialogue
    {
        public List<string> dialogue;
        public string dialogueTitle;

        public Dialogue(List<string> dialogue, string dialogueTitle)
        {
            this.dialogue = dialogue;
            this.dialogueTitle = dialogueTitle;
        }
    }
}