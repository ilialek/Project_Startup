using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VFX;
using TMPro;

public class GunNoNetCode : MonoBehaviour
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

    [SerializeField] LayerMask glassLayerMask;
    [SerializeField] Transform brokenGlassPrefab;

    [SerializeField] LayerMask wallLayerMask;
    [SerializeField] Transform radius;

    [SerializeField] LayerMask environmentLayerMask;

    [SerializeField] LayerMask suitcaseLayerMask;
    [SerializeField] float pickupRange = 5f;

    [Header("Hard coded stuff")]
    [SerializeField] GameObject rifleMeshes;
    [SerializeField] Transform weaponHolder;
    [SerializeField] float throwForce;
    [SerializeField] float throwUpwardForce;
    Transform suitcase;

    bool carryingSuitcase = false;


    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        weaponMovementScript = GetComponent<WeaponMovementHandler>();

        amountOfBulletsInMagazine = magazinCapacity;
    }

    void Update()
    {

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

                        if (!carryingSuitcase)
                        {
                            Shoot();
                        }
                        else
                        {
                            Throw();
                        }

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



        CheckForSuitcase();


    }

    void CheckForSuitcase()
    {
        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, pickupRange, suitcaseLayerMask))
        {
            hit.transform.GetComponent<SuitcaseScript>().BeingAimed();

            if (Input.GetKeyDown(KeyCode.E))
            {
                carryingSuitcase = true;
                rifleMeshes.SetActive(false);

                suitcase = hit.transform;

                suitcase.GetComponent<Rigidbody>().isKinematic = true;
                suitcase.GetComponent<BoxCollider>().isTrigger = true;
                suitcase.SetParent(weaponHolder);
                suitcase.localPosition = new Vector3(-0.02f, 0.62f, -0.16f);
                suitcase.localRotation = Quaternion.identity;
            }
        }
    }

    void Throw()
    {
        suitcase.GetComponent<Rigidbody>().isKinematic = false;
        suitcase.GetComponent<BoxCollider>().isTrigger = false;

        suitcase.SetParent(null);

        Vector3 forceToAdd = fpsCamera.transform.forward * throwForce + transform.up * throwUpwardForce;
        suitcase.GetComponent<Rigidbody>().AddForce(forceToAdd, ForceMode.Impulse);

        suitcase.GetComponent<SuitcaseScript>().beingUsed = true;

        carryingSuitcase = false;
        rifleMeshes.SetActive(true);

        suitcase = null;
    }

    void Shoot()
    {
        visualEffect.Play();
        aim.sizeDelta = new Vector2(aim.sizeDelta.x * 1.4f, aim.sizeDelta.y * 1.4f);

        PlayTheSound(shotSound, 0.7F);

        weaponMovementScript.OnShoot();
        cameraRecoilScript.RecoilFire();

        //ShootServerRpc();

        RaycastHit hit;

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range, glassLayerMask))
        {

            Destroy(hit.transform.gameObject);
            Transform brokenGlass = Instantiate(brokenGlassPrefab, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), hit.transform.rotation);
            //Transform brokenGlass = Instantiate(brokenGlassPrefab, new Vector3(hit.transform.position.x, hit.transform.position.y, hit.transform.position.z), hit.transform.rotation);


            foreach (Transform child in brokenGlass) {
                if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidBody)) {
                    childRigidBody.AddExplosionForce(200f, hit.point, 10);
                }
            }
        }

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range, wallLayerMask))
        {
            Instantiate(radius, hit.point, hit.transform.rotation);
        }

        if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range, environmentLayerMask))
        {
            if (hit.transform.TryGetComponent<Rigidbody>(out Rigidbody rigidBody) && rigidBody.isKinematic)
            {
                foreach (Transform child in hit.transform.parent)
                {
                    if (child.TryGetComponent<Rigidbody>(out Rigidbody childRigidBody))
                    {
                        childRigidBody.isKinematic = false;
                        childRigidBody.AddExplosionForce(500f, hit.point, 40);
                    }
                }
            }
            else
            {
                hit.transform.GetComponent<Rigidbody>().AddExplosionForce(500f, hit.point, 40);
            }

            
        }

     
    }

    //[ServerRpc]
    //void ShootServerRpc()
    //{

    //    RaycastHit hit;

    //    if (Physics.Raycast(fpsCamera.transform.position, fpsCamera.transform.forward, out hit, range))
    //    {
    //        PlayerScript playerScript = hit.collider.GetComponent<PlayerScript>();

    //        if (playerScript != null)
    //        {
    //            playerScript.TakeDamageClientRpc(damage);
    //        }
    //    }
    //}

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
