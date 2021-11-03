using UnityEngine;

public class CameraScroller : MonoBehaviour
{
    public Camera cam;
    private float camFOV;
    public float zoomSpeed;

    private float mouseScrollInput;
    void Start()
    {
        camFOV = cam.fieldOfView;
    }
    void Update()
    {
        mouseScrollInput = Input.GetAxis("Mouse ScrollWheel");

        camFOV -= mouseScrollInput * zoomSpeed;
        camFOV = Mathf.Clamp(camFOV, 30, 60);

        cam.fieldOfView = Mathf.Lerp(cam.fieldOfView, camFOV, zoomSpeed);
    }
}
