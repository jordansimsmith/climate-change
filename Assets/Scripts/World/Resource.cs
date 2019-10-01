using System.Collections;
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


  [System.Serializable]
  public class Resource
  {
    public Resource(ResourceType type)
    {
      this.type = type;
    }
    private ResourceType type;
    public ResourceType ResourceType => type;
    public int MinAmount;
    public int CurAmount;
    public int HealthyAmount;
  }
}

