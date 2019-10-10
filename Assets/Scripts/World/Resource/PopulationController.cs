using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using World.Resource;

/**
    Manages population, and the resource stats that derive from it
 */
public class PopulationController : MonoBehaviour
{
    public ResourceSingleton ResourceSingleton;
    public int StartPop;
    public int MaxIncrement;
    public int MinIncrement;
    public int IncrementPeriodS;

    // Start is called before the first frame update
    void Start()
    {
        ResourceSingleton.Population = StartPop;

        InvokeRepeating("PopulationTick", IncrementPeriodS, IncrementPeriodS);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Called every 30s
    void PopulationTick()
    {
        ResourceSingleton.Population += Random.Range(MinIncrement, MaxIncrement);
    }
}
