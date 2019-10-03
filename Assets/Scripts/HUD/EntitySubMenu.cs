using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntitySubMenu : MonoBehaviour
{
    private string entityType;
    private CanvasGroup canvasGroup;

    private GameObject[] currentEntities;

    public GameObject entityMenuItem;

    // Start is called before the first frame update
    void Start()
    {
        canvasGroup = gameObject.GetComponent<CanvasGroup>();
        this.Hide();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SetEntityType(string entity)   {
        this.entityType = entity;
    }

    public void Toggle(string entity) {
        if (this.entityType == entity && canvasGroup.alpha > 0.5f)  {
            this.Hide();
        } else {
            this.entityType = entity;
            this.AddEntitiesToMenu();
            this.SetupEntitiesOnMenu();
            this.Show();
        }
    }

    public void Hide() {
        canvasGroup.alpha = 0f;
        canvasGroup.blocksRaycasts = false;
    }
    
    public void Show() {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    private void AddEntitiesToMenu() {
        if (this.currentEntities != null)   {
            for (int i = 0; i < this.currentEntities.Length; i++)   {
                Destroy(this.currentEntities[i]);
            }
        }

        switch (this.entityType)    {
            case "Electricity":
                this.currentEntities = new GameObject[6];
            break;
            case "Ecosystem":
                this.currentEntities = new GameObject[5];
            break;
            case "Food":
                this.currentEntities = new GameObject[2];
            break;
            case "Shelter":
                this.currentEntities = new GameObject[3];
            break;
        }
    }

    private void SetupEntitiesOnMenu()   {
        RectTransform transform = gameObject.GetComponent<RectTransform>();
        transform.sizeDelta = new Vector2 (100, this.currentEntities.Length * 100);

        for (int i = 0; i < this.currentEntities.Length; i++)   {
            this.currentEntities[i] = (GameObject) Instantiate(entityMenuItem);
            this.currentEntities[i].transform.SetParent(this.gameObject.transform);
            this.currentEntities[i].transform.localScale = new Vector3(1,1,1);
        }
    }
}
