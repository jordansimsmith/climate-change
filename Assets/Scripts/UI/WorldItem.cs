using System.Collections;
using System.Collections.Generic;
using Persistence;
using Persistence.Serializables;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class WorldItem : MonoBehaviour
{
    [SerializeField] private Text worldNameText;
    [SerializeField] private Text carbonCreditsText;
    [SerializeField] private Text populationText;


    private SerializableWorld world;
    private PersistenceManager persistenceManager;
    
    // Start is called before the first frame update
    void Start()
    {
        persistenceManager = FindObjectOfType<PersistenceManager>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void PlayButtonOnClick()
    {
        persistenceManager.SelectedWorld = world;
        SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
    }

    public void DeleteButtonOnClick()
    {
        persistenceManager.DeleteWorld(world);
        Destroy(gameObject);
    }

    public void Initialise(SerializableWorld world)
    {
        this.world = world;
        worldNameText.text = world.Name;
        carbonCreditsText.text = world.ResourceData.Money.ToString();
        populationText.text = world.ResourceData.Population.ToString();
    }
    
    
}
