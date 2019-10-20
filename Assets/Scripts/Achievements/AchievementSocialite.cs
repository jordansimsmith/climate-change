using UnityEngine;

public class AchievementSocialite : Achievement
{
    [SerializeField] private int target;
    private int numTalkedTo;

    public void OnTalkTo()
    {
        numTalkedTo++;

        if (numTalkedTo > target)
        {
            done = true;
        }
    }

    public override void AchievementUpdate()
    {
    }
}