using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using World.Resource;

public class NaturalDisasterPanelController : MonoBehaviour
{

    public Text title;
    public Text info;
    public ResourceSingleton resources;

    public void Awake()
    {
        //sub to resource singleton
        resources.Environment.Subscribe(Callback);
        Hide();
    }

    public void Callback(ResourceEvent e)
    {
        // bottom 25% environment
        if (e.WentBelow && e.Threshold == 25)
        {
            // roll dice
            if (Random.Range(0, 10) == 0)
            {
                switch (Random.Range(0, 4))
                {
                    case 0:
                        Show("Sea Level Rises!",
                            "Your sea level rises 10% of you population now is dead" +
                            "good job!");
                        return;
                    case 1:
                        Show("Sea Level Rises!",
                            "Your sea level rises 10% of you population now is dead" +
                            "good job!");
                        return;
                    case 2:
                        Show("Sea Level Rises!",
                            "Your sea level rises 10% of you population now is dead" +
                            "good job!");
                        return;
                    case 3:
                        Show("Sea Level Rises!",
                            "Your sea level rises 10% of you population now is dead" +
                            "good job!");
                        return;
                    default:
                        return;
                }
            }
        }
        //generate Rng Event for the subscribe
    }

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
