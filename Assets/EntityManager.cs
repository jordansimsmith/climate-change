using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityManager : MonoBehaviour
{
    public GameObject[] buildings;
    private Placer placement;

    // Use this for initialization
    void Start()
    {
		foreach (var building in buildings) {
			var mesh = building.GetComponentInChildren<MeshFilter> ();
			if (mesh == null) {
				continue;
			}
			var size = mesh.sharedMesh.bounds.size.x;

			var transform = building.GetComponent<Transform> ();
			var scale = (float) 3 / size;
			
			transform.localScale = new Vector3 (scale, scale, scale);
		}
        placement = GetComponent<Placer>();
    }

    // Update is called once per frame
    void OnGUI()
    {
		int i = 0;
        for (i = 0; i < buildings.Length; i++)
        {
            if (GUI.Button(new Rect(100, 20 + 35 * i, 100, 30), buildings[i].name))
            {
				placement.SetDelete (false);
                placement.SetObject(buildings[i]);
            }
        }
		if (GUI.Button (new Rect (100, 20 + 35 * (i + 1), 100, 30), "Delete")) {
			placement.FlipDelete ();
		}
    }
}
