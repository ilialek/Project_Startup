using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using TMPro;
using Unity.Netcode;

public class Gun : NetworkBehaviour
{
    public int damage = 10;
    public float range = 100f;

    public int amountOfBullets;
    public int amountOfBulletsInMagazine;
    public int magazinCapacity;

    public TMP_Text ammoText;

    public float fireRate = 15f;
    float nextTimeToFire = 0f;

    public Camera fpsCamera;
    public VisualEffect visualEffect;

    public RectTransform aim;

    WeaponMovementHandler weaponMovementScript;
    public CameraRecoil cameraRecoilScript;

    public AudioClip shotSound;
    public AudioClip emptyMagazineSound;
    public AudioClip reloadSound;

    //public Animator animator;
    public bool reloading = false;

    AudioSource audioSource;

    public PlayerScript itsOwnPlayerScript;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        weaponMovementScript = GetComponent<WeaponMovementHandler>();

        amountOfBulletsInMagazine = magazinCapacity;
    }

    void Update()
    {
        if (!IsOwner) { return; }

        //if (IsLocalPlayer) { return; }

        ammoText.text = amountOfBulletsInMagazine.ToString() + " / " + amountOfBullets.ToString();

        if (!itsOwnPlayerScript.isSwitchingWeapons)
        {
            if (Input.GetMouseButton(1)) { weaponMovementScript.OnRightMouseButtonDown(); itsOwnPlayerScript.isAiming = true; }
            if (Input.GetMouseButtonUp(1)) { weaponMovementScript.OnRightMouseButtonUp(); itsOwnPlayerScript.isAiming = false; }


            if (Input.GetMouseButton(0))
            {
                //Debug.LogError("SHOOT");

                if (!reloading)
                {
                    if (Time.time >= nextTimeToFire && amountOfBulletsInMagazine > 0)
                    {
                        nextTimeToFire = Time.time + 1f / fireRate;
                        Shoot();

                        amountOfBulletsInMagazine--;
                    }
                    else if (Time.time >= nextTimeToFire && amountOfBulletsInMagazine == 0)
                    {
                        nextTimeToFire = Time.time + 1f / fireRate;
                        PlayTheSound(emptyMagazineSound, 1F);
                    }
                }

            }

            else
            {
                if (Input.GetKeyDown(KeyCode.R) && amountOfBulletsInMagazine < magazinCapacity && amountOfBullets > 0)
                {
                    //animator.SetBool("OnReload", true);
                    reloading = true;
                }
            }
        }
 
    }

    void Shoot()
    {
        visualEffect.Play();
        aim.sizeDelta = new Vector2(aim.sizeDelta.x * 1.4f, aim.sizeDelta.y * 1.4f);

        PlayTheSound(shotSound, 0.7F);

        weaponMovementScript.OnShoot();
        cameraRecoilScript.RecoilFire();

        ShootServerRpc();

        //RaycastHit hit;

        //if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        //{
        //    PlayerScript playerScript = hit.collider.GetComponent<PlayerScript>();

        //    if (playerScript != null)
        //    {
        //        ShootServerRpc(playerScript);
        //    }
        //}
    }

    [ServerRpc]
    void ShootServerRpc() {
        
        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
        {
            PlayerScript playerScript = hit.collider.GetComponent<PlayerScript>();

            if (playerScript != null)
            {
                playerScript.TakeDamageClientRpc(damage);
            }
        }
    }

    void PlayTheSound(AudioClip audioClip, float volume)
    {
        audioSource.PlayOneShot(audioClip, volume);
    }

    public void OnReload()
    {
        PlayTheSound(reloadSound, 1F);

        //animator.SetBool("OnReload", false);

        while (amountOfBulletsInMagazine < magazinCapacity && amountOfBullets > 0)
        {
            amountOfBullets--;
            amountOfBulletsInMagazine++;
        }
    }
}
