using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//Represents the pause menu
//Handles transitions to setting menu, back to gameplay, and quit screen through button presses

public class PauseMenu : MonoBehaviour
{
    #region Variables

    private GameStateManager gameState;
    private KeyManager keys;

    [Header("Buttons")]
    public Button resumeButton;
    public Button settingsButton;
    public Button quitButton;

    [Header("Menu Screens")]
    public GameObject pauseMenu;
    public GameObject settingsMenu;
    public GameObject quitScreen;

    #endregion Variables

    void Start()
    {
        gameState = GameObject.FindObjectOfType<GameStateManager>();
        keys = GameObject.FindObjectOfType<KeyManager>();

        resumeButton.onClick.AddListener(checkPauseMenu);
        settingsButton.onClick.AddListener(settings);
        quitButton.onClick.AddListener(quitGame);
    }

    void Update()
    {
        if(keys.checkButtonCode("Menu/Go Back"))
        {
            checkPauseMenu();
        }       
    }

    private void checkPauseMenu()
    {
        if (gameState.getGameIsPaused())
        {
            resumeGame();
        }

        else
        {
            pauseGame();
        }
    }

    private void pauseGame()
    {
        gameState.setCurrentActiveMenu(pauseMenu);
    }

    private void resumeGame()
    {
        pauseMenu.SetActive(false);
    }

    private void saveGame()
    {

    }
    
    private void loadGame()
    {

    }

    private void settings()
    {
        gameState.setCurrentActiveMenu(settingsMenu);
    }

    private void quitGame()
    {
        gameState.setCurrentActiveMenu(quitScreen);
    }
}
