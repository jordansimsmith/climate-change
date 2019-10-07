using UnityEngine;
using World.Resource;

public class GameController : MonoBehaviour
{
    [SerializeField] private ResourceSingleton resources;
    [SerializeField] private EndScreenController endScreenController;

    // Start is called before the first frame update
    void Start()
    {
        
//        void EventHandler(ResourceEvent e) {
//            Debug.Log(e.Threshold);
//            if (e.WentBelow && e.Threshold == -100) {
//                OnGameLose();
//            }
//        }
//        resources.Food.Subscribe(EventHandler);        
//        resources.Power.Subscribe(EventHandler);        
//        resources.Shelter.Subscribe(EventHandler);        
//        resources.Environment.Subscribe(EventHandler);
        InvokeRepeating("PollResources", 0, 1f);
    }

    private void PollResources()
    {
        float food = resources.Food.CurAmount;
        float power = resources.Power.CurAmount;
        float shelter = resources.Shelter.CurAmount;
        float environment = resources.Environment.CurAmount;

        if (food <= -100 || power <= -100 || shelter <= -100 || environment <= -100)
        {
            CancelInvoke("PollResources");
            OnGameLose();
        }
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