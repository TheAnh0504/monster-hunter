using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSetting : MonoBehaviour
{
    [SerializeField] private AudioMixer myMixer;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private Slider SFXSlider;
    [SerializeField] private Slider volumeSlider;

    private void Start()
    {
        
        if (PlayerPrefs.HasKey("musicVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetMusicVolume();
            SetSFXVolume();
            SetvolumeVolume();
        }
    }

    public void SetvolumeVolume()
    {
        float volume = volumeSlider.value;
        myMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volumeVolume", volume);
    }

    public void SetMusicVolume()
    {
        float volume = musicSlider.value;
        myMixer.SetFloat("music", volume);
        PlayerPrefs.SetFloat("musicVolume", volume );
    }
    public void SetSFXVolume()
    {
        float volume = SFXSlider.value;
        myMixer.SetFloat("SFX", volume);
        PlayerPrefs.SetFloat("SFXVolume", volume);
    }

    private void LoadVolume()
    {
        musicSlider.value = PlayerPrefs.GetFloat("musicVolume");
        SFXSlider.value = PlayerPrefs.GetFloat("SFXVolume");
        volumeSlider.value = PlayerPrefs.GetFloat("volumeVolume");

        SetvolumeVolume();
        SetMusicVolume();
        SetSFXVolume();
    }
    
}
