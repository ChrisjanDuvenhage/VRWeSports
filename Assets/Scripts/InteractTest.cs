using UnityEngine;
using UnityEngine.InputSystem;

public class InteractTest : MonoBehaviour
{
    void Update()
    {
        // Alternative method using input actions
        if (Mouse.current != null)
        {
            float leftButtonValue = Mouse.current.leftButton.ReadValue();
            if (leftButtonValue > 0)
            {
                Debug.Log($"Left mouse button is being held down (pressure: {leftButtonValue})");
            }
        }
    }
}