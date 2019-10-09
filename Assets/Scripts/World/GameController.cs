using UnityEngine;
using World;
using World.Resource;
using World.Entities;

public class GameController : MonoBehaviour
{
    [SerializeField] private ResourceSingleton resources;
    [SerializeField] private EndScreenController endScreenController;
    [SerializeField] private GameBoard gameBoard;

    [SerializeField] private int populationFoodMultiplier;
    [SerializeField] private int populationShelterMultiplier;
    [SerializeField] private int populationPowerMultiplier;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("PollResources", 0, 1f);
    }

    private void PollResources()
    {
        CalculateResources();

        // if (food <= -100 || power <= -100 || shelter <= -100 || environment <= -100)
        // {
        //     CancelInvoke("PollResources");
        //     OnGameLose();
        // }
    }

    // Calculates and updates current resource balance based on GameBoard, Population
    private void CalculateResources()
    {
        EntityStatsTuple totalStats = gameBoard.GetOnBoardResourceCount();

        totalStats.demand.food += resources.Population * populationFoodMultiplier;
        totalStats.demand.shelter += resources.Population * populationShelterMultiplier;
        totalStats.demand.power += resources.Population * populationPowerMultiplier;

        resources.totalDemand = totalStats.demand;
        resources.totalSupply = totalStats.supply;

    }
    
    public void OnGameLose()
    {
        Debug.Log("game lose");
        endScreenController.EnableLoseScreen();
    }

    public void OnGameWin()
    {
        endScreenController.EnableWinScreen();
    }
}