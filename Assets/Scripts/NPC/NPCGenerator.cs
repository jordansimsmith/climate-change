using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPCGenerator : MonoBehaviour
    {
        public TextAsset nameData;
        public TextAsset occupationData;
        public GameObject npcPrefab;
        public int npcCount = 3;

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
            for (int i = 0; i < npcCount; i++)
            {
                GenerateNPC();
            }
        }

        public GameObject GenerateNPC()
        {
            int x = Random.Range(85, 95);
            int y = 0;
            int z = Random.Range(85, 95);
            GameObject npc = Instantiate(npcPrefab, new Vector3(x, y, z), Quaternion.identity);

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