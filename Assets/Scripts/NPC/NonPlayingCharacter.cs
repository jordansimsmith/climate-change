﻿using System.Globalization;
using Tweets;
using UnityEngine;

public class NonPlayingCharacter : MonoBehaviour
{
    public Color highlightColour;
    public Sprite avatar;

    private string firstName;
    private string lastName;
    private string occupation;

    private Color[] colours;
    private Renderer[] renderers;

    private TweetGenerator tweetGenerator;
    private NPCPanelController panelController;

    public string FirstName
    {
        get => firstName;
        set => firstName = value;
    }

    public string LastName
    {
        get => lastName;
        set => lastName = value;
    }

    public string Occupation
    {
        get => occupation;
        set => occupation = value;
    }

    // Start is called before the first frame update
    private void Start()
    {
        // get singleton instances
        tweetGenerator = TweetGenerator.Instance;
        panelController = NPCPanelController.Instance;

        // get renderers of npc model components
        renderers = GetComponentsInChildren<Renderer>();
        colours = new Color[renderers.Length];

        // cache original color
        for (int i = 0; i < renderers.Length; i++)
        {
            colours[i] = renderers[i].material.color;
        }
    }

    private void OnMouseEnter()
    {
        // highlight npc
        foreach (Renderer rend in renderers)
        {
            rend.material.color = highlightColour;
        }
    }

    private void OnMouseExit()
    {
        // restore original colours
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = colours[i];
        }
    }

    void OnMouseDown()
    {
        string fullName = firstName + " " + lastName;
        string tweet = tweetGenerator.GenerateTweet(0, 0, 0, 0);
        tweet = "\"" + tweet + "\"";
        TextInfo info = new CultureInfo("en-US", false).TextInfo;
        string occupationTitleCase = info.ToTitleCase(occupation);

        panelController.Show(fullName, occupationTitleCase, tweet, avatar);
    }
}