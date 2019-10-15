using UnityEngine;
using World.Entities;

namespace Audio
{
    [System.Serializable]
    public class EntitySound
    {
        [SerializeField] private EntityType entityType;
        [SerializeField] private AudioClip audioClip;
        
        public EntityType EntityType => entityType;
        public AudioClip AudioClip => audioClip;

        [HideInInspector]
        public AudioSource source;
    }
}