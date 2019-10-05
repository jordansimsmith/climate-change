using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Tutorial
{
    public class TutorialManager : MonoBehaviour
    {

        public TutorialStep[] tutorialSteps;
        public bool tutorialActive;
        private int currentTutorialStep;
        
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        public bool TutorialActive => tutorialActive;


        public int CurrentTutorialStep => currentTutorialStep;
    }

}
