using UnityEngine;
using TMPro;

public class PickUp : MonoBehaviour
{
    public float pickUpRange = 3.0f;
    public LayerMask pickUpLayerMask;
    private Camera playerCamera;
    private GameObject heldItem;
    public Transform pickUpPoint;
    public TMP_Text guideText;
    private Ray ray;
    private RaycastHit hit;

    void Start()
    {
        UnityEngine.Cursor.lockState = CursorLockMode.Locked;
        UnityEngine.Cursor.visible = false;
        playerCamera = GetComponentInChildren<Camera>();
    }

    void Update()
    {
        ray = playerCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out hit, pickUpRange, pickUpLayerMask))
        {
            if (hit.transform.gameObject.layer == pickUpLayerMask.value)
            {
                guideText.text = "Press E to pick up";
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            pickUpItem();
        }
        else if (Input.GetKeyDown(KeyCode.G))
        {
            dropItem();
        }
    }

    void pickUpItem()
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

    void dropItem()
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
}
