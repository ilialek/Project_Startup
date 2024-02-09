using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource clickButtonSound;
    public AudioSource hoverOverButtonSound;

    public void PlayClickButtonSound()
    {
        clickButtonSound.Play();
    }

    public void PlayHoverOverButtonSound()
    {
        if(hoverOverButtonSound.clip != null && !hoverOverButtonSound.isPlaying)
        {
            hoverOverButtonSound.Play();
        }     
    }
}
