using UnityEngine;

namespace Tutorial
{
    public abstract class TutorialStep : ScriptableObject
    {
        [SerializeField]
        protected string title;
        [SerializeField]
        protected string description;
        protected bool stepCompleted;

        protected TutorialStep(string title, string description)
        {
            this.title = title;
            this.description = description;
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

        public bool IsStepCompleted()
        {
            return stepCompleted;
        }
    }
}