using UnityEngine;

public class CameraConrioller : MonoBehaviour
{
    [Header("References")]
    public Camera MainCamera;
    public Transform CamHolder;
    public SpriteRenderer Map;

    [Header("Zoom")]
    [SerializeField] float m_touch_zoom_sensivity = 0.01f;
    // Zoom bounds
    [Min(1)]
    [SerializeField] float m_zoom_min = 1f;
    [Min(1)]
    [SerializeField] float m_zoom_max = 8f;

    // Drag bounds
    Vector3 m_drag_origin;

    void Update()
    {
        // Updating start touch position
        if (Input.GetMouseButtonDown(0))
            m_drag_origin = MainCamera.ScreenToWorldPoint(Input.mousePosition);

        // Handling camera drag direction based on start touch position
        if (Input.GetMouseButton(0) && Input.touchCount <= 1)
            Drag();

        HandleMouseZoom();
        HandleTouchZoom();

        UpdateBounds();
    }

    /* Drag */
    void Drag()
    {
        Vector3 direction = m_drag_origin - MainCamera.ScreenToWorldPoint(Input.mousePosition);
        CamHolder.position += direction;

        UpdateBounds();
    }

    void UpdateBounds()
    {
        Vector3 pos = CamHolder.position;

        // Camera
        Vector3 camSize = MainCamera.ViewportToWorldPoint(new Vector3(1, 1, MainCamera.nearClipPlane)) - CamHolder.position;

        Vector2 minBounds = Map.bounds.min + camSize;
        Vector2 maxBounds = Map.bounds.max - camSize;

        pos.x = Mathf.Clamp(pos.x, minBounds.x, maxBounds.x);
        pos.y = Mathf.Clamp(pos.y, minBounds.y, maxBounds.y);

        CamHolder.position = pos;
    }

    /* Zoom */

    void HandleMouseZoom()
    {
        ChangeZoom(Input.GetAxis("Mouse ScrollWheel"));
    }

    void HandleTouchZoom()
    {
        if (Input.touchCount != 2)
            return;

        Touch touchZero = Input.GetTouch(0);
        Touch touchOne = Input.GetTouch(1);

        Vector2 touchZeroPrevPos = touchZero.position - touchZero.deltaPosition;
        Vector2 touchOnePrevPos = touchOne.position - touchOne.deltaPosition;

        float prevMagnitude = (touchZeroPrevPos - touchOnePrevPos).magnitude;
        float curMagnitude = (touchZero.position - touchOne.position).magnitude;

        float diff = curMagnitude - prevMagnitude;
        ChangeZoom(diff * m_touch_zoom_sensivity);
    }

    // Zoom in or out
    void ChangeZoom(float zoomDelta)
    {
        SetZoom(MainCamera.orthographicSize - zoomDelta);
    }

    // Set exact zoom value
    void SetZoom(float value)
    {
        MainCamera.orthographicSize = Mathf.Clamp(value, m_zoom_min, m_zoom_max);
    }
}
