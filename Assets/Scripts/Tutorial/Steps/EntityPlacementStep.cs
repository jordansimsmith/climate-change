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
            this.gameBoard = GameObject.FindGameObjectWithTag("Board").GetComponent<GameBoard>();
            stepCompleted = false;
        }

        public override void Update()
        {
            if (this.gameBoard.IsEntityTypeOnBoard(entityToDetect))
            {
                this.stepCompleted = true;
            }
        }
    }
}