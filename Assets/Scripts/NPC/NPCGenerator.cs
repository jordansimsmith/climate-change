using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPCGenerator : MonoBehaviour
    {
        public TextAsset nameData;
        public GameObject npcPrefab;

        private NameData names;


        private void Awake()
        {
            names = NameData.ParseJson(nameData.text);
            Debug.Log(names.Names.Count);
        }

        private void Start()
        {
            GenerateNPC();
        }

        public GameObject GenerateNPC()
        {
            GameObject npc = Instantiate(npcPrefab, new Vector3(0, 0, 0), Quaternion.identity);

            NonPlayingCharacter npcScript = npc.GetComponent<NonPlayingCharacter>();

            // TODO: randomise
            npcScript.FirstName = "Reshad";
            npcScript.LastName = "Contractor";
            npcScript.Occupation = "Chief dreaming officer";

            return npc;
        }
    }
}
