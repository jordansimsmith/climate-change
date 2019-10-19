using System.Collections;
using System.Collections.Generic;
using DefaultNamespace;
using Persistence;
using Persistence.Serializables;
using UnityEngine;
using UnityEngine.UI;
using World;

public class WorldsPanelController : MonoBehaviour
{
    
    [SerializeField] private Transform contentPanel;
    [SerializeField] private GameObject worldItemPrefab;
    [SerializeField] private InputField newWorldInput;
    [SerializeField] private Button createButton;

    [SerializeField]
    private WorldManager worldManager;

 
    
    void Start()
    {
        PopulateWorldsList();
    }

    void ClearWorlds()
    {
        WorldItem[] worldItems = GetComponentsInChildren<WorldItem>();

        foreach (WorldItem item in worldItems)
        {
            Destroy(item.gameObject);
        }
    }
    
    public void PopulateWorldsList()
    {
        ClearWorlds();
        var worlds = worldManager.LoadWorldsFromDisk();
        foreach (SerializableWorld world in worlds)
        {
            AddItem(world);
        }
    }

    private void AddItem(SerializableWorld world)
    {
        GameObject newItem = Instantiate(worldItemPrefab);

        // set parent
        newItem.transform.SetParent(contentPanel, false);


        // initialise
        WorldItem worldItem = newItem.GetComponent<WorldItem>();
        worldItem.Initialise(world);
       
    }
    

    public void NewWorldTextChanged()
    {
        if (newWorldInput.text.Trim().Equals(""))
        {
            createButton.interactable = false;
        }
        else
        {
            createButton.interactable = true;
        }
    }

    public void CreateButtonClicked()
    {
        SerializableWorld newWorld = worldManager.CreateWorld(newWorldInput.text);
        APIService.Instance.access_token = "eyJhbGciOiJSUzI1NiIsImtpZCI6ImZhMWQ3NzBlZWY5ZWFhNjU0MzY1ZGE5MDhjNDIzY2NkNzY4ODkxMDUiLCJ0eXAiOiJKV1QifQ.eyJwcm92aWRlcl9pZCI6ImFub255bW91cyIsImlzcyI6Imh0dHBzOi8vc2VjdXJldG9rZW4uZ29vZ2xlLmNvbS9jYWVsaWFwaSIsImF1ZCI6ImNhZWxpYXBpIiwiYXV0aF90aW1lIjoxNTcxNDUyODUyLCJ1c2VyX2lkIjoiNkRVa1lEQ1h4cVlOTW9vN041dnhieTlYc2JTMiIsInN1YiI6IjZEVWtZRENYeHFZTk1vbzdONXZ4Ynk5WHNiUzIiLCJpYXQiOjE1NzE0NTI4NTIsImV4cCI6MTU3MTQ1NjQ1MiwiZmlyZWJhc2UiOnsiaWRlbnRpdGllcyI6e30sInNpZ25faW5fcHJvdmlkZXIiOiJhbm9ueW1vdXMifX0.drhFO0bJpPQtnSo1e1KDJ7AUzr8bte7KuJmLkow0VC3_mYXPN_EcY8zJEz8JbVDa_dtQ7YpRdc45ddX64udKNMrGe8zJ-TJ9H2yI29Q0Z4lYkqgjJEYHGXdL4Q7hHvbGLm3EPERgiflF4b_dsCrxbFhoJXybd0l3TSQPwr2UFKWTucL1O0kdZ6PGm815St_LxAJa8B1OZ4AhdmWb6gC2fh7CFhIOrublCHlzKtiFuYHtAkiiITXf4-LKiCQ4bP0Z8s3jKceVi_c-9a3kkE-RHRcprPMttllf3KnMnkr23XThYZ5bJUhmWnFy0lCcHJbKoZsVVtOFxIKmc8-jAPV4vA";
        APIService.Instance.CreateWorld(new DbWorld(newWorld), s => Debug.Log(s));
        AddItem(newWorld);
        newWorldInput.text = "";
    }

    public void CloseButtonClicked()
    {
        ClearWorlds();
        gameObject.SetActive(false);
    }


 
    // Update is called once per frame
    void Update()
    {
        
    }
}
