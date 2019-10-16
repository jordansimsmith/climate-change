using UnityEngine;
using World.Entities;

namespace Audio
{
    [System.Serializable]
    public class Sound
    {
        [SerializeField] private AudioClip audioClip;
        
        public AudioClip AudioClip => audioClip;

        [HideInInspector]
        public AudioSource source;
    }
}