using UnityEngine;

namespace Tutorial
{
    public abstract class TutorialStep : ScriptableObject
    {
        [SerializeField] protected string title;
        [SerializeField, TextArea(3, 10)] protected string description;
        [SerializeField, TextArea(3, 10)] protected string successMessage;

        private bool stepCompleted;

        protected TutorialStep(string title, string description, string successMessage = null)
        {
            this.title = title;
            this.description = description;
            this.successMessage = successMessage;
            stepCompleted = false;
        }

        public string Title => title;

        public string Description => description;

        public string SuccessMessage => successMessage;

        /**
         * Called when this tutorial step begins/initially renders
         */
        public virtual void OnStepBegin()
        {
        }

        /**
         * Called when this step is completed (StepCompleted set to true)
         */
        public virtual void OnStepCompleted()
        {
        }


        /**
         * Called when this tutorial step ends (the user continues to next step)
         */
        public virtual void OnStepEnd()
        {
        }


        /**
         * Called every frame when this step is the active step.
         */
        public virtual void Update()
        {
        }


        public virtual bool StepCompleted
        {
            get => stepCompleted;
            set
            {
                if (!stepCompleted && value)
                {
                    OnStepCompleted();
                }

                stepCompleted = value;
            }
        }
    }
}