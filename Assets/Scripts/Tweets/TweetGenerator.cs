using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Tweets
{
    public class TweetGenerator : MonoBehaviour
    {
        public TextAsset tweetData;

        private TweetData tweets;

        private static TweetGenerator instance;

        // singleton instance as to only load the JSON once
        public static TweetGenerator Instance => instance;

        private void Awake()
        {
            // load tweet data from JSON file
            tweets = TweetData.ParseJson(tweetData.text);

            instance = this;
        }

        /// <summary>
        /// Generates a random tweet based on the current game resource levels. 
        /// </summary>
        /// <param name="food">food resource level</param>
        /// <param name="power">power resource level</param>
        /// <param name="shelter">shelter resource level</param>
        /// <param name="environment">environment resource level</param>
        /// <returns>Tweet</returns>
        public string GenerateTweet(float food, float power, float shelter, float environment)
        {
            // pick which resource the tweet should focus on
            switch (Random.Range(0, 4))
            {
                case 0:
                    return PickRandomTweet(tweets.Food, food);
                case 1:
                    return PickRandomTweet(tweets.Power, power);
                case 2:
                    return PickRandomTweet(tweets.Shelter, shelter);
                case 3:
                    return PickRandomTweet(tweets.Environment, environment);
                default:
                    return null;
            }
        }

        private string PickRandomTweet(TweetLevels tweetLevels, float level)
        {
            // determine which category of tweet to return
            switch (level)
            {
                case float l when l <= -30f:
                    List<string> veryBadTweets = tweetLevels.VeryBad;
                    return veryBadTweets[Random.Range(0, veryBadTweets.Count)];
                case float l when l <= 0f:
                    List<string> badTweets = tweetLevels.Bad;
                    return badTweets[Random.Range(0, badTweets.Count)];
                case float l when l <= 50f:
                    List<string> goodTweets = tweetLevels.Good;
                    return goodTweets[Random.Range(0, goodTweets.Count)];
                default:
                    List<string> veryGoodTweets = tweetLevels.VeryGood;
                    return veryGoodTweets[Random.Range(0, veryGoodTweets.Count)];
            }
        }
    }
}