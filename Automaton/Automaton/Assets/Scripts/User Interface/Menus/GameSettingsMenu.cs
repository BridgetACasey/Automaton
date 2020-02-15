using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//Handles the button controls and navigation to other menus from the general settings menu

public class GameSettingsMenu : MonoBehaviour
{
    #region Variables

    private GameStateManager gameState;

    [Header("Buttons")]
    public Button videoButton, audioButton, controlsButton, helpButton, backButton;

    [Header("Menus")]
    public GameObject pauseMenu, settingsMenu, videoMenu, audioMenu, controlsMenu, helpMenu;

    #endregion Variables

    void Start()
    {
        gameState = GameObject.FindObjectOfType<GameStateManager>();

        videoButton.onClick.AddListener(video);
        audioButton.onClick.AddListener(audio);
        controlsButton.onClick.AddListener(controls);
        helpButton.onClick.AddListener(help);
        backButton.onClick.AddListener(goBack);
    }

    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            goBack();
        }
    }

    private void video()
    {
        gameState.setCurrentActiveMenu(videoMenu);
    }

    private void audio()
    {
        gameState.setCurrentActiveMenu(audioMenu);
    }

    private void controls()
    {
        gameState.setCurrentActiveMenu(controlsMenu);
    }

    private void help()
    {
        gameState.setCurrentActiveMenu(helpMenu);
    }

    public void goBack()
    {
        gameState.setCurrentActiveMenu(pauseMenu);
    }
}
