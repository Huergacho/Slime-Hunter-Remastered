using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[RequireComponent(typeof(AudioSource))]
public class AudioManager : MonoBehaviour
{
    private AudioSource _audioSource;
    private void Start()
    {
        _audioSource = GetComponent<AudioSource>();
    }
    public void ReproduceOnce(AudioClip sound)
    {
        if (sound == null)
        {
            print("No existe tal sonido");
            return;
        }
        _audioSource.PlayOneShot(sound);
    }
}
