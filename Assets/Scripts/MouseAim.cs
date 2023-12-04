using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class MouseAim : NetworkBehaviour
{

    public float mouseSensitivity = 100f;

    float xRotation = 0f;

    public Transform playerBody;

    void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;

        //if (IsOwner)
        //{
        //    // Set the camera owner to the local player
        //    Camera.main.GetComponent<NetworkObject>().ChangeOwnership(OwnerClientId);
        //}
    }

    void Update()
    {
         if (!IsOwner) { return; }

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, -90f, 90f);

        transform.localRotation = Quaternion.Euler(xRotation, 0f, transform.localRotation.z);
        playerBody.Rotate(Vector3.up * mouseX);
    }
}
