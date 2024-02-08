using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GeneralGunScript : MonoBehaviour
{
    public PlayerScript playerScript;
    public Gun gunScript;

    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (playerScript.onMouseScroll)
        {
            ToPutBack();
        }

        if (gunScript.reloading)
        {
            animator.SetBool("toReload", true);
        }

    }

    public void OnSwitch()
    {
        playerScript.SwitchWeapons();
        gameObject.SetActive(false);
        animator.SetBool("isEquipped", true);
        //otherGun.SetActive(true);
    }

    public void OnReload()
    {
        gunScript.OnReload();
    }

    public void FinishReload()
    {
        gunScript.reloading = false;
        animator.SetBool("toReload", false);
    }

    public void OnWeaponTakeOut()
    {
        playerScript.isSwitchingWeapons = false;
    }

    void ToPutBack()
    {
        animator.SetBool("isEquipped", false);
        playerScript.isSwitchingWeapons = true;
    }
}
