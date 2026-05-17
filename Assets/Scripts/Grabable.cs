using UnityEngine;

public class Grabable : MonoBehaviour
{
    private Rigidbody rb;
    private Collider objectCollider;
    private bool isHeld = false;
    private float originalDrag;
    private bool usedGravity;
    private Vector3 lastPosition;
    private Vector3 velocity;

    [Header("Throw Settings")]
    [Tooltip("Multiplier for the throw force. 1.0 = normal, 0.5 = half force, 2.0 = double force")]
    [Range(0.1f, 5.0f)]
    public float throwForceMultiplier = 1.0f;

    void Start()
    {
        // Get or add required components
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        objectCollider = GetComponent<Collider>();
        originalDrag = rb.linearDamping;
        usedGravity = rb.useGravity;
        lastPosition = transform.position;
    }

    void FixedUpdate()
    {
        if (isHeld)
        {
            // Calculate velocity based on position change
            velocity = (transform.position - lastPosition) / Time.fixedDeltaTime;
            lastPosition = transform.position;
        }
    }

    public void Pickup()
    {
        isHeld = true;
        // Disable physics while holding
        rb.useGravity = false;
        rb.linearDamping = 10f; // Add drag to smooth movement
        lastPosition = transform.position;
        velocity = Vector3.zero;
        Debug.Log($"Picked up: {gameObject.name}");
    }

    public void Hold(Vector3 targetPosition)
    {
        if (isHeld)
        {
            // Move the object smoothly to the target position
            rb.MovePosition(targetPosition);
        }
    }

    public void Drop()
    {
        isHeld = false;
        // Restore physics
        rb.useGravity = usedGravity;
        rb.linearDamping = originalDrag;

        // Apply the calculated velocity with the multiplier when dropping
        rb.linearVelocity = velocity * throwForceMultiplier;

        Debug.Log($"Dropped: {gameObject.name} with velocity: {velocity * throwForceMultiplier}");
    }
}