using NaturalDisasters;
using Tutorial;
using UnityEngine;
using World.Resource;

public class CheatController : MonoBehaviour
{
    [SerializeField] private ResourceSingleton resources;
    private NaturalDisasterDispatcher disasters;
    private EndScreenController endScreen;
    private TutorialManager tutorialManager;

    private void Awake()
    {
        disasters = FindObjectOfType<NaturalDisasterDispatcher>();
        endScreen = FindObjectOfType<EndScreenController>();
        tutorialManager = FindObjectOfType<TutorialManager>();
    }

    private void Update()
    {
        // natural disasters
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

        // resources
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            // cheat for more money
            Debug.Log("money cheat");
            resources.Money += 1_000;
        }

        // game state
        if (Input.GetKeyDown(KeyCode.O))
        {
            // cheat for win
            Debug.Log("win cheat");
            endScreen.EnableWinScreen();
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            // cheat for lose
            Debug.Log("lose cheat");
            endScreen.EnableLoseScreen();
        }
        
        // tutorial
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            // cheat to skip the tutorial
            tutorialManager.EndTutorial();
        }
    }
}