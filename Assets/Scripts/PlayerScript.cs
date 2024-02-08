using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using UnityEngine.UI;

public class PlayerScript : NetworkBehaviour
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

    //public  NetworkVariable<int> health = new NetworkVariable<int>(100, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);

    [SerializeField] private int health = 100;

    [SerializeField] private Slider slider;

    [SerializeField] private AudioListener audioListener;

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
            Debug.LogError("More than 2 weapons are activated or none");
        }
        else if (numberOfActivatedWeapons == 1)
        {
            currentWeapon = activatedWeapon;
        }

        if (!IsLocalPlayer)
        {
            if (audioListener != null)
            {
                audioListener.enabled = false;
            }
        }

        // Check if the player is the local player
 HEAD
       if (IsLocalPlayer)
=======
        if (IsLocalPlayer)
 096a88d4b1bafc2391bf7987bfef254ce57822f4
        {
            // Enable the camera for the local player
            playerCamera.enabled = true;
        }
        else
        {
            // Disable the camera for non-local players
            playerCamera.enabled = false;
        }

        //SetupPlayerCamera();

    }


    void Update()
    {
        if (!IsOwner) { return; }

        slider.value = health / 100f;

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
        if (!IsOwner) { return; }

        GroundMovement();
    }

 HEAD
   public override void OnGainedOwnership()
   {
       // Called when the local player gains ownership of the object
=======
    public override void OnGainedOwnership()
    {
        // Called when the local player gains ownership of the object
 096a88d4b1bafc2391bf7987bfef254ce57822f4
        if (IsLocalPlayer)
        {
            // Enable the camera for the local player
            playerCamera.enabled = true;
        }
    }

    [ClientRpc]
    public void TakeDamageClientRpc(int damage)
    {
        health -= damage;
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

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
        }
    }

}
