using UnityEngine;
using UnityEngine.UI;

// Manages achievement pop up UI
public class AchievementUI : MonoBehaviour
{
    private bool uiShowing;
    private bool debounce;
    private GameObject shade;
    private GameObject achievementList;
    private GameObject[] achievementViews;

    [SerializeField] private GameObject achievementPrefab;
    [SerializeField] private AchievementManager achievementManager;
    [SerializeField] private Button closeButton;

    private void Start()
    {
        shade = gameObject.transform.Find("Shade").gameObject;
        closeButton.onClick.AddListener(ToggleUI);

        // find list
        achievementList = gameObject.transform.Find("Shade/OuterContainer/InnerContainer/AchievementList").gameObject;

        RectTransform listTransform = achievementList.GetComponent<RectTransform>();
        listTransform.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical,
            150 * achievementManager.Achievements.Length);

        achievementViews = new GameObject[achievementManager.Achievements.Length];

        int i = 0;
        foreach (var achievement in achievementManager.Achievements)
        {
            // initialise achievements list
            var achievementView = Instantiate(achievementPrefab);
            achievementView.transform.parent = achievementList.transform;
            achievementView.transform.localScale = new Vector3(1f, 1f, 1f);

            // find ui components
            achievementView.transform.Find("TitleText").GetComponent<Text>().text = achievement.Title;
            achievementView.transform.Find("SubtitleText").GetComponent<Text>().text = achievement.Description;
            achievementView.transform.Find("DisabledShade").gameObject.SetActive(!achievement.Done);

            achievementViews[i] = achievementView;
            i++;
        }
    }

    private void Update()
    {
        if ((Input.GetKey(KeyCode.LeftControl) || Input.GetKey(KeyCode.RightControl)) && Input.GetKeyDown(KeyCode.A))
        {
            if (!debounce)
            {
                debounce = true;
                ToggleUI();
            }
        }
        else
        {
            debounce = false;
        }
    }

    private void ToggleUI()
    {
        if (uiShowing)
        {
            shade.SetActive(false);
        }
        else
        {
            shade.SetActive(true);
            int i = 0;
            foreach (var achievementView in achievementViews)
            {
                achievementView.transform.Find("DisabledShade").gameObject
                    .SetActive(!achievementManager.Achievements[i].Done);
                i++;
            }
        }

        uiShowing = !uiShowing;
    }
}