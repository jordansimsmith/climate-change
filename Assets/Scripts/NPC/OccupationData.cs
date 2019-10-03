using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    [System.Serializable]
    public class OccupationData
    {
        [SerializeField] private List<string> occupations;

        public List<string> Occupations => occupations;

        public static OccupationData ParseJson(string json)
        {
            return JsonUtility.FromJson<OccupationData>(json);
        }
    }
}