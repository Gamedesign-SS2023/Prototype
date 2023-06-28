using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundManager : MonoBehaviour
{
    [SerializeField] Slider VolumeSlider;
    [SerializeField] Image SoundOn;
    [SerializeField] Image SoundOff;
    private bool muted = false;

    private void Start()
    {
        if (!PlayerPrefs.HasKey("musicVolume"))
        {
            PlayerPrefs.SetFloat("musicVolume", 1);
            Load();
        }
        else
        {
            Load();
        }
        if (!PlayerPrefs.HasKey("muted"))
        {
            PlayerPrefs.SetFloat("muted", 0);
            Load();
        }
        else
        {
            Load();
        }
        UpdateButtonIcon();
        AudioListener.pause = muted;
    }

    public void ChangeVolume()
    {
        AudioListener.volume = VolumeSlider.value;
        Save();
    }

    private void Load()
    {
        VolumeSlider.value = PlayerPrefs.GetFloat("musicVolume");
    }
     
    private void Save()
    {
        PlayerPrefs.SetFloat("musicVolume",VolumeSlider.value);
    }
    public void OnButtonPress()
    {
        if (muted == false)
        {
            muted = true;
            AudioListener.pause = true;
        }
        else
        {
            muted= false;
            AudioListener.pause = false;
        }
        SaveButton();
        UpdateButtonIcon();
    }

    private void LoadButton()
    {
        muted = PlayerPrefs.GetInt("muted") == 1;
    }

    private void SaveButton() {
        PlayerPrefs.SetInt("muted", muted ? 1 : 0);
    }

    private void UpdateButtonIcon()
    {
        if(muted)
        {
            SoundOn.enabled = false;
            SoundOff.enabled = true;
        }
        else
        {
            SoundOn.enabled = true;
            SoundOff.enabled = false;
        }
    }
}

