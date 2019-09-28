using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldGenerator : MonoBehaviour {

	public GameObject grass;
	public GameObject water;
	public GameObject beach;
	
	public GameObject[] trees;
	
	public float generationOffset;
	public float generationFidelity;
	public float forestFidelity;
	public float treeScale;
	
	public enum Tile {
		GRASS, WATER, BEACH
	}

	void Start () {
		var grid = FindObjectOfType<Grid>();

		var unit = (int)grid.GetUnitSize(); 
		var end = 20;
		var start = 0;
		
		generationOffset += Random.Range(0, 100);
		
		Tile[,] worldArray = new Tile[end,end];
		
		foreach (var tree in trees)	{
			tree.transform.localScale = new Vector3(treeScale, treeScale, treeScale);
		}
		
		for (int x = start; x < end; x += 1)  
        {
            for (int z = start; z < end; z += 1)
            {			
                var centerRelOffsetX = Mathf.Abs((float)(x - (start + end)/2)/(end - start));
                var centerRelOffsetZ = Mathf.Abs((float)(z - (start + end)/2)/(end - start));

                var centerRelOffset = Mathf.Sqrt(Mathf.Pow(centerRelOffsetX, 2) + Mathf.Pow(centerRelOffsetZ, 2));

                var num = Mathf.PerlinNoise(generationOffset + x*generationFidelity, generationOffset + z*generationFidelity);
            
                num += (float)0.5;
                num -= centerRelOffset*1.5f;
				
				Tile tile;
				if (num > 0.4)  {
					tile = Tile.GRASS;
                } else {
					tile = Tile.WATER;
                }
				worldArray[x,z] = tile;
            }
        }
        // Add water borders
        for (int x = start; x < end; x++)  
        {
            for (int z = start; z < end; z++)
            {
                if (x == 0 || z == 0 || x == end - 1 || z == end - 1)	{
					worldArray[x, z] = Tile.WATER;
				}				
            }
        }
        // Add beaches
        for (int x = start; x < end; x++)  
        {
            for (int z = start; z < end; z++)
            {
                var position = new Vector3Int(x, z, 0);
                if (worldArray[x,z] != Tile.GRASS)   {
                    continue;
                }
                if (z < end - 1 && worldArray[x,z + 1] == Tile.WATER)    {
                    worldArray[x,z] = Tile.BEACH;
                }
                if (z > 0 && worldArray[x,z - 1] == Tile.WATER)    {
                    worldArray[x,z] = Tile.BEACH;
                }
                if (x < end - 1 && worldArray[x + 1,z] == Tile.WATER)    {
                    worldArray[x,z] = Tile.BEACH;
                }
                if (x > 0 && worldArray[x - 1,z] == Tile.WATER)    {
                    worldArray[x,z] = Tile.BEACH;
                }
            }
        }
        // Add forests
        for (int x = start; x < end; x++)  
        {
            for (int z = start; z < end; z++)
            {
                if (worldArray[x,z] != Tile.GRASS)   {
                    continue;
                }
                
                var num = Mathf.PerlinNoise(2 + generationOffset + x*forestFidelity, 2 + generationOffset + z*forestFidelity);
            
				var numT = Random.Range(0, 6);
				
                if (num > 0.4)  {
					for (int i = 0; i < numT; i++)	{
						var xo = (float) Random.Range(0f, 1f)*unit;
						var zo = (float) Random.Range(0f, 1f)*unit;
						
						GameObject tree = (GameObject) Instantiate(trees[Random.Range(0,trees.Length - 1)]);
						tree.transform.position = new Vector3((float)x*unit + xo, 0.3f, (float)z*unit + zo);
					}
                } 
            }
        }
		
		var mesh = grass.GetComponentInChildren<MeshFilter> ();		
		var tileHeight = mesh.sharedMesh.bounds.size.y * grass.GetComponent<Transform> ().localScale.y - 0.01;
		
		
		for (int x = start; x < end; x++)  
        {
            for (int z = start; z < end; z++)
            {
				GameObject tile;
				switch (worldArray[x,z])	{
					case Tile.WATER:
						tile = (GameObject) Instantiate(water);
						break;
					case Tile.GRASS:
						tile = (GameObject) Instantiate(grass);
						break;
					case Tile.BEACH:
						tile = (GameObject) Instantiate(beach);
						break;
					default:
						tile = null;
						break;
				}
				if (tile == null)	{
					Debug.Log("not good");
					return;
				}
				tile.transform.position = new Vector3((float) x*unit, (float) -tileHeight, (float) z*unit);
			}
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
