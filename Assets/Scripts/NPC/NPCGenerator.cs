using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPCGenerator : MonoBehaviour
    {
        public TextAsset nameData;
        public TextAsset occupationData;
        public GameObject npcPrefab;

        private NameData names;
        private OccupationData occupations;


        private void Awake()
        {
            names = NameData.ParseJson(nameData.text);
            occupations = OccupationData.ParseJson(occupationData.text);
            Debug.Log(names.Names.Count);
            Debug.Log(occupations.Occupations.Count);
        }

        private void Start()
        {
            GenerateNPC();
        }

        public GameObject GenerateNPC()
        {
            GameObject npc = Instantiate(npcPrefab, new Vector3(0, 0, 0), Quaternion.identity);

            NonPlayingCharacter npcScript = npc.GetComponent<NonPlayingCharacter>();

            npcScript.FirstName = GetRandomFromList(names.Names);
            npcScript.LastName = GetRandomFromList(names.Names);
            npcScript.Occupation = GetRandomFromList(occupations.Occupations);

            return npc;
        }

        private string GetRandomFromList(List<string> list)
        {
            return list[Random.Range(0, list.Count)];
        }
    }
}
