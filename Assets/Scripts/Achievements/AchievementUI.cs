using UnityEngine;
using UnityEngine.UI;

// Manages achievement pop up UI
public class AchievementUI : MonoBehaviour  {
    private bool uiShowing;
    private bool debounce;
    private GameObject shade;
    private GameObject achievementList;

    [SerializeField] private GameObject achievementPrefab;
    [SerializeField] private AchievementManager achievementManager;
    private GameObject[] achievementViews;

    void Start()    {
        shade = gameObject.transform.Find("Shade").gameObject;

        // Populate list with achievements        
        achievementList = gameObject.transform.Find("Shade/OuterContainer/InnerContainer/AchievementList").gameObject;

        RectTransform listTransform = achievementList.GetComponent<RectTransform>();
        listTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, 150 * achievementManager.Achievements.Length);
        
        achievementViews = new GameObject[achievementManager.Achievements.Length];

        int i = 0;
        foreach (var achievement in achievementManager.Achievements)    {
            var achievementView = Instantiate(achievementPrefab);
            achievementView.transform.parent = achievementList.transform;
            achievementView.transform.localScale = new Vector3(1f, 1f, 1f);

            achievementView.transform.Find("TitleText").GetComponent<Text>().text = achievement.Title;
            achievementView.transform.Find("SubtitleText").GetComponent<Text>().text = achievement.Description;

            achievementView.transform.Find("DisabledShade").gameObject.SetActive(!achievement.Done);

            achievementViews[i] = achievementView;
            i++;
        }
    }

    void Update()   {
        if ((Input.GetKey(KeyCode.LeftControl) || (Input.GetKey(KeyCode.RightControl))) && Input.GetKeyDown(KeyCode.A))    {
            if (!debounce)  {
                debounce = true;
                ToggleUI();
            }
        } else {
            debounce = false;
        }
    }

    private void ToggleUI() {
        if (uiShowing)  {
            shade.SetActive(false);
        } else {
            int i = 0;
            foreach (var achievementView in achievementViews)    {
                achievementView.transform.Find("DisabledShade").gameObject.SetActive(!achievementManager.Achievements[i].Done);
                i++;
            }
            shade.SetActive(true);
        }
        uiShowing = !uiShowing;
    }
}