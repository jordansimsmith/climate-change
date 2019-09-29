using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridWorldController : MonoBehaviour
{
    public GameObject grass;
    public GameObject water;
    public GameObject beach;
    public GameObject trees;

    public float generationOffset;
    public float generationFidelity;
    public float forestFidelity;

    public GameObject[,] entities; // Placable entities
    Tile[,] worldArray; // Ground tiles

    private int unit;

    public enum Tile
    {
        GRASS,
        WATER,
        BEACH
    }

    void Start()
    {
        var grid = FindObjectOfType<Grid>();

        unit = (int) grid.GetUnitSize();
        var end = 20;
        var start = 0;

        generationOffset += Random.Range(0, 100);

        worldArray = new Tile[end, end];
        entities = new GameObject[end, end];

        for (int x = start; x < end; x += 1)
        {
            for (int z = start; z < end; z += 1)
            {
                var centerRelOffsetX = Mathf.Abs((float) (x - (start + end) / 2) / (end - start));
                var centerRelOffsetZ = Mathf.Abs((float) (z - (start + end) / 2) / (end - start));

                var centerRelOffset = Mathf.Sqrt(Mathf.Pow(centerRelOffsetX, 2) + Mathf.Pow(centerRelOffsetZ, 2));

                var num = Mathf.PerlinNoise(generationOffset + x * generationFidelity,
                    generationOffset + z * generationFidelity);

                num += (float) 0.5;
                num -= centerRelOffset * 1.5f;

                Tile tile;
                if (num > 0.4)
                {
                    tile = Tile.GRASS;
                }
                else
                {
                    tile = Tile.WATER;
                }

                worldArray[x, z] = tile;
            }
        }

        // Add water borders
        for (int x = start; x < end; x++)
        {
            for (int z = start; z < end; z++)
            {
                if (x == 0 || z == 0 || x == end - 1 || z == end - 1)
                {
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
                if (worldArray[x, z] != Tile.GRASS)
                {
                    continue;
                }

                if (z < end - 1 && worldArray[x, z + 1] == Tile.WATER)
                {
                    worldArray[x, z] = Tile.BEACH;
                }

                if (z > 0 && worldArray[x, z - 1] == Tile.WATER)
                {
                    worldArray[x, z] = Tile.BEACH;
                }

                if (x < end - 1 && worldArray[x + 1, z] == Tile.WATER)
                {
                    worldArray[x, z] = Tile.BEACH;
                }

                if (x > 0 && worldArray[x - 1, z] == Tile.WATER)
                {
                    worldArray[x, z] = Tile.BEACH;
                }
            }
        }

        var mesh = grass.GetComponentInChildren<MeshFilter>();
        var tileHeight = mesh.sharedMesh.bounds.size.y * grass.GetComponent<Transform>().localScale.y - 0.01;


        for (int x = start; x < end; x++)
        {
            for (int z = start; z < end; z++)
            {
                GameObject tile;
                switch (worldArray[x, z])
                {
                    case Tile.WATER:
                        tile = (GameObject) Instantiate(water);
                        break;
                    case Tile.GRASS:
                        tile = (GameObject) Instantiate(grass);
                        var num = Mathf.PerlinNoise(2 + generationOffset + x * forestFidelity,
                            2 + generationOffset + z * forestFidelity);

                        if (num > 0.4)
                        {
                            GameObject treeSet = (GameObject) Instantiate(trees);
                            treeSet.transform.parent = tile.transform;
                            treeSet.transform.localPosition = Vector3.zero;
                            entities[x, z] = treeSet;
                        }

                        break;
                    case Tile.BEACH:
                        tile = (GameObject) Instantiate(beach);
                        break;
                    default:
                        tile = null;
                        break;
                }

                if (tile == null)
                {
                    Debug.Log("not good");
                    return;
                }

                tile.transform.position = new Vector3((float) x * unit, (float) -tileHeight, (float) z * unit);
                tile.transform.parent = gameObject.transform;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
    }

    public bool AddAtReal(float xf, float zf, GameObject entity)
    {
        int x = Mathf.FloorToInt(xf / unit);
        int z = Mathf.FloorToInt(zf / unit);

        if (entities[x, z] != null)
        {
            return false;
        }

        entities[x, z] = entity;
        return true;
    }

    public Tile? GetTileAtReal(float xf, float zf)
    {
        int x = Mathf.FloorToInt(xf / unit);
        int z = Mathf.FloorToInt(zf / unit);

        return worldArray[x, z];
    }

    public void TryDeleteAtReal(float xf, float zf)
    {
        int x = Mathf.FloorToInt(xf / unit);
        int z = Mathf.FloorToInt(zf / unit);

        Destroy(entities[x, z]);
        entities[x, z] = null;
    }
}