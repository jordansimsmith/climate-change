using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class TutorialManager : MonoBehaviour
    {

        public TutorialStep[] tutorialSteps;
        public bool tutorialActive;
        public Canvas tutorialCanvas;
        private DialogueManager dialogueManager;
        private int currentTutorialStep;
        
        
        // Start is called before the first frame update
        void Start()
        {
            this.EndTutorial();
            this.dialogueManager = tutorialCanvas.GetComponent<DialogueManager>();
            this.StartTutorial(0);
        }

        // Update is called once per frame
        void Update()
        {
            if (!tutorialActive || currentTutorialStep >= tutorialSteps.Length)
            {
                return;
            }
            
            TutorialStep currentStep = tutorialSteps[currentTutorialStep];
            if (!currentStep.IsStepCompleted())
            {
               
                currentStep.Update();
                InvalidateUI();
            }
        }

        public bool TutorialActive => tutorialActive;

        public void StartTutorial(int startingStep)
        {
            this.tutorialActive = true;
            this.tutorialCanvas.enabled = true;
            this.currentTutorialStep = startingStep;
            TutorialStep currentStep = tutorialSteps[currentTutorialStep];
            currentStep.OnStepBegin();
            dialogueManager.StartDialogue(currentStep.Title, currentStep.Description);

            InvalidateUI();
            
        }

        public void NextStep()
        {
            TutorialStep currentStep = tutorialSteps[currentTutorialStep];
            currentStep.OnStepEnd();
            currentTutorialStep++;
            if (currentTutorialStep >= tutorialSteps.Length)
            {
               EndTutorial();
                return;
            }
            
            TutorialStep nextStep = tutorialSteps[currentTutorialStep];
            dialogueManager.StartDialogue(nextStep.Title, nextStep.Description);
            nextStep.OnStepBegin();
            InvalidateUI();

        }

        public void InvalidateUI()
        {
            TutorialStep currentStep = tutorialSteps[currentTutorialStep];
           
            dialogueManager.ContinueInteractable = currentStep.IsStepCompleted();
        }
        
        public void EndTutorial()
        {
            this.tutorialActive = false;
            this.tutorialCanvas.enabled = false;
        }
        


        public int CurrentTutorialStep => currentTutorialStep;
    }

}
