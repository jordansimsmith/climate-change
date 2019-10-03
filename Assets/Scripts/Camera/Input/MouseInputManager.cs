using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MouseInputManager : InputManager
{
    Vector2Int screen;
    Vector2 mousePositionOnRotateStart;

    //events

    public static event MoveInputHandler OnMoveInput;
    public static event RotateInputHandler OnRotateInput;
    public static event ZoomInputHandler OnZoomInput;

    private void Awake()
    {
        screen = new Vector2Int(Screen.width, Screen.height);
    }

    private void Update()
    {
        Vector3 mp = Input.mousePosition;
        bool mouseValid = (mp.y <= screen.y * 1.05f && mp.y >= screen.y * -0.05f &&
            mp.x <= screen.x * 1.05f && mp.x >= screen.x * -0.05f);

        if (!mouseValid) return;

        //movement
        if(mp.y > screen.y * 0.95f)
        {
            OnMoveInput?.Invoke(Vector3.forward);
        }
        else if(mp.y < screen.y * 0.05f)
        {
            OnMoveInput?.Invoke(-Vector3.forward);
        }

        if (mp.x > screen.x * 0.95f)
        {
            OnMoveInput?.Invoke(Vector3.right);
        }
        else if (mp.x < screen.x * 0.05f)
        {
            OnMoveInput?.Invoke(-Vector3.right);
        }

        //rotation
        if (Input.GetMouseButtonDown(1))
        {
            mousePositionOnRotateStart = new Vector2(mp.x,mp.y);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            Vector3 rotate = new Vector3(0f, 0f,0f);
            if(mp.x < mousePositionOnRotateStart.x)
            {
                rotate.x = -1f;
            }
            else if (mp.x > mousePositionOnRotateStart.x)
            {
                rotate.x = 1f;
            }

            if(mp.y < mousePositionOnRotateStart.y)
            {
                rotate.y = -1f;
            }else if(mp.y > mousePositionOnRotateStart.y)
            {
                rotate.y = 1f;
            }
            if (rotate.x != 0f || rotate.y != 0f)
            {
                OnRotateInput?.Invoke(rotate);
            }
        }

        if(Input.mouseScrollDelta.y > 0)
        {
            OnZoomInput?.Invoke(-3f);
        }
        else if(Input.mouseScrollDelta.y < 0)
        {
            OnZoomInput.Invoke(3f);
        }

    }
}
