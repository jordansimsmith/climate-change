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
        private int currentTutorialStep;
        
        
        // Start is called before the first frame update
        void Start()
        {
            this.tutorialActive = false;
            
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
            }
        }

        public bool TutorialActive => tutorialActive;

        public void StartTutorial(int startingStep)
        {
            this.tutorialActive = true;
            this.currentTutorialStep = startingStep;
            tutorialSteps[currentTutorialStep].OnStepBegin();
        }
        
        public void EndTutorial()
        {
            this.tutorialActive = false;
        }


        public int CurrentTutorialStep => currentTutorialStep;
    }

}
