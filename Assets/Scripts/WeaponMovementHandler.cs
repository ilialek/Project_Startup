using UnityEngine;
using UnityEngine.UI;

public class WeaponMovementHandler : MonoBehaviour
{
    [Header("Rotation handling")]
    public float smooth;
    public float swayMultiplier;

    public Transform clipProjector;
    public float checkDistance;
    public Vector3 newDirection;

    float lerpPos;
    RaycastHit hit;

    [Header("Bobbing handling")]
    public float bobbingAmount;
    public float bobbingSpeed;

    float bobbingValue;

    private float timer = 0f;

    Vector3 originalPosition;

    [Header("Aim handling")]
    public Transform weaponHolder;
    public Vector3 aimingPositionHolder;
    public Quaternion aimingRotationHolder;
    Quaternion originalRotationHolder;
    Vector3 originalPositionHolder;
    bool isAiming = false;
    public RectTransform aim;
    public float restingSize;
    public float aimSize;
    float currentSize;

    [Header("Shooting handling")]
    public float shootImpactOnWeapon;

    void Start()
    {
        originalPositionHolder = weaponHolder.localPosition;
        originalRotationHolder = weaponHolder.localRotation;

        bobbingValue = bobbingAmount;
    }

    
    void Update()
    {

        if (isAiming) { ToAim(); }
        else { ToGoToOriginalTransform(); }
        

        //Rotation handling
        if (Physics.Raycast(clipProjector.position, clipProjector.forward, out hit, checkDistance))
        {
            lerpPos = 1 - (hit.distance / checkDistance);

            Mathf.Clamp01(lerpPos);

            transform.localRotation = Quaternion.Lerp(Quaternion.Euler(Vector3.zero), Quaternion.Euler(newDirection), lerpPos);
        }
        else
        {
            // Weapon Sway
            float mouseX = Input.GetAxisRaw("Mouse X") * swayMultiplier;
            float mouseY = Input.GetAxisRaw("Mouse Y") * swayMultiplier;

            Quaternion rotationX = Quaternion.AngleAxis(-mouseY, Vector3.right);
            Quaternion rotationY = Quaternion.AngleAxis(mouseX, Vector3.up);

            Quaternion targetRotation = rotationX * rotationY;

            transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, smooth * Time.deltaTime);
        }


        //Position handling
        if (isMoving())
        {
            // Weapon Bobbing
            float bobbingValueX = Mathf.Sin(timer) * bobbingAmount;
            float bobbingValueY = Mathf.Sin(timer * 2) * bobbingAmount;

            transform.localPosition = new Vector3(bobbingValueX, bobbingValueY, transform.localPosition.z);

            timer += bobbingSpeed * Time.deltaTime;
        }
        else
        {
            transform.localPosition = Vector3.Lerp(transform.localPosition, originalPosition, smooth * Time.deltaTime);
        }

    }

    void ToAim()
    {
        weaponHolder.localPosition = Vector3.Lerp(weaponHolder.localPosition, aimingPositionHolder, smooth * 2f * Time.deltaTime);
        weaponHolder.localRotation = Quaternion.Slerp(weaponHolder.localRotation, aimingRotationHolder, smooth * 2f * Time.deltaTime);
        currentSize = Mathf.Lerp(aim.sizeDelta.x, aimSize, smooth * 1.5f * Time.deltaTime);
        aim.sizeDelta = new Vector2(currentSize, currentSize);
    }

    void ToGoToOriginalTransform()
    {
        weaponHolder.localPosition = Vector3.Lerp(weaponHolder.localPosition, originalPositionHolder, smooth * Time.deltaTime);
        weaponHolder.localRotation = Quaternion.Slerp(weaponHolder.localRotation, originalRotationHolder, smooth * Time.deltaTime);
        currentSize = Mathf.Lerp(aim.sizeDelta.x, restingSize, smooth * 1.5f * Time.deltaTime);
        aim.sizeDelta = new Vector2(currentSize, currentSize);
    }

    public void OnShoot()
    {
        if (isAiming)
        {
            weaponHolder.localRotation = Quaternion.Euler(weaponHolder.localRotation.eulerAngles.x - shootImpactOnWeapon/2, weaponHolder.localRotation.eulerAngles.y + Random.Range(-0.5f, 0.5f), Random.Range(-0.8f, 0.8f));
        }
        else
        {
            weaponHolder.localRotation = Quaternion.Euler(weaponHolder.localRotation.eulerAngles.x - shootImpactOnWeapon, weaponHolder.localRotation.eulerAngles.y + Random.Range(-0.8f, 0.8f), Random.Range(-1.1f, 1.1f));
        }
        
    }

    public void OnRightMouseButtonDown()
    {
        isAiming = true; bobbingAmount = 0.006f; swayMultiplier = 4;
    }

    public void OnRightMouseButtonUp()
    {
        isAiming = false; bobbingAmount = bobbingValue; swayMultiplier = 7;
    }

    bool isMoving()
    {
        if (new Vector3(Input.GetAxis("Vertical"), 0f, Input.GetAxis("Horizontal")).magnitude > 0)
        {
            return true;
        }
        else
        {
            timer = 0f;
            return false;
        }
    }

}
