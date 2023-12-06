using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using System;
using UnityEngine.UI;

public class PickUp : MonoBehaviour 
{
    public float pickUpRange = 3.0f;

    public LayerMask pickUpLayerMask;
    public LayerMask caseLayerMask;

    public Slider chestSlider;

    private Camera playerCamera;
    private GameObject heldItem;
    public Transform pickUpPoint;
    public TMP_Text guideText;
    private Ray ray;
    private RaycastHit hit;
    private bool isOpeningCase = false;
    private float holdTimer;
    private float requiredHoldTime = 1.5f;

    private Chest currentChest;

    public bool lookingAtItem = false;
    public bool lookingAtCase = false;

    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        playerCamera = GetComponentInChildren<Camera>();
        chestSlider.gameObject.SetActive(false);
    }

    private void Update()
    {
        // Guide Text and raycast
        /////////////////////////
        guideText.text = "";

        ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, pickUpRange, pickUpLayerMask))
        {
            lookingAtItem = true;
            guideText.text = "Press E to pick up";
            //Debug.Log("is looking at an item");
        }
        else
        {
            lookingAtItem = false;
            //Debug.Log("is NOT looking at an item");
        }

        if (Physics.Raycast(ray, out hit, pickUpRange, caseLayerMask))
        {        
            Chest chest = hit.transform.GetComponent<Chest>();
            if (chest != null) 
            {
                if(!chest.IsOpened) 
                {
                    lookingAtCase = true;
                    guideText.text = "Hold E to open";
                    currentChest = chest;
                    Debug.Log("is looking at a case");
                }
            }
            else
            {
                lookingAtCase = false;
                currentChest = null;
                Debug.Log("it is NOT looking at a case");
            }
        }
        
        /////////////////////////
        /////////////////////////
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            PickUpItem();
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            DropItem();
        }

        if (Input.GetKey(KeyCode.E) && lookingAtCase && !currentChest.IsOpened)
        {      
            // Timer starts only once when the key is initially pressed
            holdTimer += Time.deltaTime;
            // Enable the slider and update its value based on hold time
            chestSlider.gameObject.SetActive(true);
            chestSlider.value = Mathf.Clamp01(holdTimer / requiredHoldTime);
            Debug.Log(chestSlider.value + " is opening case");
        }
        else
        {
            // Reset the timer and disable the slider when the key is released               
            holdTimer = 0f;
            chestSlider.gameObject.SetActive(false);
            chestSlider.value = 0;
        }
        if (holdTimer >= requiredHoldTime)
        {
            OpenCase();
        }
    }


    void PickUpItem()
    {
        if (Physics.Raycast(ray, out hit, pickUpRange, pickUpLayerMask))
        {
            GameObject selectedItem = hit.transform.gameObject;

            if (selectedItem != null)
            {
                selectedItem.GetComponent<Collider>().enabled = false;
                selectedItem.GetComponent<Rigidbody>().isKinematic = true;
                selectedItem.GetComponent<Rigidbody>().useGravity = false;
                selectedItem.transform.localScale = Vector3.one;
                selectedItem.transform.position = Vector3.zero;
                selectedItem.transform.rotation = Quaternion.identity;
                selectedItem.transform.SetParent(pickUpPoint, false);
                heldItem = selectedItem;
            }
        }
    }

    void DropItem()
    {
        if (heldItem != null)
        {
            heldItem.GetComponent<Collider>().enabled = true;
            heldItem.GetComponent<Rigidbody>().isKinematic = false;
            heldItem.GetComponent<Rigidbody>().useGravity = true;
            heldItem.transform.SetParent(null);
            heldItem = null;
        }
    }

    void OpenCase()
    {
        if (currentChest != null)
        {
            chestSlider.gameObject.SetActive(false);
            currentChest.OpenChest();
            holdTimer = 0f;
            currentChest = null;
        }
    }
}
