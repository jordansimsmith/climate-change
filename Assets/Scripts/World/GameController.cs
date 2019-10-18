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

    [SerializeField] private float minResourceRatio; // Minimum ok ratio supply/demand for a resource
    [SerializeField] private int demandMargin; // A margin that will be subtracted from demand when calculating ratios, makes it safer for new players/small towns
    [SerializeField] private int secondsToLose; // Number of secs a player can survive a loosing condition
    private int secondsLoosing; // Used to count number of secs (0-20) a loss condition has been present
    public bool Loosing => secondsLoosing > 0;


    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("PollResources", 0, 1f);
    }

    private void PollResources()
    {
        CalculateResources();
        CalculateMoneyRate();
        LoseConditions();
    }


    private void CalculateMoneyRate() {
        int moneyRate = 0;
        foreach (var tile in gameBoard.Tiles) {
            if (tile.Entity == null) continue;
            moneyRate += tile.Entity.Stats.money;
        }
        resources.MoneyRate = moneyRate;
    }

    // Triggers loss if any lose condition held for more than "secondsToLose"
    private void LoseConditions()
    {
        float[] balances = new float[4];
        float demandMarginAdj = demandMargin + 0.5f; // Prevent divide by zero :)
        balances[0] = resources.totalSupply.environment/(resources.totalDemand.environment - demandMarginAdj);
        balances[1] = resources.totalSupply.power/(resources.totalDemand.power - demandMarginAdj);
        balances[2] = resources.totalSupply.shelter/(resources.totalDemand.shelter - demandMarginAdj);
        balances[3] = resources.totalSupply.food/(resources.totalDemand.food - demandMarginAdj);

        bool loseCondition = false;
        foreach (float balance in balances)   {
            if (balance > 0 && balance < minResourceRatio) {
                loseCondition = true; 
            }
        }

        if (loseCondition)  {
            if (secondsLoosing > secondsToLose) {
                CancelInvoke("PollResources");
                OnGameLose();
            }
            secondsLoosing++;
        } else {
            secondsLoosing = 0;
        }
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