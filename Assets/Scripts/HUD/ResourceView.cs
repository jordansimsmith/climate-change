using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class ResourceView : MonoBehaviour
{
    private ViewedResource[] viewedResources;

    // Takes name of gameObject that encompasses slider
    private ViewedResource getViewedResource(string name) {
        var resourceMaster = gameObject.transform.Find(name).gameObject;
        return new ViewedResource(resourceMaster.GetComponentInChildren<Slider>());
    }

    // Start is called before the first frame update
    void Start()
    {
        viewedResources = new ViewedResource[4];

        viewedResources[0] = getViewedResource("Electricity");
        viewedResources[0].SetValues(4, 10);
        viewedResources[1] = getViewedResource("Ecosystem");
        viewedResources[1].SetValues(20, 10);
        viewedResources[2] = getViewedResource("Food");
        viewedResources[2].SetValues(10, 4);
        viewedResources[3] = getViewedResource("Shelter");
        viewedResources[3].SetValues(1, 10);

        InvokeRepeating("TickTenthSecond", 0.1f, 0.1f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void TickTenthSecond()    {
        foreach (var resource in viewedResources)    {
            resource.Tick(10f);
        }
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
        float targetAmount = current/desired/2f; 
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