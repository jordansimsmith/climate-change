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
    public GameObject tornadoPrefab;
    private GameObject activeTornado;

    public void Awake()
    {
        //Every 10 Seconds check if we should dispatch an event
        InvokeRepeating("DisasterEventDispatcher", 0, 10f);
        Hide();
    }

    public void DisasterEventDispatcher()
    {
        doCyclone();
        // If environment drops below 100, i.e. relatively few trees to factories (you start w +300 env thanks trees).  
        var envScore = resources.totalSupply.environment - resources.totalDemand.environment;
        if (envScore < 100)
        {
            // roll dice 10% to have the natural event
            if (Random.Range(0, 10) == 0)
            {
                switch (Random.Range(0, 5))
                {
                    case 0:
                        Show("Sea Level Rises!",
                            "Your sea level rises 10% of you population now is dead" +
                            "good job!");
                        return;
                    case 1:
                        doCyclone();
                        return;
                    case 2:
                        doCyclone();
                        return;
                    case 3:
                        Show("There has been a drought!",
                            "Your citizens don't have water to drink and you have lost 10% of your Islands population!");
                        return;
                    case 4:
                        Show("There has been a drought!",
                            "Your citizens don't have water to drink and you have lost 10% of your Islands population!");
                        return;
                    default:
                        return;
                }
            }
        }

        //generate Rng Event for the subscribe
    }
    

    void doCyclone()
    {
        if (activeTornado != null)
        {
            return;
        }

        activeTornado = Instantiate(tornadoPrefab);
        Show("Cyclone hits your Island!",
            "A cyclone ravages through your island killing people and destroying your land");
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