using UnityEngine;

namespace Tutorial
{
    public abstract class TutorialStep : ScriptableObject
    {
        [SerializeField]
        protected string title;
        [SerializeField,  TextArea(3, 10)]
        protected string description;

        protected bool stepCompleted;

        protected TutorialStep(string title, string description)
        {
            this.title = title;
            this.description = description;
            this.stepCompleted = false;
        }

        public string Title => title;

        public string Description => description;

        public virtual void OnStepBegin()
        {
            
        }

        public virtual void OnStepEnd()
        {
            
        }

        /**
         * Called every frame when this step is the active step.
         */
        public virtual void Update()
        {
            
        }

        public virtual bool IsStepCompleted()
        {
            return stepCompleted;
        }
    }
}