using UnityEngine;
using UnityEngine.UI;

public class NaturalDisasterPanelController : MonoBehaviour
{
    public Text title;
    public Text info;

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show(string disatster, string disasterInfo)
    {
        //Setup event info
        title.text = disatster;
        info.text = disasterInfo;
        gameObject.SetActive(true);
    }
}