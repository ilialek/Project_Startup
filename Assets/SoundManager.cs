using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource clickButtonSound;

    public void PlayClickButtonSound()
    {
        clickButtonSound.Play();
    }
}
