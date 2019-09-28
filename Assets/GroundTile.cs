using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundTile : MonoBehaviour {
    private GameObject groundObject;
	// Use this for initialization
	void Start () {
       
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void SetTile(string modelPath)
    {
        groundObject = (GameObject)Instantiate(Resources.Load(modelPath), Vector3.zero, Quaternion.identity);
        groundObject.transform.parent = this.gameObject.transform;
    }
}
