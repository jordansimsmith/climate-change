using UnityEngine;

public class AchievementManager : MonoBehaviour
{
    private Achievement[] achievements;
    public Achievement[] Achievements => achievements; 

    // Start is called before the first frame update
    private void Awake()
    {
        // load achievements
        achievements = gameObject.GetComponentsInChildren<Achievement>();
    }

    // Update is called once per frame
    private void Update()
    {
        // check unfinished achievements if they are completed
        foreach (var achievement in achievements)   {
            if (!achievement.Done)  {
                achievement.AchievementUpdate();
                if (achievement.Done)   {
                    TriggerAchievement(achievement);
                }
            }
        }
    }

    // Displays achievement when it is earned
    public void TriggerAchievement(Achievement achievement)
    {
        // TODO: message to the user that the achievement is completed
    }
}
