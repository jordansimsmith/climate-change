using UnityEngine;
using World.Resource;

public class UpgradeTownHallController : MonoBehaviour
{
    [SerializeField] private ResourceSingleton resourceSingleton;
    [SerializeField] private GameController gameController;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void ButtonPressed()
    {
        if (resourceSingleton.Money >= 10_000)
        {
            // win game
            gameController.OnGameWin();
        }
        else
        {
            Debug.Log("insufficient credits to upgrade the town hall");
        }
    }
}