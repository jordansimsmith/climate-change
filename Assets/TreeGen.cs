using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeGen : MonoBehaviour {
	public GameObject[] trees;
	private int numTrees;
	public float treeScale;

	// Use this for initialization
	void Start () {
		numTrees = Random.Range (0, 10);

		foreach (var tree in trees)	{
			tree.transform.localScale = new Vector3(treeScale, treeScale, treeScale);
		}

		var grid = FindObjectOfType<Grid>();
		var unit = gameObject.transform.parent.gameObject.GetComponent<MeshFilter> ().sharedMesh.bounds.size.x * gameObject.transform.parent.lossyScale.x;

		for (int i = 0; i < numTrees; i++) {
			var tree = (GameObject)Instantiate (trees [Random.Range (0, trees.Length)]);
			tree.transform.parent = gameObject.transform;	

			tree.transform.localPosition = new Vector3 (Random.Range (0f, (float)unit), tree.transform.localPosition.y, Random.Range (0f, (float)unit));
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
