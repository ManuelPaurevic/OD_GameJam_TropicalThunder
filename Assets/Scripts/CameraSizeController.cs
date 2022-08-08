using UnityEngine;

public class CameraSizeController : MonoBehaviour
{
    [SerializeField]
    private Camera cam;

    [SerializeField]
    private float pixelsPerUnit = 100;

    [SerializeField]
    private float zoomChange = 0.25f;

    [SerializeField]
    private float maxZoom = 0.75f;

    private Vector2 resolution = Vector2.zero;

    private float baseOrthographicSize;

    private float zoomAmount = 0;

    private void Awake() {
        if (!cam) {
            cam = GetComponent<Camera>();
        }
    }

    private void Start()
    {
        UpdateCachedResolution();
        CalculateBaseOrthographicSize();
        UpdateCameraSize();
    }

    private void Update()
    {
        if (hasResolutionChanged()) {
            UpdateCachedResolution();
            CalculateBaseOrthographicSize();
            UpdateCameraSize();
        }
    }

    private void UpdateCameraSize() {
        cam.orthographicSize = baseOrthographicSize + (baseOrthographicSize * zoomAmount);
    }

    private void OnZoomOut() {
        zoomAmount += zoomChange;
        ClampZoom();
        UpdateCameraSize();
    }

    private void OnZoomIn() {
        zoomAmount -= zoomChange;
        ClampZoom();
        UpdateCameraSize();
    }

    private void ClampZoom() {
        zoomAmount = Mathf.Clamp(zoomAmount, -maxZoom, maxZoom);
    }

    private bool hasResolutionChanged() {
        return resolution.x != Screen.width || resolution.y != Screen.height;
    }

    private void UpdateCachedResolution() {
        resolution.x = Screen.width;
        resolution.y = Screen.height;
    }

    private void CalculateBaseOrthographicSize() {
        float screenHeight = resolution.y;
        baseOrthographicSize = screenHeight / (pixelsPerUnit * 2);
    }
}
