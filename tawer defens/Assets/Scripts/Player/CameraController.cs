using UnityEngine;

public class CameraController : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float moveSpeed = 25f;
    [SerializeField] private float zoomSpeed = 200f;
    [SerializeField] private float minY = 10f;
    [SerializeField] private float maxY = 80f;
    [SerializeField] private bool useEdgePanning = false;
    [SerializeField] private float edgeSize = 25f;

    private Camera cam;

    private void Awake()
    {
        cam = Camera.main;
        Cursor.lockState = CursorLockMode.None;
    }

    private void Update()
    {
        HandleKeyboardMovement();
        if (useEdgePanning) HandleEdgePanning();
        HandleZoom();
    }

    private void HandleKeyboardMovement()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        Vector3 right = new Vector3(cam.transform.right.x, 0, cam.transform.right.z).normalized;
        Vector3 forward = new Vector3(cam.transform.forward.x, 0, cam.transform.forward.z).normalized;

        transform.position += (right * h + forward * v).normalized * moveSpeed * Time.deltaTime;
    }

    private void HandleEdgePanning()
    {
        Vector3 move = Vector3.zero;
        Vector3 mousePos = Input.mousePosition;

        if (mousePos.x < edgeSize) move += -transform.right;
        if (mousePos.x > Screen.width - edgeSize) move += transform.right;
        if (mousePos.y < edgeSize) move += -transform.forward;
        if (mousePos.y > Screen.height - edgeSize) move += transform.forward;

        move.y = 0;
        transform.position += move.normalized * moveSpeed * Time.deltaTime;
    }

    private void HandleZoom()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        if (Mathf.Abs(scroll) < 0.01f) return;

        Vector3 newPos = transform.position + cam.transform.forward * scroll * zoomSpeed * Time.deltaTime;
        newPos.y = Mathf.Clamp(newPos.y, minY, maxY);
        transform.position = newPos;
    }
}
