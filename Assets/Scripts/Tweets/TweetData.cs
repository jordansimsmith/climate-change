using System.Collections.Generic;
using UnityEngine;

namespace Tweets
{
    [System.Serializable]
    public class TweetData
    {
        [SerializeField] private TweetLevels food;
        [SerializeField] private TweetLevels power;
        [SerializeField] private TweetLevels shelter;
        [SerializeField] private TweetLevels environment;

        public TweetLevels Food => food;
        public TweetLevels Power => power;
        public TweetLevels Shelter => shelter;
        public TweetLevels Environment => environment;

        /// <summary>
        /// ParseJson deserializes the provided JSON string into a TweetData object.
        /// </summary>
        /// <param name="json">JSON string</param>
        /// <returns>Deserialized TweetData object</returns>
        public static TweetData ParseJson(string json)
        {
            return JsonUtility.FromJson<TweetData>(json);
        }
    }

    [System.Serializable]
    public class TweetLevels
    {
        [SerializeField] private List<string> veryBad;
        [SerializeField] private List<string> bad;
        [SerializeField] private List<string> good;
        [SerializeField] private List<string> veryGood;

        public List<string> VeryBad => veryBad;
        public List<string> Bad => bad;
        public List<string> Good => good;
        public List<string> VeryGood => veryGood;
    }
}