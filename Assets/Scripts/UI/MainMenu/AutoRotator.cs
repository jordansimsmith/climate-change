using UnityEngine;

public class AutoRotator : MonoBehaviour
{

    // Update is called once per frame
    private void Update()
    {
        // spin the object
        transform.RotateAround(new Vector3(100,-2.5f,100), new Vector3(0, 0.25f*Time.deltaTime, 0), 0.25f);
    }
}
