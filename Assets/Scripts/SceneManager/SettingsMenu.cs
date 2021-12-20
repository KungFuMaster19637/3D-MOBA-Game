using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;
using TMPro;

public class SettingsMenu : MonoBehaviour
{
    public AudioMixer backgroundMusicMixer;
    public AudioMixer soundEffectsMixer;
    [SerializeField] private GameObject pauseMenu;

    Resolution[] resolutions;

    //public Dropdown resolutionDropdown;
    public TMP_Dropdown resolutionDropdown;

    private void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }

        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }
    
    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }
    public void SetQuality(int qualityIndex)
    {
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullScreen(bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }
    public void SetBackgroundVolume(float volume)
    {
        backgroundMusicMixer.SetFloat("Volume", Mathf.Log10(volume) * 20); //Volume scales logarithmicly 
    }

    public void SetSFXVolume(float volume)
    {
        soundEffectsMixer.SetFloat("Volume", Mathf.Log10(volume) * 20); //Volume scales logarithmicly 
    }

    public void SettingsBack()
    {
        pauseMenu.GetComponent<PauseMenu>().SettingsBack();
    }
}
