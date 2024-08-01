using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class SettingsMenu : MonoBehaviour
{
    public TMP_Text topSever;
    public int maxScore;
    public string nameMaxScore;

    public AudioMixer audioMixer;
    [SerializeField] private Slider volumeSlider;

    Resolution[] resolutions;
    public TMP_Dropdown resolutionDropdown;
    
    private void Start()
    {
        
        // hiện tên + điểm top sever
        maxScore = PlayerPrefs.GetInt("maxScore");
        nameMaxScore = PlayerPrefs.GetString("nameMaxScore");
        if (maxScore != 0)
        {
            topSever.text = "Top 1 Sever: " + nameMaxScore + maxScore + " Scores";
        }
        // setting
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        List<string> options = new List<string>();


        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].height == Screen.currentResolution.height &&
                resolutions[i].width  == Screen.currentResolution.width)
            {
                currentResolutionIndex = i;
            }
        }
        resolutionDropdown.AddOptions(options);
        currentResolutionIndex = PlayerPrefs.GetInt("resolution");
        PlayerPrefs.SetInt("resolution", currentResolutionIndex);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        //âm thanh
        if (PlayerPrefs.HasKey("volumeVolume"))
        {
            LoadVolume();
        }
        else
        {
            SetvolumeVolume();
        }
    }

    public void SetvolumeVolume()
    {
        float volume = volumeSlider.value;
        audioMixer.SetFloat("volume", volume);
        PlayerPrefs.SetFloat("volumeVolume", volume);
    }

    private void LoadVolume()
    {
        volumeSlider.value = PlayerPrefs.GetFloat("volumeVolume");

        SetvolumeVolume();
    }

    public void SetResolution(int resolutionIndex)
    {
        PlayerPrefs.SetInt("resolution", resolutionIndex);
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    /*public void SetVolume (float volume)
    {
        audioMixer.SetFloat("volume", volume);
    }*/

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
}
