using UnityEngine;

public abstract class Achievement : MonoBehaviour   {
    [SerializeField] private string title;
    public string Title => title;
    [SerializeField] private string description;
    public string Description => description;

    protected float progress;
    public float Progress => progress;
    protected bool done;
    public bool Done => done;

    public abstract void AchievementUpdate(); 
}