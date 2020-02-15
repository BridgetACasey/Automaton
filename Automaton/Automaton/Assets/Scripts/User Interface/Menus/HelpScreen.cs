using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//Represents the help screen.
//Handles the UI object being activated and deactivated.

public class HelpScreen : MonoBehaviour
{
    #region Variables

    private GameStateManager gameState;
    private KeyManager keys;

    [Header("Buttons")]
    public Button backButton;

    [Header("Menus")]
    public GameObject helpScreen;
    public GameObject settingsMenu;

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

    public void goBack()
    {
        gameState.setCurrentActiveMenu(settingsMenu);
    }
}
