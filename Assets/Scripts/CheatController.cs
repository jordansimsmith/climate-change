using UnityEngine;
using World.Resource;

public class CheatController : MonoBehaviour
{
    [SerializeField] private ResourceSingleton resources;
    private NaturalDisasterPanelController disasters;

    private void Awake()
    {
        disasters = FindObjectOfType<NaturalDisasterPanelController>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            // cheat for cyclone
            Debug.Log("cyclone cheat");
            disasters.DoCyclone();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            // cheat for drought
            Debug.Log("drought cheat");
            disasters.DoDrought();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            // cheat for sea level rise
            Debug.Log("sea level rise cheat");
            disasters.DoSeaLevelRise();
        }

        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            // cheat for more money
            Debug.Log("money cheat");
            resources.Money += 1_000;
        }
    }
}