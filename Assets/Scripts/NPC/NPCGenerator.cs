using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NPC
{
    public class NPCGenerator : MonoBehaviour
    {
        public TextAsset nameData;

        private NameData names;

        private void Awake()
        {
            names = NameData.ParseJson(nameData.text);
            Debug.Log(names.Names.Count);
        }

        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
