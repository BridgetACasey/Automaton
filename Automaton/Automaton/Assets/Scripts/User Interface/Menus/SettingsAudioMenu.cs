using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.Audio;
using UnityEngine;

//Represents the audio menu
//Handles changes in master, sound effects, and music volume

public class SettingsAudioMenu : MonoBehaviour
{
    #region Variables

    private GameStateManager gameState;
    private KeyManager keys;

    [Header("Buttons")]
    public Button backButton;

    [Header("Menus")]
    public GameObject audioMenu;
    public GameObject settingsMenu;
    public AudioMixer audioMixer;
    public float masterVolume;
    public float effectsVolume;
    public float musicVolume;

    #endregion Variables

    void Start()
    {
        gameState = GameObject.FindObjectOfType<GameStateManager>();
        keys = GameObject.FindObjectOfType<KeyManager>();

        backButton.onClick.AddListener(goBack);
    }

    void Update()
    {
        if (keys.checkButtonCode("Menu/Go Back"))
        {
            goBack();
        }
    }

    public void setMasterVolume(float volume)
    {
        audioMixer.SetFloat("masterVolume", volume);
    }

    public void setEffectsVolume(float volume)
    {
        audioMixer.SetFloat("effectsVolume", volume);
    }

    public void setMusicVolume(float volume)
    {
        audioMixer.SetFloat("musicVolume", volume);
    }

    public void goBack()
    {
        gameState.setCurrentActiveMenu(settingsMenu);
    }

    public AudioSettingsData setAudioData()
    {
        AudioSettingsData data = new AudioSettingsData();

        data.masterVolume = this.masterVolume;
        data.effectsVolume = this.effectsVolume;
        data.musicVolume = this.musicVolume;

        return data;
    }
}

[System.Serializable]
public struct AudioSettingsData
{
    public float masterVolume;
    public float effectsVolume;
    public float musicVolume;
}
