using System.Collections;
using System.Collections.Generic;
using Persistence.Serializables;
using UnityEngine;
using UnityEngine.UI;

public class WorldItem : MonoBehaviour
{
    [SerializeField] private Text worldNameText;
    [SerializeField] private Text carbonCreditsText;
    [SerializeField] private Text populationText;


    private SerializableWorld world;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayButtonOnClick()
    {
        Debug.Log("Loading World");
    }

    public void DeleteButtonOnClick()
    {
        
    }

    public void Initialise(SerializableWorld world)
    {
        this.world = world;
        carbonCreditsText.text = world.ResourceData.Money.ToString();
        populationText.text = world.ResourceData.Population.ToString();
    }
    
    
}
