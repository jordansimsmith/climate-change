using System.Collections;
using System.Collections.Generic;
using Tutorial;
using UnityEngine;

public class TutorialFinishedAchievement : Achievement
{
    [SerializeField] private TutorialManager tutorialManager;

    // Update is called once per frame
    public override void AchievementUpdate()
    {
        if (tutorialManager.TutorialComplete && !done)  {
            done = true;
        }
    }
}
