using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayingCharacter : MonoBehaviour
{
    private string firstName;
    private string lastName;
    private string occupation;
    
    private Color[] colours;
    private Renderer[] renderers;

    public string FirstName { get => firstName; set => firstName = value; }
    public string LastName { get => lastName; set => lastName = value; }
    public string Occupation { get => occupation; set => occupation = value; }

    // Start is called before the first frame update
    private void Start()
    {
        renderers = GetComponentsInChildren<Renderer>();
        colours = new Color[renderers.Length];
        // cache original color
        for (int i = 0; i < renderers.Length; i++)
        {
            colours[i] = renderers[i].material.color;
        }
    }
    private void OnMouseEnter()
    {
        foreach (Renderer rend in renderers)
        {
            rend.material.color = Color.magenta;
        }
    }

    private void OnMouseExit()
    {
        // restore original colours
        for (int i = 0; i < renderers.Length; i++)
        {
            renderers[i].material.color = colours[i];
        }
    }

    void OnMouseDown()
    {
        Debug.Log("Clicked " + firstName + " " + lastName);
    }
}
