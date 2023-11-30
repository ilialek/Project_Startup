using System;
using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;

using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UIElements;

public class PickUp : MonoBehaviour
{
    public float pickUpRange = 3.0f;

    public LayerMask pickUpLayerMask;

    private Camera playerCamera;

    private GameObject heldItem;

    public Transform pickUpPoint;

    void Start()
    {
        //UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        //UnityEngine.Cursor.visible = false;
        playerCamera = GetComponentInChildren<Camera>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E)) 
        {
            if (heldItem == null) 
            {
                pickUpItem();
            }
        }

        else if (Input.GetKeyDown(KeyCode.G)) 
        {
            if (heldItem != null)
            {
                dropItem();
            }
        }
    }

    void pickUpItem()
    {
        Ray ray = playerCamera.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, pickUpRange, pickUpLayerMask))
        {
            heldItem = hit.collider.gameObject;
            heldItem.GetComponent<Rigidbody>().isKinematic = true;
            heldItem.transform.SetParent(pickUpPoint.transform);
            heldItem.transform.rotation = Quaternion.identity;
            heldItem.transform.position = pickUpPoint.position;
        }
    }

    void dropItem()
    {
        heldItem.GetComponent<Rigidbody>().isKinematic = false;
        heldItem.transform.SetParent(null);
        heldItem = null;
    }
}
