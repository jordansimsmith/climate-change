using UnityEngine;

public class MouseInputManager : InputManager
{
    //events
    public static event MoveInputHandler OnMoveInput;
    public static event RotateInputHandler OnRotateInput;
    public static event ZoomInputHandler OnZoomInput;

    private Vector2Int screen;

    private void Awake()
    {
        screen = new Vector2Int(Screen.width, Screen.height);
    }

    private void Update()
    {
        // check mouse is within bounds
        Vector3 mp = Input.mousePosition;
        bool mouseValid = mp.y <= screen.y * 1.05f && mp.y >= screen.y * -0.05f &&
                          mp.x <= screen.x * 1.05f && mp.x >= screen.x * -0.05f;
        if (!mouseValid) return;

        // pan camera if mouse is on edge of screen
        if (mp.y > screen.y * 0.995f)
        {
            OnMoveInput?.Invoke(Vector3.forward);
        }
        else if (mp.y < screen.y * 0.005f)
        {
            OnMoveInput?.Invoke(-Vector3.forward);
        }

        if (mp.x > screen.x * 0.995f)
        {
            OnMoveInput?.Invoke(Vector3.right);
        }
        else if (mp.x < screen.x * 0.005f)
        {
            OnMoveInput?.Invoke(-Vector3.right);
        }

        if (Input.mouseScrollDelta.y > 0)
        {
            OnZoomInput?.Invoke(-3f);
        }
        else if (Input.mouseScrollDelta.y < 0)
        {
            OnZoomInput?.Invoke(3f);
        }
    }
}