using Persistence;
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
    private void Start()
    {
        persistenceManager = FindObjectOfType<PersistenceManager>();
    }

    public void PlayButtonOnClick()
    {
        // load main scene
        persistenceManager.SelectedWorld = world;
        loader.SetActive(true);
        SceneManager.LoadScene("TestScene", LoadSceneMode.Single);
        SceneManager.sceneLoaded += (arg0, mode) => loader.SetActive(false);
    }

    public void DeleteButtonOnClick()
    {
        // delete world
        persistenceManager.DeleteWorld(world);
        Destroy(gameObject);
    }

    public void Initialise(ServerWorld serverWorld, GameObject loader)
    {
        // set world item properties
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