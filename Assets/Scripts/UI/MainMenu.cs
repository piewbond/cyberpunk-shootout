using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    public AudioMixer audioMixer;

    public TMP_Dropdown resolutionDropdown;
    Resolution[] resolutions;

    public GameObject musicButtonToggle;
    public GameObject FullscreenButtonToggle;
    bool isMusicOn;

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].width >= 800)
            {
                string option = resolutions[i].width + " x " + resolutions[i].height + "    @" + resolutions[i].refreshRateRatio + "Hz";
                options.Add(option);
            }

            if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
                currentResolutionIndex = i;
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();


        if (Screen.fullScreen)
        {
            FullscreenButtonToggle.SetActive(true);
        }
        else
        {
            FullscreenButtonToggle.SetActive(false);
        }

        if (PlayerPrefs.HasKey("Music"))
        {
            isMusicOn = PlayerPrefs.GetInt("Music") == 1;
        }
        if (isMusicOn)
        {
            musicButtonToggle.SetActive(true);
        }
        else
        {
            musicButtonToggle.SetActive(false);
        }
    }

    public void PlayGame()
    {
        SceneManager.LoadScene("GameplayScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetGameMode(int mode)
    {
        PlayerPrefs.SetInt("GameMode", mode);
    }

    public void SetVolume(float volume)
    {
        PlayerPrefs.SetFloat("Volume", volume);
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void SetFullscreen()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void ToggleMusic()
    {
        isMusicOn = !isMusicOn;
        PlayerPrefs.SetInt("Music", isMusicOn ? 1 : 0);
        Debug.Log("Music is on: " + isMusicOn);
    }

}
