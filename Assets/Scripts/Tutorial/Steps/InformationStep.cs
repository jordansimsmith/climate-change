using UnityEngine;

namespace Tutorial.Steps
{
    [CreateAssetMenu(menuName = "Tutorial Steps/Information Step")]
    public class InformationStep : TutorialStep
    {
        public InformationStep(string title, string description) : base(title, description)
        {
        }

        public override void OnStepBegin()
        {
            StepCompleted = true;
        }
    }
}