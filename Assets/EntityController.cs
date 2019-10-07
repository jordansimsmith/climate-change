using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using World.Entities;

public class EntityController : MonoBehaviour
{
    [SerializeField] private EntityFactory entityFactory;
    [SerializeField] private Text costLabel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void onHover(EntityType entityType)
    {
        int cost = entityFactory.GetCost(entityType);
        costLabel.enabled = true;
        costLabel.text = "Cost:" + cost;
    }

    public void onHoverExit()
    {
        costLabel.enabled = false;
        costLabel.text = "";
    }
}
