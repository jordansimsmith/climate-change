using System;
using UnityEngine;
using World.Tiles;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        // singleton instance to prevent duplication
        private static AudioManager _instance;
        public static AudioManager Instance => _instance;

        public EntitySound[] sounds;
        public Sound conversationSound;
        private bool conversationRunning;

        private void Awake()
        {
            // set singleton instance
            _instance = this;

            foreach (EntitySound s in sounds)
            {
                s.source = gameObject.AddComponent<AudioSource>();
                s.source.clip = s.AudioClip;
            }
            
            conversationSound.source = gameObject.AddComponent<AudioSource>();
            conversationSound.source.clip = conversationSound.AudioClip;
            conversationSound.source.volume = 0.6f;
        }

        public void Play(GameObject gameTile)
        {
            // find the appropriate sound for the tile placed
            Tile tile = gameTile.GetComponent<Tile>();
            EntitySound s = Array.Find(sounds, sound => sound.EntityType == tile.Entity.Type);

            // play sound
            s?.source.Play();
        }

        public void StartConversation()
        {
            // used when interacting with NPCs
            if (!conversationRunning)
            {
                conversationRunning = true;
                conversationSound.source.Play();
            }
        }

        public void EndConversation()
        {
            // stops conversation sounds
            if (conversationRunning)
            {
                conversationRunning = false;
                conversationSound.source.Stop();
            }
        }
    }
}