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
    public class APIService : MonoBehaviour
    {
        private static string BASE_ENDPOINT = "https://us-east4-caeliapi.cloudfunctions.net/api/";
        private static string WORLDS_ENDPOINT = BASE_ENDPOINT + "worlds/";
        private static string SHARED_WORLDS_ENDPOINT = BASE_ENDPOINT + "sharedworlds/";

        public string access_token { get; set; } =
            "eyJhbGciOiJSUzI1NiIsImtpZCI6ImZhMWQ3NzBlZWY5ZWFhNjU0MzY1ZGE5MDhjNDIzY2NkNzY4ODkxMDUiLCJ0eXAiOiJKV1QifQ.eyJwcm92aWRlcl9pZCI6ImFub255bW91cyIsImlzcyI6Imh0dHBzOi8vc2VjdXJldG9rZW4uZ29vZ2xlLmNvbS9jYWVsaWFwaSIsImF1ZCI6ImNhZWxpYXBpIiwiYXV0aF90aW1lIjoxNTcxNDU0NDY0LCJ1c2VyX2lkIjoiRTc3RHZCUllIRFI2YVVjcWszZDQ0c1pTRGhxMSIsInN1YiI6IkU3N0R2QlJZSERSNmFVY3FrM2Q0NHNaU0RocTEiLCJpYXQiOjE1NzE0NTQ0NjQsImV4cCI6MTU3MTQ1ODA2NCwiZmlyZWJhc2UiOnsiaWRlbnRpdGllcyI6e30sInNpZ25faW5fcHJvdmlkZXIiOiJhbm9ueW1vdXMifX0.RdwQidKhqG-xIWqrM6ZL2-wQij3MUGR4r3Fyx-kMaPtLRmnWX8NwmCCQ5pvlTvO34Y2mLYrWsbbTxqPzsDfrrEWtqCBzd3J9sgHg4El5rlQdWD0Z7tQ8czQ8fp_M1CJCzcwzVoIQtattMp9vf5wHDmPdu9dhLVo_8o0LYqRIcrUH5c06gi6nA1fQR6qo9RkpTChwPYWgxUyXPG6smmSU3p8_EMrQVX1f84fl4P36spRHAvZf-VSqZ2liqio1y00pxi2lUywMs9k4FT2D1PAuKA9Nx_cc48gGabjf09do01JuT2WNb4NPORcZLA-VHL6VtVJblDbDX0-ka-08FbP_jA";


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
                        Debug.Log(jsonResult);
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
                www.uploadHandler.contentType = "application/json";
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
                www.uploadHandler.contentType = "application/json";
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


        public void GetWorlds( System.Action<List<DbWorld>> callBack)
        {
            StartCoroutine(Get(WORLDS_ENDPOINT, (jsonData) =>
            {
                Debug.Log(jsonData);
                List<DbWorld> worlds  = JsonConvert.DeserializeObject<List<DbWorld>>(jsonData);
                callBack(worlds);
            }));
        }

        public void GetSharedWorlds(string shareCode, System.Action<List<DbWorld>> callBack)
        {
            string url = SHARED_WORLDS_ENDPOINT + shareCode;
            StartCoroutine(Get(url, (jsonData) =>
            {
                List<DbWorld> worlds = JsonConvert.DeserializeObject<List<DbWorld>>(jsonData);
                callBack(worlds);
            }));
        }

        public void CreateWorld(DbWorld world, System.Action<string> callBack)
        {
            string json = JsonConvert.SerializeObject(world);
            Debug.Log(json);
            StartCoroutine(Post(WORLDS_ENDPOINT, json, callBack));
        }

        public void DeleteWorld(string id)
        {
            string url = WORLDS_ENDPOINT + id;
            StartCoroutine(Delete(url));
        }

        public void UpdateWorld(DbWorld world, string id)
        {
            string url = WORLDS_ENDPOINT + id;
            string json = JsonConvert.SerializeObject(world);
            StartCoroutine(Put(url, json));
        }

        public void CreateShareCode(string id, System.Action<string> callBack)
        {
            string url = WORLDS_ENDPOINT + id + "/sharecode";
            StartCoroutine(Post(url, "", (jsonData) => { callBack(jsonData); }));
        }

        public void DeleteShareCode(string id)
        {
            string url = WORLDS_ENDPOINT + id + "/sharecode";
            StartCoroutine(Delete(url));
        }
    }
}