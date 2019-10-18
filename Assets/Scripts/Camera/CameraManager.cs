using UnityEngine;

public class CameraManager : MonoBehaviour
{
    [Header("Camera Positioning")] public Vector2 cameraOffset = new Vector2(10f, 14f);
    public float lookAtOffset = 20f;

    [Header("Move Controls")] public float inOutSpeed = 75f;
    public float lateralSpeed = 75f;

    [Header("Move Bounds")] public Vector2 minBounds, maxBounds;

    [Header("Rotate Controls")] public float rotateSpeed = 50f;

    [Header("Zoom Controls")] public float zoomSpeed = 30f;
    public float nearZoomLimit = 50f;
    public float farZoomLimit = 1000f;
    public float startingZoom = 50f;

    private IZoomStrategy zoomStrategy;
    private Vector3 frameMove;
    private float frameZoom;
    private float frameRotate;
    private Camera cam;

    private Vector3 initialPosition;
    private Quaternion initialRotation;

    private void Awake()
    {
        maxBounds.x = 190;
        maxBounds.y = 190;
        cam = GetComponentInChildren<Camera>();
        cam.transform.localPosition = new Vector3(0f, Mathf.Abs(cameraOffset.y), -Mathf.Abs(cameraOffset.x));
        zoomStrategy = cam.orthographic
            ? (IZoomStrategy) new OrthographicZoomStrategy(cam, startingZoom)
            : new PerspectiveZoomStrategy(cam, cameraOffset, startingZoom);
        cam.transform.LookAt(transform.position + Vector3.up * lookAtOffset);

        initialPosition = transform.position;
        initialRotation = transform.rotation;
    }

    private void OnEnable()
    {
        KeyboardInputManager.OnMoveInput += UpdateFrameMove;
        KeyboardInputManager.OnZoomInput += UpdateFrameZoom;
        KeyboardInputManager.OnRotateInput += UpdateFrameRotate;
        MouseInputManager.OnMoveInput += UpdateFrameMove;
        MouseInputManager.OnZoomInput += UpdateFrameZoom;
    }

    private void UpdateFrameRotate(float rotateAmount)
    {
        frameRotate += rotateAmount;
    }

    private void OnDisable()
    {
        KeyboardInputManager.OnMoveInput -= UpdateFrameMove;
        KeyboardInputManager.OnZoomInput -= UpdateFrameZoom;
        KeyboardInputManager.OnRotateInput -= UpdateFrameRotate;
        MouseInputManager.OnMoveInput -= UpdateFrameMove;
        MouseInputManager.OnZoomInput -= UpdateFrameZoom;
    }

    private void UpdateFrameZoom(float zoomAmount)
    {
        frameZoom += zoomAmount;
    }

    private void UpdateFrameMove(Vector3 moveVector)
    {
        frameMove += moveVector;
    }

    private void LateUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            transform.SetPositionAndRotation(initialPosition, initialRotation);
            zoomStrategy = cam.orthographic
                ? ((IZoomStrategy) new OrthographicZoomStrategy(cam, startingZoom))
                : new PerspectiveZoomStrategy(cam, cameraOffset, startingZoom);
        }

        if (frameMove != Vector3.zero)
        {
            Vector3 speedModFrameMove = new Vector3(frameMove.x * lateralSpeed, frameMove.y, frameMove.z * inOutSpeed);
            transform.position += transform.TransformDirection(speedModFrameMove) * Time.deltaTime;
            LockPositionInBounds();
            frameMove = Vector3.zero;
        }

        if (frameZoom < 0f)
        {
            zoomStrategy.ZoomIn(cam, Time.deltaTime * Mathf.Abs(frameZoom) * zoomSpeed, nearZoomLimit);
            frameZoom = 0f;
        }

        if (frameZoom > 0f)
        {
            zoomStrategy.ZoomOut(cam, Time.deltaTime * frameZoom * zoomSpeed, farZoomLimit);
            frameZoom = 0f;
        }

        if (frameRotate != 0f)
        {
            transform.RotateAround(new Vector3(95f, 0f, 95f), new Vector3(0, 1f, 0),
                frameRotate * Time.deltaTime * rotateSpeed);
            frameRotate = 0f;
        }
    }

    private void LockPositionInBounds()
    {
        transform.position = new Vector3(
            Mathf.Clamp(transform.position.x, minBounds.x, maxBounds.x),
            transform.position.y,
            Mathf.Clamp(transform.position.z, minBounds.y, maxBounds.y)
        );
    }
}