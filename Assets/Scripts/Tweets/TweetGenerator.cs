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
        /// Generates a random tweet based on the current game resource levels. Resource levels should be
        /// normalised. i.e. 0.0 to 1.0.
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
            // ensure level is normalised
            if (level < 0.0f || level > 1.0f)
            {
                throw new ArgumentException("resource percentage is < 0 or > 1");
            }

            // determine which category of tweet to return
            switch (level)
            {
                case float l when l <= 0.25f:
                    List<string> veryBadTweets = tweetLevels.Bad;
                    return veryBadTweets[Random.Range(0, veryBadTweets.Count)];
                case float l when l <= 0.5f:
                    List<string> badTweets = tweetLevels.Bad;
                    return badTweets[Random.Range(0, badTweets.Count)];
                case float l when l <= 0.75f:
                    List<string> goodTweets = tweetLevels.Bad;
                    return goodTweets[Random.Range(0, goodTweets.Count)];
                case float l when l <= 1.0f:
                    List<string> veryGoodTweets = tweetLevels.Bad;
                    return veryGoodTweets[Random.Range(0, veryGoodTweets.Count)];
                default:
                    return null;
            }
        }
    }
}