using System.Collections;
using System.Collections.Generic;
using Persistence;
using Persistence.Serializables;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using World;

public class WorldItem : MonoBehaviour
{
    [SerializeField] private Text worldNameText;
    [SerializeField] private Text carbonCreditsText;
    [SerializeField] private Text populationText;
    [SerializeField] private GameObject sharePanel;


    private GameObject loader;
    private ServerWorld world;
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
        loader.SetActive(true);
        SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
        SceneManager.sceneLoaded += (arg0, mode) => loader.SetActive(false);
    }

    public void DeleteButtonOnClick()
    {
        persistenceManager.DeleteWorld(world);
        Destroy(gameObject);
    }

    public void Initialise(ServerWorld serverWorld, GameObject loader)
    {
        this.loader = loader;
        this.world = serverWorld;
        worldNameText.text = serverWorld.world.Name;
        carbonCreditsText.text = serverWorld.world.ResourceData.Money.ToString();
        populationText.text = serverWorld.world.ResourceData.Population.ToString();

        if (serverWorld.shareCode != null)
        {
            sharePanel.SetActive(true);
            sharePanel.GetComponentInChildren<Text>().text = serverWorld.shareCode;
        }
        else
        {
            sharePanel.SetActive(false);
        }
    }
    
    
}
