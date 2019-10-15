using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using World;
using World.Resource;
using World.Tiles;
using Random = UnityEngine.Random;

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