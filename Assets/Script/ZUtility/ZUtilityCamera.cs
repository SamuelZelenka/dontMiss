using UnityEngine;
using ZUtility;

public class ZUtilityCamera : MonoBehaviour
{
    [SerializeField] Camera activeCamera;

    [Header("Scene")]
    [SerializeField] Vector3 maxPos = new Vector3();
    [SerializeField] Vector3 minPos = new Vector3();

    [Header("Camera")]
    [SerializeField] float cameraLerpSpeed = 5;
    [Range(0, 100)] [SerializeField] float cameraSpeed = 0;

    [Header("Zoom")]
    [SerializeField] float zoomMin = 1;
    [SerializeField] float zoomMax = 10;
    [SerializeField] float zoomLerpSpeed = 10;
    [SerializeField] float zoomSpeed = 200;
    [Range(1, 10)] [SerializeField] float zoomSpeedScale = 1;

    [Header("Input Axis")]
    [SerializeField] string zoomInput;
    CameraTransform cameraTransform;

    Vector3 mouseDownPos = new Vector3();
    Vector3 mousePos = new Vector3();

    private void Start()
    {
        activeCamera = Camera.main;

        cameraTransform = new CameraTransform(activeCamera);
    }
    private void Update()
    {
        InputHandler();
        UpdatePosition();
    }
    public void SetPosition(Vector3 pos)
    {
        cameraTransform.cameraPosition = pos;
    }
    public void SetBoundries(Vector3 min, Vector3 max)
    {
        minPos = min;
        maxPos = max;
    }
    public void SetBoundries(Transform min, Transform max)
    {
        minPos = min.position;
        maxPos = max.position;
    }
    public void SetCamera(Camera camera)
    {
        activeCamera = camera;
    }

    void InputHandler()
    {
        //Zoom
        if (Input.GetAxis(zoomInput) != 0)
        {
            //newCameraTransform.cameraPosition = transform.position;
            cameraTransform.cameraSize -= Input.GetAxis(zoomInput) * zoomSpeed * Time.deltaTime;
        }
        //Middle Mouse Movement
        if (Input.GetMouseButtonDown(2))
        {
            mouseDownPos = activeCamera.ScreenToWorldPoint(Input.mousePosition);
        }
        if (Input.GetMouseButton(2))
        {
            mousePos = activeCamera.ScreenToWorldPoint(Input.mousePosition);
            cameraTransform.cameraPosition = transform.localPosition + mouseDownPos - mousePos;
        }
    }
    void UpdatePosition()
    {
        //Apply zoom
        cameraTransform.cameraSize = Mathf.Clamp(cameraTransform.cameraSize, zoomMin, zoomMax);
        activeCamera.orthographicSize = Mathf.Lerp(activeCamera.orthographicSize, cameraTransform.cameraSize, zoomLerpSpeed * Time.deltaTime);

        //Apply movement
        float halfScreenWidth = activeCamera.ScreenToWorldPoint(new Vector3(activeCamera.scaledPixelWidth, 0, 0)).x - transform.localPosition.x;
        float halfScreenHeight = activeCamera.ScreenToWorldPoint(new Vector3(0, activeCamera.scaledPixelHeight, 0)).y - transform.localPosition.y;
        cameraTransform.cameraPosition.x = Mathf.Clamp(cameraTransform.cameraPosition.x, minPos.x + halfScreenWidth, maxPos.x - halfScreenWidth);
        cameraTransform.cameraPosition.y = Mathf.Clamp(cameraTransform.cameraPosition.y, minPos.y + halfScreenHeight, maxPos.y - halfScreenHeight);
        cameraTransform.cameraPosition.z = -5;

        Vector3 lerpVector = Vector3.Lerp(transform.localPosition, cameraTransform.cameraPosition, cameraLerpSpeed * Time.deltaTime);
        transform.position = lerpVector;
    }
    struct CameraTransform
    {
        public float cameraSize;
        public Vector3 cameraPosition;
        public CameraTransform(Camera camera)
        {
            cameraSize = camera.orthographicSize;
            cameraPosition = camera.transform.position;
        }
    }
}