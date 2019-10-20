using System.Collections.Generic;
using UnityEngine;
using World;
using World.Entities;

namespace Tutorial.Steps
{
    [CreateAssetMenu(menuName="Tutorial Steps/Entity Placement Step")]
    public class EntityPlacementStep : TutorialStep
    {
        public EntityType entityToDetect;
        private GameBoard gameBoard;
        
        public EntityPlacementStep(string title, string description) : base(title, description)
        {
        }

        public override void OnStepBegin()
        {
            gameBoard = GameObject.FindGameObjectWithTag("InGameBoard").GetComponent<GameBoard>();
            StepCompleted = false;
        }

        public override void Update()
        {
            if (gameBoard.IsEntityTypeOnBoard(entityToDetect))
            {
                StepCompleted = true;
            }
        }
    }
}