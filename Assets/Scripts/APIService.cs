using System.Collections;
using System.Collections.Generic;
using Persistence.Serializables;
using UnityEngine;
using UnityEngine.Networking;

namespace DefaultNamespace
{
    public class APIService : MonoBehaviour
    {
        private static string endpoint = "";

        public static APIService _instance;

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

        public IEnumerator Get(string url)
        {
            using (UnityWebRequest www = UnityWebRequest.Get(url))
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
                        string jsonResult = System.Text.Encoding.UTF8.GetString(www.downloadHandler.data);
                        Debug.Log(jsonResult);
                    }
                }
            }
        }


//        public List<SerializableWorld> GetWorlds()
//        {
//            UnityWebRequest www = UnityWebRequest.Get;
//        }
    }
}