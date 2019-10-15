using UnityEngine;
using World.Entities;
using World;

public class EntityPlacedAchievement : Achievement
{
    [SerializeField] private EntityType targetEntity;
    [SerializeField] private int targetAmount;
    [SerializeField] private GameBoard gameBoard;

    private int offset = 0;
    private bool ready = false;

    public void Start() {
        Invoke("SetOffset", 2);
    }

    public void SetOffset() {
        offset = gameBoard.CountEntityTypeOnBoard(targetEntity);
        ready = true;
    }

    public override void AchievementUpdate()
    {
        if (!ready) {
            return;
        }
        
        var num = gameBoard.CountEntityTypeOnBoard(targetEntity);
        if (num >= (offset + targetAmount) && !this.done)    {
            Debug.Log(this.Title+" Done");
            this.done = true;
        }
        this.progress = num/(offset + targetAmount);
    }
}