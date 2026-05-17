using UnityEngine;
using System.Collections;

public class CameraMove : MonoBehaviour
{
    public Transform cameraPosition;

    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    private void Update()
    {
        transform.position = cameraPosition.position;
    }
}
