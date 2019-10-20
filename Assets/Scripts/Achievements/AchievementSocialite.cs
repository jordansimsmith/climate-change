using UnityEngine;

public class AchievementSocialite : Achievement
{
    [SerializeField] private int target;
    private int numTalkedTo;

    public void OnTalkTo()
    {
        numTalkedTo++;
    }

    public override void AchievementUpdate()
    {
        if (numTalkedTo >= target)
        {
            done = true;
        }
    }
}