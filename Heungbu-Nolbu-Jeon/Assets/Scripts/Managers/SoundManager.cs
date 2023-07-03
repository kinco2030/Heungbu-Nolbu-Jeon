using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public AudioSource audioSource;

    private void Start()
    {
        PlayBackgroundBgm();
    }

    public void PlayBackgroundBgm()
    {
        audioSource.loop = true;
        audioSource.Play();
    }

    public void StopBackgroundBgm()
    {
        audioSource.Stop();
    }
}
