using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;

public class PlayerMovement : NetworkBehaviour
{
    Rigidbody rb;

    [Header("Ground movement and jump settings")]
    public float speed = 12f;
    public float groundDrag;
    public float airFriction;
    public float jumpForce;
    public float jumpHeightCheck;
    public LayerMask whatIsGround;
    public bool isGrounded;

    [Header("Other settings")]
    public Camera sceneCamera;

    public Transform headTransform;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.freezeRotation = true;


        //if (IsOwner)
        //{
        //    sceneCamera = Camera.main;

        //    if (sceneCamera != null)
        //    {
        //        sceneCamera.enabled = false;
        //    }
        //}

        SetupPlayerCamera();

    }


    void Update()
    {
        if (!IsOwner) { return; }

        JumpAndDrag();
        SpeedControl();
    }

    void FixedUpdate()
    {
        if (!IsOwner) { return; }
        GroundMovement();
    }

    void SetupPlayerCamera()
    {
        if (!IsOwner) { return; }
        if (headTransform != null)
        {
            // Set camera position to the head
            Camera.main.transform.position = headTransform.position;

            // Look at a point in front of the player
            Camera.main.transform.LookAt(transform.position + transform.forward * 30);

            // Set the player as the parent of the camera
            Camera.main.transform.parent = headTransform;
        }
        else
        {
            Debug.LogError("Head transform not found. Please ensure the 'Head' transform exists.");
        }
    }

    void GroundMovement()
    {

        float x = Input.GetAxisRaw("Horizontal");
        float z = Input.GetAxisRaw("Vertical");

        Vector3 move = transform.right * x + transform.forward * z;

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

        if (Input.GetKeyDown(KeyCode.C) && isGrounded)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }
    
}
