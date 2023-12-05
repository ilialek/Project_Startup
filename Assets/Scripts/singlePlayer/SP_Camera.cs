using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_Camera : MonoBehaviour
{
    public float mouseSensitivity = 100f;

    private float verticalRotation = 0f;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        // Update vertical rotation
        verticalRotation -= mouseY;
        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);

        // Apply rotation to the camera for vertical movement
        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);

        // Apply rotation to the player object for horizontal movement
        transform.parent.Rotate(Vector3.up * mouseX);
    }
}
