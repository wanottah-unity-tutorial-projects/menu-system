﻿
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;


public class OptionsMenu : MonoBehaviour
{
    public Toggle fullscreenTog, vsyncTog;

    // array of screen resolutions
    public ResItem[] resolutions;

    // stores selected screen resolution
    public int selectedResolution;

    public Text resolutionLabel;

    public AudioMixer theMixer;

    public Slider mastSlider, musicSlider, sfxSlider;

    public Text mastLabel, musicLabel, sfxLabel;

    public AudioSource sfxLoop;



    // Start is called before the first frame update
    void Start()
    {
        fullscreenTog.isOn = Screen.fullScreen;

        if (QualitySettings.vSyncCount == 0)
        {
            vsyncTog.isOn = false;

        } 
        
        else
        {
            vsyncTog.isOn = true;
        }

        // search for resolution in list
        bool foundRes = false;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (Screen.width == resolutions[i].horizontal && Screen.height == resolutions[i].vertical)
            {
                foundRes = true;

                selectedResolution = i;

                UpdateResLabel();
            }
        }

        if (!foundRes)
        {
            resolutionLabel.text = Screen.width.ToString() + " x " + Screen.height.ToString();
        }


        if (PlayerPrefs.HasKey("MasterVol"))
        {
            theMixer.SetFloat("MasterVol", PlayerPrefs.GetFloat("MasterVol"));

            mastSlider.value = PlayerPrefs.GetFloat("MasterVol");
        }

        if (PlayerPrefs.HasKey("MusicVol"))
        {
            theMixer.SetFloat("MusicVol", PlayerPrefs.GetFloat("MusicVol"));

            musicSlider.value = PlayerPrefs.GetFloat("MusicVol");
        }

        if (PlayerPrefs.HasKey("SFXVol"))
        {
            theMixer.SetFloat("SFXVol", PlayerPrefs.GetFloat("SFXVol"));

            sfxSlider.value = PlayerPrefs.GetFloat("SFXVol");
        }

        mastLabel.text = (mastSlider.value + 80).ToString();

        musicLabel.text = (musicSlider.value + 80).ToString();

        sfxLabel.text = (sfxSlider.value + 80).ToString();
    }


    public void ResLeft()
    {
        selectedResolution--;

        if (selectedResolution < 0)
        {
            selectedResolution = 0;
        }

        UpdateResLabel();
    }


    public void ResRight()
    {
        selectedResolution++;

        if (selectedResolution > resolutions.Length - 1)
        {
            selectedResolution = resolutions.Length - 1;
        }

        UpdateResLabel();
    }


    public void UpdateResLabel()
    {
        resolutionLabel.text = resolutions[selectedResolution].horizontal.ToString() + " x " + resolutions[selectedResolution].vertical.ToString();
    }


    public void ApplyGraphics()
    {
        // apply fullscreen
        // Screen.fullScreen = fullscreenTog.isOn;

        if (vsyncTog.isOn)
        {
            QualitySettings.vSyncCount = 1;
        } 
        
        else
        {
            QualitySettings.vSyncCount = 0;
        }

        // set resolution
        Screen.SetResolution(resolutions[selectedResolution].horizontal, resolutions[selectedResolution].vertical, fullscreenTog.isOn);
    }


    public void SetMasterVol()
    {
        mastLabel.text = (mastSlider.value + 80).ToString();

        theMixer.SetFloat("MasterVol", mastSlider.value);

        PlayerPrefs.SetFloat("MasterVol", mastSlider.value);
    }


    public void SetMusicVol()
    {
        musicLabel.text = (musicSlider.value + 80).ToString();

        theMixer.SetFloat("MusicVol", musicSlider.value);

        PlayerPrefs.SetFloat("MusicVol", musicSlider.value);
    }


    public void SetSFXVol()
    {
        sfxLabel.text = (sfxSlider.value + 80).ToString();

        theMixer.SetFloat("SFXVol", sfxSlider.value);

        PlayerPrefs.SetFloat("SFXVol", sfxSlider.value);
    }


    public void PlaySFXLoop()
    {
        sfxLoop.Play();
    }


    public void StopSFXLoop()
    {
        sfxLoop.Stop();
    }


} // end if class



// class to store horizontal and vertical screen resolutions
// make visible in inspector
[Serializable]
public class ResItem
{
    public int horizontal, vertical;
}
