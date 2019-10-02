using System;
using System.Collections.Generic;
using UnityEngine;

namespace World
{
  public enum ResourceType
  {
    Money,
    Shelter,
    Power,
    Food,
    Environment
  }

  public enum Threshold
  {
    Zero = 0, // min
    TwentyFive = 25,
    Fifty = 50,
    SeventyFive = 75,
    Hundred = 100 // healthy
  }

  public delegate void ResourceEventHandler(ResourceEvent e);

  [Serializable]
  public class Resource
  {
    [SerializeField] private int minAmount;
    [SerializeField] private int curAmount;
    [SerializeField] private int healthyAmount;
    private readonly ResourceType type;

    private readonly List<ResourceEventHandler> subscribers =
        new List<ResourceEventHandler>();

    public Resource(ResourceType type)
    {
      this.type = type;
    }

    public ResourceType ResourceType => type;

    public int MinAmount
    {
      get => minAmount;
      set
      {
        minAmount = value < 0 ? 0 : value;
        if (minAmount >= healthyAmount) healthyAmount = minAmount + 1;
      }
    }

    public int CurAmount
    {
      get => curAmount;
      set
      {
        int oldAmount = curAmount;
        int oldPercentage = CurPercentage;
        curAmount = value < 0 ? 0 : value;
        if (curAmount == oldAmount)
        {
          return;
        }

        if (subscribers.Count == 0)
        {
          return;
        }

        foreach (Threshold t in Enum.GetValues(typeof(Threshold)))
        {
          int thresh = (int)t;
          if (oldPercentage < thresh && CurPercentage > thresh ||
              oldPercentage > thresh && CurPercentage < thresh)
          {
            var resourceEvent = new ResourceEvent(curAmount > oldAmount, thresh);
            foreach (var handler in subscribers)
            {
              handler.Invoke(resourceEvent);
            }
          }
        }
      }
    }

    public int CurPercentage =>
        (int)Math.Round((double)((curAmount - minAmount) * 100) / (healthyAmount - minAmount));

    public int HealthyAmount
    {
      get => healthyAmount;
      set => healthyAmount = value <= minAmount ? minAmount + 1 : value;
    }

    public void Subscribe(ResourceEventHandler handler)
    {
      subscribers.Add(handler);
    }
  }
}