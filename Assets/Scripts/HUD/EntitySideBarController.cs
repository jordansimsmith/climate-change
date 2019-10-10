using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;
using World.Entities;

public class EntitySideBarController : MonoBehaviour
{
    [SerializeField] private Text electricity;
    [SerializeField] private Text environment;
    [SerializeField] private Text food;
    [SerializeField] private Text shelter;
    [SerializeField] private Text Income;
    private Entity entity;

    // Start is called before the first frame update
    void Start()
    {
         gameObject.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ShowSideBar()
    {
        gameObject.SetActive(true);
    }
}
