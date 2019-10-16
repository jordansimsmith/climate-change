using UnityEngine;

namespace Tutorial
{
    public class TutorialManager : MonoBehaviour
    {
        public TutorialStep[] tutorialSteps;
        public bool tutorialActive;
        public GameObject tutorialCanvas;
        private DialogueManager dialogueManager;
        private int currentTutorialStep;
        private bool tutorialComplete = false;

        public bool TutorialComplete => tutorialComplete;
        public int CurrentTutorialStep => currentTutorialStep;

        // Start is called before the first frame update
        private void Start()
        {
            EndTutorial();
            dialogueManager = tutorialCanvas.GetComponent<DialogueManager>();
            StartTutorial(0);
        }

        // Update is called once per frame
        private void Update()
        {
            if (!tutorialActive || currentTutorialStep >= tutorialSteps.Length)
            {
                return;
            }

            TutorialStep currentStep = tutorialSteps[currentTutorialStep];
            if (!currentStep.StepCompleted)
            {
                currentStep.Update();

                if (currentStep.StepCompleted)
                {
                    dialogueManager.AppendDialogue(currentStep.SuccessMessage);
                    InvalidateUI();
                }
            }
            else if (Input.GetKeyDown(KeyCode.Space))
            {
                NextStep();
            }
        }

        public bool TutorialActive => tutorialActive;

        public void StartTutorial(int startingStep)
        {
            tutorialComplete = false;
            tutorialActive = true;
            tutorialCanvas.SetActive(true);
            currentTutorialStep = startingStep;
            DrawStep(currentTutorialStep);
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

            DrawStep(currentTutorialStep);
        }

        private void DrawStep(int step)
        {
            TutorialStep nextStep = tutorialSteps[step];
            dialogueManager.StartDialogue(nextStep.Title, nextStep.Description);
            nextStep.OnStepBegin();
            InvalidateUI();
        }

        public void InvalidateUI()
        {
            TutorialStep currentStep = tutorialSteps[currentTutorialStep];

            dialogueManager.ContinueInteractable = currentStep.StepCompleted;
        }

        public void EndTutorial()
        {
            tutorialActive = false;
            tutorialCanvas.SetActive(false);
            tutorialComplete = true;
        }
    }
}