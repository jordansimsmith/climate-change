using HUD;
using UnityEngine;

namespace Achievements
{
    public class AchievementManager : MonoBehaviour
    {

        private static readonly string ACHIEVEMENT_UNLOCKED_TITLE = "Achievement Unlocked!";
        private Achievement[] achievements;
        public Achievement[] Achievements => achievements;

        private int numOfUnlockedAchievements = 0;

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
                    if (achievement.Done)
                    {
                        numOfUnlockedAchievements++;
                        TriggerAchievement(achievement);
                    }
                }
            }
        }

        // Displays achievement when it is earned
        private void TriggerAchievement(Achievement achievement)
        {
            string achievementStatus = "Unlocked: " + numOfUnlockedAchievements + "/" + achievements.Length;
            string toolTip = "Click ctrl + A to view all achievements";
            string achievementCompletionText = "Congratulations, you have unlocked \"" + achievement.Title + "\"\n" + achievementStatus + "\n" + toolTip;
        
            SimpleDialogueManager.Instance.SetCurrentDialogue(new [] {achievementCompletionText}, ACHIEVEMENT_UNLOCKED_TITLE);
        }
    }
}
