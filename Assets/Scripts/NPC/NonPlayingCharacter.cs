using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NonPlayingCharacter : MonoBehaviour
{
    private string firstName;
    private string lastName;
    private string occupation;

    public string FirstName { get => firstName; set => firstName = value; }
    public string LastName { get => lastName; set => lastName = value; }
    public string Occupation { get => occupation; set => occupation = value; }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // transform.position += new Vector3(0.01f, 0, 0);
    }

    void OnMouseDown()
    {
        Debug.Log("Clicked " + firstName + " " + lastName);
    }
}
