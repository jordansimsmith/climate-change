using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class AuthHandler : MonoBehaviour
{
    
    [DllImport("__Internal")]
    private static extern void OpenAuthUI();
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #if UNITY_WEBGL
    public void OpenUI() {
        OpenAuthUI();
    }

    #else
    public void OpenUI() {
        // Login anonymously
    }
    #endif
}
