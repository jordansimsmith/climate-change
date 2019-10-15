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

      public Sound[] sounds;

      private void Awake()
      {
         foreach (var s in sounds)
         {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.AudioClip;
         }
      }

      public void Play(GameObject gameTile)
      {
         Tile tile = gameTile.GetComponent<Tile>();
         Sound s = Array.Find(sounds, sound => sound.EntityType == tile.Entity.Type);
         if (s != null)   {
            s.source.Play();
         }
      }

   }
}

