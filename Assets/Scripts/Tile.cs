using System.Collections;
using System.Collections.Generic;
using Unity.UNetWeaver;
using UnityEngine;

public class Tile : MonoBehaviour
{
    [SerializeField]
    private Biome biome;

    private Tile left, right, above, below;
    
    private
    // Start is called before the first frame update
    void Start()
    {
        Vector3 size = this.gameObject.GetComponent<Renderer>().bounds.size;
        Debug.Log(size);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
