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
        void EventHandler(ResourceEvent e) {
            if (e.WentBelow && e.Threshold == -100) {
                OnGameLose();
            }
        }
        resources.Food.Subscribe(EventHandler);        
        resources.Power.Subscribe(EventHandler);        
        resources.Shelter.Subscribe(EventHandler);        
    }
    

    public void OnGameLose() {
        var endScreen = FindObjectOfType<EndScreenController>();
        endScreen.EnableLoseScreen();
    }
    
    public void OnGameWin() {
        var endScreen = FindObjectOfType<EndScreenController>();
        endScreen.EnableWinScreen();
    }

}
