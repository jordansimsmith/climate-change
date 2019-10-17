using System;
using UnityEngine;

namespace World.Entities
{

    [CreateAssetMenu]
    public class EntityUpgradeInfo : ScriptableObject {
        [Header("Elements of array correspond to levels of the Entity")]
        [SerializeField] private LevelInfo[] levelInfo;

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
    public struct LevelInfo {
        [SerializeField] private EntityStats baseStats;
        public EntityStats BaseStats => baseStats;
        
        [SerializeField] private ResearchOption[] researchOptions;
        public ResearchOption[] ResearchOptions => researchOptions;
    }

    [Serializable]
    public struct ResearchOption {
        [SerializeField] private string name;
        public String Name => name;
        
        [SerializeField] private EntityStats researchDiff;
        public EntityStats ResearchDiff => researchDiff;
        
        [NonSerialized] public bool isResearched;
    }
}