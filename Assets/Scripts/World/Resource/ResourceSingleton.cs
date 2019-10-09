﻿using UnityEngine;
using World.Entities;

namespace World.Resource {
    [CreateAssetMenu]
    public class ResourceSingleton : ScriptableObject {
//        [SerializeField] private Resource money = new Resource(ResourceType.Money);
//        public Resource Money => money;
        [SerializeField] private Resource food = new Resource(ResourceType.Food);
        public Resource Food => food;
        [SerializeField] private Resource shelter = new Resource(ResourceType.Shelter);
        public Resource Shelter => shelter;
        [SerializeField] private Resource environment = new Resource(ResourceType.Environment);
        public Resource Environment => environment;
        [SerializeField] private Resource power = new Resource(ResourceType.Power);
        public Resource Power => power;

        [SerializeField] public EntityStats totalDemand = new EntityStats();
        
        [SerializeField] public EntityStats totalSupply = new EntityStats();

        public int Population;

        public int Money;
        public int MoneyRate;
    }
}