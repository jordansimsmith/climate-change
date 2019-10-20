using UnityEngine;
using World.Entities;

namespace Audio
{
    [System.Serializable]
    public class EntitySound
    {
        // mapping between entity type and build sound
        [SerializeField] private EntityType entityType;
        [SerializeField] private AudioClip audioClip;

        public EntityType EntityType => entityType;
        public AudioClip AudioClip => audioClip;

        [HideInInspector] public AudioSource source;
    }
}