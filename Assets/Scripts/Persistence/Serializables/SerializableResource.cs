using System;
using World.Entities;
using World.Resource;

namespace Persistence.Serializables
{
    [Serializable]
    public class SerializableResource
    {

        private int money;
        private int population;
        private EntityStats totalDemand;
        private EntityStats totalSupply;
        public SerializableResource()
        {
        }

        public SerializableResource(ResourceSingleton resources)
        {
            Money = resources.Money;
            Population = resources.Population;
            TotalDemand = resources.totalDemand;
            TotalSupply = resources.totalSupply;
        }

        public int Money
        {
            get => money;
            set => money = value;
        }

        public int Population
        {
            get => population;
            set => population = value;
        }

        public EntityStats TotalDemand
        {
            get => totalDemand;
            set => totalDemand = value;
        }

        public EntityStats TotalSupply
        {
            get => totalSupply;
            set => totalSupply = value;
        }
    }
}