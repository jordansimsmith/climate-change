using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace World
{
  [CreateAssetMenu]
  public class ResourceSingleton : ScriptableObject
  {

    [SerializeField]
    private Resource money = new Resource(ResourceType.Money);

    [SerializeField]
    private Resource food = new Resource(ResourceType.Food);

    [SerializeField]
    private Resource shelter = new Resource(ResourceType.Shelter);

    [SerializeField]
    private Resource environment = new Resource(ResourceType.Environment);

    [SerializeField]
    private Resource power = new Resource(ResourceType.Power);

    public delegate void ResourceEventHandler(ResourceEvent e);

    private Dictionary<ResourceType, List<ResourceEventHandler>> subscribers = default;

    public void incrementValue(ResourceType type, int value)
    {
      Resource res = get(type);
      int oldAmount = res.CurAmount;
      int newAmount = res.CurAmount + value;

      List<ResourceEventHandler> handlers = this.subscribers[type];

      var values = System.Enum.GetValues(typeof(Threshold));

      float range = res.HealthyAmount - res.MinAmount;

      int newThreshold = Mathf.RoundToInt(((newAmount - res.MinAmount) / range) * 100);
      int oldThreshold = Mathf.RoundToInt(((oldAmount - res.MinAmount) / range) * 100);

      foreach (Threshold t in values)
      {
        // if ((newThreshold > (int)t && oldThreshold < (int)t) || (newThreshold < (int)t && oldThreshold > (int)t))
        // {


        // }




      }

      res.CurAmount = newAmount;
    }

    public int getCurValue(ResourceType type)
    {
      Resource res = get(type);
      return res.CurAmount;
    }

    public int getMinValue(ResourceType type)
    {
      Resource res = get(type);
      return res.MinAmount;
    }
    public int getHealthyValue(ResourceType type)
    {
      Resource res = get(type);
      return res.HealthyAmount;
    }

    private Resource get(ResourceType type)
    {
      switch (type)
      {
        case ResourceType.Environment:
          return environment;
        case ResourceType.Food:
          return food;
        case ResourceType.Money:
          return money;
        case ResourceType.Shelter:
          return shelter;
        case ResourceType.Power:
          return power;
        default:
          Debug.Log("Resource type not found");
          return null;

      }
    }


    public void Subscribe(ResourceType resourceType, ResourceEventHandler handler)
    {
      List<ResourceEventHandler> handlers = this.subscribers[resourceType];
      if (handlers == null)
      {
        handlers = new List<ResourceEventHandler>();
      }

      handlers.Add(handler);

      this.subscribers.Add(resourceType, handlers);

    }





  }

}

public enum Threshold
{
  ZERO = 0, // min
  TWENTY_FIVE = 25,

  FIFTY = 50,
  SEVENTY_FIVE = 75,

  HUNDRED = 100 // healthy
}

