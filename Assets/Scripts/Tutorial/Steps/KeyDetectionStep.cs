using System.Collections.Generic;
using UnityEngine;

namespace Tutorial.Steps
{
    [CreateAssetMenu(menuName="Tutorial Steps/Key Detection Step")]
    public class KeyDetectionStep : TutorialStep
    {
        public List<KeyCode> keys;
        private HashSet<KeyCode> usedKeys;
        public KeyDetectionStep(string title, string description, string successMessage) : base(title, description, successMessage)
        {
        }

        public override void OnStepBegin()
        {
            usedKeys = new HashSet<KeyCode>();
            StepCompleted = false;
        }


        public override void Update()
        {

            foreach (KeyCode key in keys)
            {
                if (!usedKeys.Contains(key) && Input.GetKeyDown(key))
                {
                    usedKeys.Add(key);
                }
            }

            if (usedKeys.Count == keys.Count)
            {
                StepCompleted = true;
            }
        }
    }
}