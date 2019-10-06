using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using World.Resource;

public class GameController : MonoBehaviour {

    [SerializeField] private ResourceSingleton resources;
    
    // Start is called before the first frame update
    void Start() {
        resources.Money = 1000;
        resources.MoneyRate = 0;
        resources.Environment.MinAmount = -100;
        resources.Environment.CurAmount = 0;
        resources.Power.MinAmount = -100;
        resources.Power.CurAmount = 0;
        resources.Food.MinAmount = -100;
        resources.Food.CurAmount = 0;
        resources.Shelter.MinAmount = -100;
        resources.Shelter.CurAmount = 0;
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


        public void OnGameLose() {
        Debug.Log("game lose");
        var endScreen = FindObjectOfType<EndScreenController>();
        endScreen.EnableLoseScreen();
    }
    
    public void OnGameWin() {
        var endScreen = FindObjectOfType<EndScreenController>();
        endScreen.EnableWinScreen();
    }

}
