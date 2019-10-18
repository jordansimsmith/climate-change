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
    private GameObject loader;

    // Start is called before the first frame update
    void Start()
    {
        persistenceManager = FindObjectOfType<PersistenceManager>();
    }

    public void PlayButtonOnClick()
    {
        persistenceManager.SelectedWorld = world;
        SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
        loader.SetActive(true);
        SceneManager.sceneLoaded += (scene, mode) =>
        {
            if (scene.name.Equals("TestScene"))
            {
                loader.SetActive(false);
            }
        };
    }

    public void DeleteButtonOnClick()
    {
        persistenceManager.DeleteWorld(world);
        Destroy(gameObject);
    }

    public void Initialise(SerializableWorld world, GameObject loader)
    {
        this.world = world;
        worldNameText.text = world.Name;
        carbonCreditsText.text = world.ResourceData.Money.ToString();
        populationText.text = world.ResourceData.Population.ToString();
        this.loader = loader;
    }
}