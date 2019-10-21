using Persistence;
using UnityEngine;
using UnityEngine.UI;

public class ObserverPanelController : MonoBehaviour
{
    public Text worldText;
    public Text shareText;
    public GameObject tornadoPrefab;

    private PersistenceManager persistenceManager;

    // Start is called before the first frame update
    private void Start()
    {
        // locate persistence manager in heirachy
        persistenceManager = FindObjectOfType<PersistenceManager>();
        InvalidateUI();
    }

    public void TornadoButtonClicked()
    {
        // create tornado
        Instantiate(tornadoPrefab);
    }

    private void InvalidateUI()
    {
        if (!persistenceManager || persistenceManager.SelectedWorld == null)
        {
            return;
        }

        worldText.text = "World: " + persistenceManager.SelectedWorld.world.Name;
        shareText.text = "Sharing Code: " + persistenceManager.SelectedWorld.shareCode;
    }
}