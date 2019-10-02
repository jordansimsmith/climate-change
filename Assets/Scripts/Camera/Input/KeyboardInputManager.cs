using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyboardInputManager : InputManager
{
    public static event MoveInputHandler OnMoveInput;
    public static event RotateInputHandler OnRotateInput;
    public static event ZoomInputHandler OnZoomInput;

    // Update is called once per frame
    void Update()
    {
        //Handle Move
        if (Input.GetKey(KeyCode.W))
        {
            OnMoveInput?.Invoke(Vector3.forward);
        }
        if (Input.GetKey(KeyCode.S))
        {
            OnMoveInput?.Invoke(-Vector3.forward);
        }
        if (Input.GetKey(KeyCode.A))
        {
            OnMoveInput?.Invoke(-Vector3.right);
        }
        if (Input.GetKey(KeyCode.D))
        {
            OnMoveInput?.Invoke(Vector3.right);
        }

/*        Vector3 rotate = new Vector3(0f, 0f);
        //Handle Rotation
        if (Input.GetKey(KeyCode.Q))
        {
            rotate.x = 1f;
        }
        if (Input.GetKey(KeyCode.E))
        {
            rotate.x = -1f;
        }
        if (Input.GetKey(KeyCode.R))
        {
            rotate.y = 1f;
        }
        if (Input.GetKey(KeyCode.T))
        {
            rotate.y = -1f;
        }
        if (Input.GetKey(KeyCode.G))
        {
            rotate.z = 1f;
        }
        if (Input.GetKey(KeyCode.H))
        {
            rotate.z = -1f;
        }
        if (rotate.x != 0f || rotate.y != 0f || rotate.z != 0f)
        {
            OnRotateInput?.Invoke(rotate);
        }*/


        //Handle Zoom
        if (Input.GetKey(KeyCode.Z))
        {
            OnZoomInput?.Invoke(-1f);
        }
        if (Input.GetKey(KeyCode.X))
        {
            OnZoomInput?.Invoke(1f);
        }
    }
}
