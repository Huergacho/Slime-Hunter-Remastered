using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public enum AudioEnum
{
    Music,
    Ambience,
    SFX,
    Shoots
}

[System.Serializable]
public class Audio
{
    public AudioSource Source;
    public AudioEnum State;
    public Slider slider;
}

public class AudioController : MonoBehaviour
{
    private Dictionary<AudioEnum, Audio> audioDictionary = new Dictionary<AudioEnum, Audio>();
    [SerializeField] private Audio[] _audios;

    private void Start()
    {
        InitializeDictionaries();
        foreach (var audio in audioDictionary)
        {
            LoadValues(audio.Key);
            audio.Value.slider.value = audio.Value.Source.volume;
        }
    }

    private void VolumeController(AudioEnum sound, float desiredVolume)
    {
        PlayerPrefs.SetFloat("" + sound + "VolumeValue",desiredVolume);
        LoadValues(sound);
    }

    public void SaveVolume()
    {
        foreach (var audio in _audios)
        {
            VolumeController(audio.State,audio.slider.value);
        }
    }
    private void LoadValues(AudioEnum sound)
    {
        float volumeValue = PlayerPrefs.GetFloat("" + sound + "VolumeValue");
        audioDictionary[sound].Source.volume = volumeValue;

    }
    private void InitializeDictionaries()
    {
        foreach (var audio in _audios)
        {
            audioDictionary.Add(audio.State,audio);
        }
    }
    public void ReproduceOnce(AudioEnum source,AudioClip sound)
    {
        if (sound == null)
        {
            print("No existe tal sonido");
            return;
        }
        audioDictionary[source].Source.PlayOneShot(sound);
    }
}
