using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    [System.Serializable]
    public class NameData
    {
        [SerializeField] private List<string> names;

        public List<string> Names => names;

        public static NameData ParseJson(string json)
        {
            return JsonUtility.FromJson<NameData>(json);
        }
    }
}