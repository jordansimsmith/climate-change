using System;
using UnityEngine;
using World.Tiles;

namespace Audio
{
   public class AudioManager : MonoBehaviour
   {
      private static AudioManager _instance;
      public static AudioManager Instance
      {
         get
         {
            if (_instance == null)
            {
               _instance = FindObjectOfType<AudioManager>();
               if (_instance == null)
               {
                  _instance = new GameObject("Spawned AudioManager", typeof(AudioManager)).GetComponent<AudioManager>();
               }
            }

            return _instance;
         }

         private set { _instance = value; }
      }

      public EntitySound[] sounds;
      public Sound conversationSound;
      private bool conversationRunning;

      private void Awake()
      {
         foreach (var s in sounds)
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
         Tile tile = gameTile.GetComponent<Tile>();
         EntitySound s = Array.Find(sounds, sound => sound.EntityType == tile.Entity.Type);
         if (s != null)   {
            s.source.Play();
         }
      }

      public void StartConversation()
      {
         if (!conversationRunning)  {
            conversationRunning = true;
            conversationSound.source.Play();
         }
      }
      
      public void EndConversation()
      {
         if (conversationRunning)  {
            conversationRunning = false;
            conversationSound.source.Stop();
         }
      }

   }
}

