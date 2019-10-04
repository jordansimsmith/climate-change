using System;
using UnityEngine;
using UnityEngine.UI;

public class NPCPanelController : MonoBehaviour
{
    public Text npcName;
    public Text npcOccupation;
    public Text npcTweet;
    public Image npcAvatar;

    private static NPCPanelController instance;

    public static NPCPanelController Instance => instance;

    private void Awake()
    {
        instance = this;
        Hide();
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show(string name, string occupation, string tweet, Image image)
    {
        gameObject.SetActive(true);

        npcName.text = name;
        npcOccupation.text = occupation;
        npcTweet.text = tweet;
    }
}
