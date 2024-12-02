using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicHandler : MonoBehaviour
{
    bool isMusicOn = false;
    float musicVolume = 0.5f;
    void Start()
    {
        AudioSource audioSource = GetComponent<AudioSource>();
        if (PlayerPrefs.HasKey("Music"))
        {
            isMusicOn = PlayerPrefs.GetInt("Music") == 1;
        }
        if (PlayerPrefs.HasKey("MusicVolume"))
        {
            musicVolume = PlayerPrefs.GetFloat("MusicVolume");

        }
        if (isMusicOn)
        {
            audioSource.volume = musicVolume;
            audioSource.Play();
        }
    }

}
