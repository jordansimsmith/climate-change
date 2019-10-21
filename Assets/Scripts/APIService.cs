using System;
using System.Collections;
using System.Collections.Generic;
using Newtonsoft.Json;
using Persistence.Serializables;
using UnityEngine;
using UnityEngine.Networking;
using World;

namespace DefaultNamespace
{
    /**
        API Client class that interfaces with the API.
     */
    public class APIService : MonoBehaviour
    {
//        private static string BASE_ENDPOINT = "http://localhost:5001/caeliapi/us-east4/api/";
        private static string BASE_ENDPOINT = "https://us-east4-caeliapi.cloudfunctions.net/api/";
        private static string WORLDS_ENDPOINT = BASE_ENDPOINT + "worlds/";
        private static string SHARED_WORLDS_ENDPOINT = BASE_ENDPOINT + "sharedworlds/";

        public string access_token { get; set; }

        private static APIService _instance;

        public static APIService Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = FindObjectOfType<APIService>();
                    if (_instance == null)
                    {
                        GameObject gameObject = new GameObject();
                        gameObject.name = typeof(APIService).Name;
                        _instance = gameObject.AddComponent<APIService>();
                        DontDestroyOnLoad(gameObject);
                    }
                    
                    
                }
                return _instance;
            }
        }
        
        private IEnumerator Get(string url, System.Action<string> callBack)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(url))
            {
                www.SetRequestHeader("Authorization", "Bearer " + access_token );
                yield return www.SendWebRequest();

                if (www.isNetworkError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    if (www.isDone)
                    {
                        string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                        callBack(jsonResult);
                    }
                }
            }
        }
        
        private IEnumerator Post(string url, string data,  System.Action<string> callBack)
        {    
            using (UnityWebRequest www = UnityWebRequest.Post(url, data))
            {
                www.SetRequestHeader("Authorization", "Bearer " + access_token );
                www.SetRequestHeader("Content-Type", "application/json");
                www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(data)); 
                yield return www.SendWebRequest();

                if (www.isNetworkError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    if (www.isDone)
                    {
                        string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                        callBack(jsonResult);
                    }
                    
                    
                }

            }
        }

        private IEnumerator Put(string url, string data)
        {
            using (UnityWebRequest www = UnityWebRequest.Put(url, data))
            {
                www.SetRequestHeader("Authorization", "Bearer " + access_token );
                www.SetRequestHeader("Content-Type", "application/json");
                www.uploadHandler = new UploadHandlerRaw(System.Text.Encoding.UTF8.GetBytes(data));

                yield return www.SendWebRequest();
                
                if (www.isNetworkError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    if (www.isDone)
                    {
                        Debug.Log("done");
                    }
                }    
                
                
            }
            
        }
        private IEnumerator Delete(string url)
        {
            using (UnityWebRequest www = UnityWebRequest.Delete(url))
            {
                www.SetRequestHeader("Authorization", "Bearer " + access_token );
                yield return www.SendWebRequest();

                if (www.isNetworkError)
                {
                    Debug.Log(www.error);
                }
                else
                {
                    if (www.isDone)
                    {
                        Debug.Log("done");
                    }
                }
            }
        }


        public void GetWorlds( System.Action<List<ServerWorld>> callBack)
        {
            StartCoroutine(Get(WORLDS_ENDPOINT, (jsonData) =>
            {
                Debug.Log(jsonData);
                List<ServerWorld> worlds  = JsonConvert.DeserializeObject<List<ServerWorld>>(jsonData);
                callBack(worlds);
            }));
        }

        public void GetSharedWorld(string shareCode, System.Action<ServerWorld> callBack)
        {
            string url = SHARED_WORLDS_ENDPOINT + shareCode;
            StartCoroutine(Get(url, (jsonData) =>
            {
                Debug.Log(jsonData);
                ServerWorld worlds = JsonConvert.DeserializeObject<ServerWorld>(jsonData);
                callBack(worlds);
            }));
        }
        
        private static JsonSerializerSettings serializationSettings = new JsonSerializerSettings
        {
            Formatting = Formatting.None,
            NullValueHandling = NullValueHandling.Ignore
        };

        public void CreateWorld(ServerWorld world, System.Action<string> callBack)
        {
            string json = JsonConvert.SerializeObject(world, serializationSettings);
            Debug.Log(json);
            StartCoroutine(Post(WORLDS_ENDPOINT, json, callBack));
        }

        public void DeleteWorld(string id)
        {
            string url = WORLDS_ENDPOINT + id;
            StartCoroutine(Delete(url));
        }

        public void UpdateWorld(ServerWorld world, string id)
        {
            string url = WORLDS_ENDPOINT + id;
            string json = JsonConvert.SerializeObject(world);
            StartCoroutine(Put(url, json));
        }

        public void CreateShareCode(string id, System.Action<string> callBack)
        {
            string url = WORLDS_ENDPOINT + id + "/sharecode";
            
            // send empty json body
            StartCoroutine(Post(url, "{}", (jsonData) => { callBack(jsonData); }));
        }

        public void DeleteShareCode(string id)
        {
            string url = WORLDS_ENDPOINT + id + "/sharecode";
            StartCoroutine(Delete(url));
        }
    }
}