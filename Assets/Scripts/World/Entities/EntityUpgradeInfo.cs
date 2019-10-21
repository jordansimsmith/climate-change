using System;
using UnityEngine;

namespace World.Entities
{

    /// <summary>
    /// Serializes the resource contributions and upgrade options for each entity
    /// for different levels.
    /// </summary>
    [CreateAssetMenu]
    public class EntityUpgradeInfo : ScriptableObject {
        [Header("Elements of array correspond to levels of the Entity")]
        [SerializeField] private LevelInfo[] levelInfo;
        [SerializeField] private String description;
        public String Description => description;

        public LevelInfo GetLevel(int level) {
            if (level > levelInfo.Length) {
                Debug.LogError("level doesn't exist lmao\n This is a big fucky wucky");
                return new LevelInfo();
            }
            return levelInfo[level - 1];
        }

        public int NumberOfLevels => levelInfo.Length;
    }
    
    
    [Serializable]
    public class LevelInfo {
        [SerializeField] private EntityStats baseStats;
        [SerializeField] private ResearchOption[] researchOptions;
        public ResearchOption[] ResearchOptions => researchOptions;
        public EntityStats BaseStats => baseStats;
    }

    [Serializable]
    public class ResearchOption {
        [SerializeField] private string name;
        [SerializeField] private EntityStats researchDiff;
        [NonSerialized] public bool isResearched;
        public String Name => name;
        public EntityStats ResearchDiff => researchDiff;
    }
}