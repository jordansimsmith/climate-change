using Tutorial;
using UnityEngine;

public class TutorialFinishedAchievement : Achievement
{
    [SerializeField] private TutorialManager tutorialManager;

    public override void AchievementUpdate()
    {
        if (tutorialManager.TutorialComplete && !done)
        {
            Debug.Log(Title + " Done");
            done = true;
        }
    }
}