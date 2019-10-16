using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementSocialite : Achievement
{
    private int numTalkedTo = 0;
    [SerializeField] private int target;

    public void OnTalkTo()  {
        numTalkedTo++;

        if (numTalkedTo > target)   {
            this.done = true;
        }
    }

    public override void AchievementUpdate()
    {
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
