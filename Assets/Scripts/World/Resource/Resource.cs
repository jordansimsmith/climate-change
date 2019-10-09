using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace World.Resource {
    public enum ResourceType {
        Money,
        Shelter,
        Power,
        Food,
        Environment,
        Population
    }

    public enum Threshold {
        Yeet4 = -100,
        Yeet3 = -75,
        Yeet2 = -50,
        Yeet = -25,
        Zero = 0,
        TwentyFive = 25,
        Fifty = 50,
        SeventyFive = 75,
        Hundred = 100
    }

    public delegate void ResourceEventHandler(ResourceEvent e);

    [Serializable]
    public class Resource {
        /// <summary>
        /// minAmount is always less than 0.
        /// 
        /// If a resource falls below minAmount, the player risks losing.
        /// </summary>
        [SerializeField] private int minAmount;

        /// <summary>
        /// curAmount greater than zero means resource surplus.
        /// curAmount less than zero means resource deficiency.
        /// zero is considered the middle point.
        /// </summary>
        [SerializeField] private int curAmount;

        private readonly ResourceType type;

        private readonly List<ResourceEventHandler> subscribers =
            new List<ResourceEventHandler>();

        public Resource(ResourceType type) {
            this.type = type;
        }

        public ResourceType ResourceType => type;

        public int MinAmount {
            get => minAmount;
            set {
                if (minAmount >= 0) {
                    Debug.Log("Min amount should be less than zero");
                }
                minAmount = value;
            }
        }

        public int CurAmount {
            get => curAmount;
            set {
                int oldAmount = curAmount;
                int oldPercentage = CurPercentage;
                curAmount = value;
                if (curAmount == oldAmount) {
                    return;
                }
                if (subscribers.Count == 0) {
                    return;
                }
                foreach (Threshold t in Enum.GetValues(typeof(Threshold))) {
                    int thresh = (int) t;
                    if (oldPercentage < thresh && CurPercentage > thresh ||
                        oldPercentage > thresh && CurPercentage < thresh) {
                        var resourceEvent = new ResourceEvent(curAmount > oldAmount, thresh);
                        foreach (var handler in subscribers) {
                            handler.Invoke(resourceEvent);
                        }
                    }
                }
            }
        }

        public int CurPercentage => (int) Math.Round((double) (curAmount * 100) / Math.Abs(minAmount));

        public void Subscribe(ResourceEventHandler handler) {
            subscribers.Add(handler);
        }
    }
}