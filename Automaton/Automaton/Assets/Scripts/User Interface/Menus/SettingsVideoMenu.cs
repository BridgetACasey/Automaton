using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//Represents the video settings menu
//Handles changes in resolution, graphics, and fullscreen mode

public class SettingsVideoMenu : MonoBehaviour
{
    #region Variables

    private GameStateManager gameState;
    private KeyManager keys;
    private int defaultResolution;
    private Resolution[] resolutions;
    private List<string> resolutionOptions;

    private bool checkIsFullscreen;
    private Resolution currentResolution;
    private int currentGraphicsQualityLevel;

    [Header("Buttons")]
    public Button lowQuality;
    public Button mediumQuality;
    public Button highQuality;
    public Button backButton;

    [Header("Menus")]
    public GameObject videoMenu;
    public GameObject settingsMenu;
    public Dropdown resolutionDropdown;

    #endregion Variables

    void Start()
    {
        resolutions = Screen.resolutions;
        resolutionOptions = new List<string>();

        defaultResolution = 0;
        for(int counter = 0; counter < resolutions.Length; counter++)
        {
            string option = resolutions[counter].width + " x " + resolutions[counter].height;
            resolutionOptions.Add(option);

            if(resolutions[counter].width == Screen.currentResolution.width && resolutions[counter].height == Screen.currentResolution.height)
            {
                defaultResolution = counter;
            }
        }

        resolutionDropdown.AddOptions(resolutionOptions);
        resolutionDropdown.value = defaultResolution;
        resolutionDropdown.RefreshShownValue();

        gameState = GameObject.FindObjectOfType<GameStateManager>();
        keys = GameObject.FindObjectOfType<KeyManager>();

        lowQuality.onClick.AddListener(setQualityLow);
        mediumQuality.onClick.AddListener(setQualityMedium);
        highQuality.onClick.AddListener(setQualityHigh);

        backButton.onClick.AddListener(goBack);
    }

    void Update()
    {
        if(keys.checkButtonCode("Menu/Go Back"))
        {
            goBack();
        }
    }

    public void setFullscreen(bool isFullscreen)
    {
        this.checkIsFullscreen = isFullscreen;
        Screen.fullScreen = isFullscreen;
    }

    public void setResolution(int newResolution)
    {
        Resolution resolution = resolutions[newResolution];
        this.currentResolution = resolution;
        Screen.SetResolution(resolution.width, resolution.height, Screen.fullScreen);
    }

    public void setQualityLow()
    {
        this.currentGraphicsQualityLevel = 0;
        QualitySettings.SetQualityLevel(0);
    }

    public void setQualityMedium()
    {
        this.currentGraphicsQualityLevel = 1;
        QualitySettings.SetQualityLevel(1);
    }

    public void setQualityHigh()
    {
        this.currentGraphicsQualityLevel = 2;
        QualitySettings.SetQualityLevel(2);
    }

    public void goBack()
    {
        gameState.setCurrentActiveMenu(settingsMenu);
    }

    public VideoSettingsData setVideoSettings()
    {
        VideoSettingsData data = new VideoSettingsData();

        data.checkIsFullscreen = this.checkIsFullscreen;
        data.currentResolution = this.currentResolution;
        data.currentGraphicsQualityLevel = this.currentGraphicsQualityLevel;

        return data;
    }
}

[System.Serializable]
public struct VideoSettingsData
{
    public bool checkIsFullscreen;
    public Resolution currentResolution;
    public int currentGraphicsQualityLevel;
}
