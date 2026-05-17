using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCam : MonoBehaviour
{
    public float sensX = 100f;
    public float sensY = 100f;
    public float smoothing = 10f;

    public Transform orientation;
    public InputActionAsset inputActions;

    private InputAction lookAction;
    private float xRotation;
    private float yRotation;
    private Vector2 smoothVelocity;

    private void Awake()
    {
        // Get the Look action from the Input Action Asset
        lookAction = inputActions.FindAction("Player/Look");

        // Lock and hide cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void OnEnable()
    {
        lookAction.Enable();
    }

    private void OnDisable()
    {
        lookAction.Disable();
    }

    void Update()
    {
        // Read the mouse delta from the Look action
        Vector2 mouseDelta = lookAction.ReadValue<Vector2>();

        // Apply smoothing
        mouseDelta = Vector2.SmoothDamp(smoothVelocity, mouseDelta, ref smoothVelocity, 1f / smoothing);

        float mouseX = mouseDelta.x * sensX * Time.deltaTime;
        float mouseY = mouseDelta.y * sensY * Time.deltaTime;

        yRotation += mouseX;
        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.rotation = Quaternion.Euler(xRotation, yRotation, 0);
        orientation.rotation = Quaternion.Euler(0, yRotation, 0);
    }
}