using UnityEngine;
using UnityEngine.InputSystem;

public class BowlingPin : MonoBehaviour
{
    public bool down;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Ground"))
        {
            down = true;
        }
    }
}
