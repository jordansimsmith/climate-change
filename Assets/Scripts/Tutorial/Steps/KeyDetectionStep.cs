using System.Collections.Generic;
using UnityEngine;

namespace Tutorial.Steps
{
    [CreateAssetMenu(menuName = "Tutorial Steps/Key Detection Step")]
    public class KeyDetectionStep : TutorialStep
    {
        public List<KeyCode> keys;
        private HashSet<KeyCode> usedKeys;

        public KeyDetectionStep(string title, string description, string successMessage) : base(title, description,
            successMessage)
        {
        }

        public override void OnStepBegin()
        {
            // store user keypresses
            usedKeys = new HashSet<KeyCode>();
            StepCompleted = false;
        }


        public override void Update()
        {
            // check if each desired key has been pressed
            foreach (KeyCode key in keys)
            {
                // key pressed
                if (!usedKeys.Contains(key) && Input.GetKeyDown(key))
                {
                    usedKeys.Add(key);
                }
            }

            // player pressed all of the keys
            if (usedKeys.Count == keys.Count)
            {
                StepCompleted = true;
            }
        }
    }
}