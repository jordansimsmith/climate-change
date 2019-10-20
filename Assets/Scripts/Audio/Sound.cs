using UnityEngine;
using World.Entities;

namespace Audio
{
    [System.Serializable]
    public class Sound
    {
        [HideInInspector] public AudioSource source;

        [SerializeField] private AudioClip audioClip;
        public AudioClip AudioClip => audioClip;
    }
}