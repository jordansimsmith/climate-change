﻿using System;
using System.Runtime.Serialization;
using UnityEngine;
using World.Entities;

namespace World.Resource {
    [CreateAssetMenu, DataContract]
    public class ResourceSingleton : ScriptableObject {
        
        // Ignore the money and cost fields of EntityStats
        [SerializeField, DataMember] public EntityStats totalDemand = new EntityStats();
        [SerializeField, DataMember] public EntityStats totalSupply = new EntityStats();
        
        [DataMember]
        public int Population;
        [DataMember]
        public int Money;
        [DataMember]
        public int MoneyRate;

        // Kms
        public ResourceStat GetResourceBalanceFor(String typeStr)
        {
            Enum.TryParse(typeStr, out ResourceType type);
            return GetResourceBalanceFor(type);
        }
        
        // Kms2
        public ResourceStat GetResourceBalanceFor(ResourceType type)
        {
            var stat = new ResourceStat();
            switch (type)
            {
               case ResourceType.Environment:
                   stat.demand = totalDemand.environment;
                   stat.supply = totalSupply.environment;
                   break;
               case ResourceType.Food:
                   stat.demand = totalDemand.food;
                   stat.supply = totalSupply.food;
                   break;
               case ResourceType.Power:
                   stat.demand = totalDemand.power;
                   stat.supply = totalSupply.power;
                   break;
               case ResourceType.Shelter:
                   stat.demand = totalDemand.shelter;
                   stat.supply = totalSupply.shelter;
                   break;
            }
            return stat;
        }
    }

    public enum ResourceType {
        Shelter,
        Power,
        Food,
        Environment,
    }
    
    public struct ResourceStat
    {
        public int supply;
        public int demand;
    }
}