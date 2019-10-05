using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using World.Resource;

public class GameController : MonoBehaviour {

    [SerializeField] private ResourceSingleton resources;
    
    // Start is called before the first frame update
    void Start()
    {
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
