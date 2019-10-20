using System;
using System.Linq;
using Audit;
using Boo.Lang;
using UnityEngine;
using World.Entities;
using World.Resource;

namespace HUD
{
    public class EwanAuditGenerator: MonoBehaviour
    {
        public TextAsset auditData;
        private AuditData audit;
        
        [SerializeField] private ResourceSingleton resources;
        private static EwanAuditGenerator _instance;
        public static EwanAuditGenerator Instance => _instance;

        private void Awake()
        {
            audit = AuditData.ParseJson(auditData.text);
            _instance = this;
        }
        
        public string[] GenerateAuditText()
        {
            List<string> dialogue = new List<string>();

            dialogue.Add(
                "Congratulations on upgrading your Town Hall to Level Two. You can now upgrade all your entities to Level Two as well.");
            dialogue.Add("Now that I'm here, I might as well take a look at your island and resources to give you some feedback on how your going !");
            
            var resourceTypes = Enum.GetValues(typeof(ResourceType)).Cast<ResourceType>();

            foreach (ResourceType resource in resourceTypes)
            {
                ResourceStat stat = resources.GetResourceBalanceFor(resource);

                int supply = stat.supply;
                int demand = stat.demand;

                int overall = supply - demand;

                AuditLevels auditLevels = null;
                
                switch (resource)
                {
                    case ResourceType.Environment:
                        auditLevels = audit.Environment;
                        break;
                    case ResourceType.Food:
                        auditLevels = audit.Food;
                        break;
                    case ResourceType.Power:
                        auditLevels = audit.Power;
                        break;
                    case ResourceType.Shelter:
                        auditLevels = audit.Shelter;
                        break;
                }

                string resourceDialogue = GetAuditForResource(auditLevels, overall);
                dialogue.Add(resourceDialogue);
            }

            dialogue.Add(
                "I'll be off now. Good luck on your journey in maintaining a healthy, prosperous and sustainable island. Remember, once you hit Level Three, you can unlock some special entities...");


            return dialogue.ToArray();
        }

        private String GetAuditForResource(AuditLevels auditLevels, float level)
        {
            switch (level)
            {
                case float l when l <= -30f:
                   return auditLevels.VeryBad;
                case float l when l <= 0f:
                    return auditLevels.Bad;
                case float l when l <= 50f:
                    return auditLevels.Good;
                default:
                    return auditLevels.VeryGood;
            }
            
        }
        
        
    } 
}