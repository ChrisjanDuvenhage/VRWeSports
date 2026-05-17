using UnityEngine;
using UnityEngine.InputSystem;

public class Grab : MonoBehaviour
{
    private Camera mainCamera;
    private Grabable currentlyHeldObject;
    private float holdDistance = 5f;
    private Vector3 lastHoldPosition;
    private Vector3 lastCameraPosition;
    private Quaternion lastCameraRotation;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        // Check if left mouse button is being held
        if (Mouse.current != null && Mouse.current.leftButton.isPressed)
        {
            // If we're not holding anything yet, try to pick one up
            if (currentlyHeldObject == null)
            {
                TryPickupObject();
            }
            else
            {
                // Hold and move the object
                HoldObject();
            }
        }
        else
        {
            // Drop the object when mouse button is released
            if (currentlyHeldObject != null)
            {
                DropObject();
            }
        }
    }

    void TryPickupObject()
    {
        // Raycast from mouse position to detect objects
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 100f))
        {
            Grabable interactable = hit.collider.GetComponent<Grabable>();
            if (interactable != null)
            {
                currentlyHeldObject = interactable;
                currentlyHeldObject.Pickup();
                holdDistance = Vector3.Distance(mainCamera.transform.position, hit.point);

                // Store initial positions for velocity calculation
                lastHoldPosition = hit.point;
                lastCameraPosition = mainCamera.transform.position;
                lastCameraRotation = mainCamera.transform.rotation;
            }
        }
    }

    void HoldObject()
    {
        if (currentlyHeldObject != null)
        {
            // Calculate position in front of camera at the hold distance
            Vector3 holdPosition = mainCamera.transform.position + mainCamera.transform.forward * holdDistance;
            currentlyHeldObject.Hold(holdPosition);

            // Update position tracking for velocity calculation
            lastHoldPosition = holdPosition;
        }
    }

    void DropObject()
    {
        if (currentlyHeldObject != null)
        {
            currentlyHeldObject.Drop();
            currentlyHeldObject = null;
        }
    }
}