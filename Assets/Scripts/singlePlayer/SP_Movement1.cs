using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SP_Movement1 : MonoBehaviour
{
    public Camera playerCamera;
    Rigidbody rb;

    [Header("Ground movement and jump settings")]
    public float speed = 12f;
    public float groundDrag;
    public float airFriction;
    public float jumpForce;
    public float jumpHeightCheck;
    public LayerMask whatIsGround;
    public bool isGrounded;

    [Header("Weapon related settings")]
    public GameObject currentWeapon;

    public bool onMouseScroll = false;
    public GameObject[] ownedWeapons;

    public bool isSwitchingWeapons = false;

    public bool isAiming = false;

    [Header("Other settings")]
    public Transform headTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;

        //camera = Camera.main;

        int numberOfActivatedWeapons = 0;
        GameObject activatedWeapon = null;

        foreach (GameObject weapon in ownedWeapons)
        {
            if (weapon.activeSelf)
            {
                numberOfActivatedWeapons++;
                activatedWeapon = weapon;
            }
        }

        if (numberOfActivatedWeapons > 1 || numberOfActivatedWeapons == 0)
        {
            //Debug.LogError("More than 2 weapons are activated or none");
        }
        else if (numberOfActivatedWeapons == 1)
        {
            currentWeapon = activatedWeapon;
        }
    }


    void Update()
    {

        JumpAndDrag();
        SpeedControl();

        if (Input.mouseScrollDelta.y != 0 && !isSwitchingWeapons && !isAiming)
        {
            onMouseScroll = true;
        }
        else
        {
            onMouseScroll = false;
        }
    }

    void FixedUpdate()
    {

        GroundMovement();
    }


    public void SwitchWeapons()
    {
        for (int i = 0; i < ownedWeapons.Length; i++)
        {
            if (currentWeapon == ownedWeapons[i] && i != ownedWeapons.Length - 1)
            {
                currentWeapon.SetActive(false);
                ownedWeapons[i + 1].SetActive(true);
                currentWeapon = ownedWeapons[i + 1];
                return;
            }
            else if (currentWeapon == ownedWeapons[i] && i == ownedWeapons.Length - 1)
            {
                currentWeapon.SetActive(false);
                ownedWeapons[0].SetActive(true);
                currentWeapon = ownedWeapons[0];
                return;
            }
        }
    }

    void GroundMovement()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 cameraForward = playerCamera.transform.forward;

        cameraForward.y = 0f;
        cameraForward.Normalize();

        Vector3 move = cameraForward * z + playerCamera.transform.right * x;

        if (isGrounded)
        {
            rb.AddForce(move.normalized * speed * 10f, ForceMode.Force);
        }
        else
        {
            rb.AddForce(move.normalized * speed * 10f * airFriction, ForceMode.Force);
        }
    }

    void SpeedControl()
    {
        Vector3 XZVelocity = new Vector3(rb.velocity.x, 0f, rb.velocity.z);

        if (XZVelocity.magnitude > speed)
        {
            Vector3 normalizedVelocity = XZVelocity.normalized * speed;
            rb.velocity = new Vector3(normalizedVelocity.x, rb.velocity.y, normalizedVelocity.z);
        }
    }

    void JumpAndDrag()
    {

        isGrounded = Physics.Raycast(transform.position, Vector3.down, jumpHeightCheck, whatIsGround);

        if (isGrounded)
        {
            rb.drag = groundDrag;
        }
        else
        {
            rb.drag = 0;
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }
}
