using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using World.Resource;

public class GameController : MonoBehaviour {

    [SerializeField] private ResourceSingleton resources;
    
    // Start is called before the first frame update
    void Start()
    {

        resources.Environment.MinAmount = 1;
        resources.Environment.CurAmount = 10;
        resources.Power.MinAmount = 1;
        resources.Power.CurAmount = 2;
        resources.Food.MinAmount = 1;
        resources.Food.CurAmount = 2;
        resources.Shelter.MinAmount = 1;
        resources.Shelter.CurAmount = 2;
        
        void EventHandler(ResourceEvent e) {
            if (e.WentBelow && e.Threshold == 0) {
                OnGameLose();
            }
        }
        resources.Food.Subscribe(EventHandler);        
        resources.Power.Subscribe(EventHandler);        
        resources.Shelter.Subscribe(EventHandler);        
    }
    

    public void OnGameLose() {
       // Display a lose scene? 
    }
    
    public void OnGameWin() {
       // Display a win scene? 
    }

}
