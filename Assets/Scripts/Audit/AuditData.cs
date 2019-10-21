using UnityEngine;

namespace Audit
{
    [System.Serializable]
    public class AuditData
    {
        [SerializeField] private AuditLevels food;
        [SerializeField] private AuditLevels power;
        [SerializeField] private AuditLevels shelter;
        [SerializeField] private AuditLevels environment;
        
        public AuditLevels Food => food;
        public AuditLevels Power => power;
        public AuditLevels Shelter => shelter;
        public AuditLevels Environment => environment;

        public static AuditData ParseJson(string json)
        {
            return JsonUtility.FromJson<AuditData>(json);
        }
    }

    [System.Serializable]
    public class AuditLevels
    {
        [SerializeField] private string veryBad;
        [SerializeField] private string bad;
        [SerializeField] private string good;
        [SerializeField] private string veryGood;
        
        public string VeryBad => veryBad;
        public string Bad => bad;
        public string Good => good;
        public string VeryGood => veryGood;

    }
}