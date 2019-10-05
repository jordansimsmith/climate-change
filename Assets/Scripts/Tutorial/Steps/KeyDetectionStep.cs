using System.Collections.Generic;
using UnityEngine;

namespace Tutorial.Steps
{
    [CreateAssetMenu(menuName="Tutorial Steps/Key Detection Step")]
    public class KeyDetectionStep : TutorialStep
    {
        public List<KeyCode> keys;
        public KeyDetectionStep(string title, string description) : base(title, description)
        {
        }


        public override void Update()
        {
            foreach (KeyCode key in keys)
            {
                if (Input.GetKeyDown(key))
                {
                    keys.Remove(key);
                }
            }

            if (keys.Count == 0)
            {
                this.stepCompleted = true;
            }
        }
    }
}