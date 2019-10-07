using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using World.Resource;


public class ResourceView : MonoBehaviour
{

    [SerializeField] private ResourceSingleton resources;

    public Text[] resourceUsage;
//    private ViewedResource[] viewedResources;

    // Takes name of gameObject that encompasses slider
//    private ViewedResource getViewedResource(string name) {
//        var resourceMaster = gameObject.transform.Find(name).gameObject;
//        return new ViewedResource(resourceMaster.GetComponentInChildren<Slider>());
//    }

    private Text getViewedResource(string name)
    {
        var resourceMaster = gameObject.transform.Find(name);
        return resourceMaster.GetComponentInChildren<Text>();
    }

    
    
    // Start is called before the first frame update
    void Start()
    {
   
//        viewedResources = new ViewedResource[4];
//
//        resourceUsage[0] = getViewedResource("Electricity");
//        resourceUsage[1] = getViewedResource("Ecosystem");
//        resourceUsage[2] = getViewedResource("Food");
//        resourceUsage[3] = getViewedResource("Shelter");
//        
        InvokeRepeating("TickTenthSecond", 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
//        viewedResources[0].SetValues(resources.Power.MinAmount, resources.Power.CurAmount);
//        viewedResources[1].SetValues(resources.Environment.MinAmount, resources.Environment.CurAmount);
//        viewedResources[2].SetValues(resources.Food.MinAmount, resources.Food.CurAmount);
//        viewedResources[3].SetValues(resources.Shelter.MinAmount, resources.Shelter.CurAmount);
        resourceUsage[0].text = resources.Power.CurAmount.ToString();
        resourceUsage[1].text = resources.Environment.CurAmount.ToString();
        resourceUsage[2].text = resources.Food.CurAmount.ToString();
        resourceUsage[3].text = resources.Shelter.CurAmount.ToString();
    }

    void TickTenthSecond()    {
//        foreach (var resource in viewedResources)    {
//            resource.Tick(10f);
//        }
    }
}

class ViewedResource    {
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
        float targetAmount = current/desired; 
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

        //slider.value += showingVelocity/4f;
        slider.value = targetAmount;
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