using System;
using System.Collections;
using System.Collections.Generic;
using HUD;
using UnityEngine;
using UnityEngine.UI;
using World.Resource;


public class ResourceView : MonoBehaviour
{

    [SerializeField] private ResourceSingleton resources;
    [SerializeField] private GameController gameController;
    
    // Displays a popup showing current demand and supply of 
    // resource on mouse hover
    [SerializeField] private ResourceStatHint resStatHint; // prefab
    private ResourceStatHint resStatHintInstance; // gameobject

    // Used for flashing warning on low resources
    private bool flipflop;
    private Vector4 normal = new Vector4(1f, 1f, 1f, 1f);
    private Vector4 red = new Vector4(0.5f, 0, 0, 1f);

    private ViewedResource[] viewedResources;
    [SerializeField] private Text populationCounter;
    private Dictionary<String,Transform> sliderTransform;


    //  Takes name of gameObject that encompasses slider
    private ViewedResource getViewedResource(string name) {
        var resourceMaster = gameObject.transform.Find(name).gameObject;
        var slider = resourceMaster.GetComponentInChildren<Slider>();
        sliderTransform[name] = slider.transform;
        return new ViewedResource(slider);
    }
    // Start is called before the first frame update
    void Start()
    {
   
        viewedResources = new ViewedResource[4];
        sliderTransform = new Dictionary<string, Transform>();

        viewedResources[0] = getViewedResource("Power");
        viewedResources[1] = getViewedResource("Environment");
        viewedResources[2] = getViewedResource("Food");
        viewedResources[3] = getViewedResource("Shelter");
       
        InvokeRepeating("TickTenthSecond", 0.1f, 0.1f);
        InvokeRepeating("TickSecond", 1f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        viewedResources[0].SetValues(resources.totalDemand.power, resources.totalSupply.power);
        viewedResources[1].SetValues(resources.totalDemand.environment, resources.totalSupply.environment);
        viewedResources[2].SetValues(resources.totalDemand.food, resources.totalSupply.food);
        viewedResources[3].SetValues(resources.totalDemand.shelter, resources.totalSupply.shelter);

        populationCounter.text = resources.Population.ToString();
    }

    void TickTenthSecond()    {
        foreach (var resource in viewedResources)    {
            resource.Tick(10f);
        }
    }
    
    void TickSecond()    {
        if (flipflop && gameController.Loosing)    {
            gameObject.GetComponent<Image>().color = red;
        } else {
            gameObject.GetComponent<Image>().color = normal;
        }
        flipflop = !flipflop;
    }

    public void ShowResourceStat(String type)
    {
        if (resStatHintInstance == null)
        {
            resStatHintInstance = Instantiate(resStatHint);
        }
        var stat = resources.GetResourceBalanceFor(type);
        resStatHintInstance.transform.SetParent(sliderTransform[type], false);
        resStatHintInstance.ShowHint(stat);
    }

    public void HideResourceStat()
    {
        resStatHintInstance.HideHint();
    }
   
}

public class ViewedResource    {
    private Slider slider;
    private Image sliderColor;
    private int desired;
    private int current;

    // Used for pretty moving of sliders
    private float showingVelocity;

    private static float maxDeltaVelocity = 0.4f; 
    private static float maxDeltaAcceleration = 0.025f; 

    public ViewedResource(Slider slider)    {
        this.slider = slider;

        this.sliderColor = slider.transform.Find("Fill Area").GetComponentInChildren<Image>();
    }

    // Sets the desired number of a resource, and the current number the player has
    // The slider will adjust overtime to represent these numbers
    public void SetValues(int desired, int current)  {
        this.desired = desired;
        this.current = current;
    }

    public void Tick(float ticksPerSecond)   {
        float targetAmount;
        if (desired == 0 && current == 0)   {
            targetAmount = 0.5f;
        } else if (desired == 0) {
            targetAmount = 1f;
        } else {
            targetAmount = current/desired/2f;
        }

        if (targetAmount > 1f)  {
            targetAmount = 1f;
        }

        if (targetAmount > (slider.value + 0.05f)) {
            showingVelocity += maxDeltaAcceleration/ticksPerSecond;
            if (showingVelocity > maxDeltaVelocity/ticksPerSecond)  {
                showingVelocity = maxDeltaVelocity/ticksPerSecond;
            }
        } else if (targetAmount < (slider.value - 0.05f)) {
            showingVelocity -= maxDeltaAcceleration/ticksPerSecond;
            if (showingVelocity < -maxDeltaVelocity/ticksPerSecond)  {
                showingVelocity = -maxDeltaVelocity/ticksPerSecond;
            }
        } else {
            showingVelocity = 0f;
        }

        slider.value += showingVelocity/4f;
        
        this.SetColor(slider.value);
    }

    private void SetColor(float percentFull)    {
        ColorBlock cb = ColorBlock.defaultColorBlock;
        Color color;
        if (percentFull < 0.5f) {
            percentFull = percentFull * 2f;
            color = Color.Lerp(new Color(1f, 0, 0), new Color(0, 0.5f, 0.1f), percentFull);
        } else {
            percentFull = percentFull * 2f - 1f;
            color = Color.Lerp(new Color(0, 0.5f, 0.1f), new Color(1f, 0.9f, 0f), percentFull);
        }
        cb.disabledColor = color;
        cb.highlightedColor = color;
        cb.normalColor = color;
        cb.pressedColor = color;
        cb.selectedColor = color;
        slider.colors = cb;

        sliderColor.color = color;
    }
}