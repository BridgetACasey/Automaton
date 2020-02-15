using System.Collections.Generic;
using UnityEngine.UI;
using System;
using UnityEngine;

//Handles the key rebinding process for any button that has previously been selected and passed into this class from the controls menu

public class KeyBindingsMenu : MonoBehaviour
{
    #region Variables

    private KeyManager keyManager;
    private InputData inputData;
    private SettingsControlsMenu controls;
    private GameStateManager gameState;

    private Dictionary<string, Text> buttonToText;
    private string[] totalButtonNames;
    private string buttonToRebind, buttonName, keyDisplay;

    [Header("Buttons")]
    public Button backButton;
    public Button keyBindButton; //The button that acts as a display for the rebinding process

    [Header("Text")]
    public Text keyBindText;
    public Text keyBindButtonText;

    [Header("Menus")]
    public GameObject keyBindMenu;
    public GameObject controlsMenu;

    #endregion Variables

    void OnEnable()
    {
        buttonToRebind = null;
        gameState = GameObject.FindObjectOfType<GameStateManager>();
        keyManager = GameObject.FindObjectOfType<KeyManager>();
        controls = controlsMenu.GetComponent<SettingsControlsMenu>();

        backButton.onClick.AddListener(goBack);
        totalButtonNames = keyManager.getAllButtonNames();

        Debug.Log("Active Button: " + keyManager.getCurrentActiveButton().name);

        buttonName = keyManager.getCurrentActiveButton().name;
        keyBindText.text = buttonName;

        for (int counter = 0; counter < totalButtonNames.Length; counter++)
        {
            if (buttonName == totalButtonNames[counter])
            {
                keyBindButtonText.GetComponent<Text>().text = keyManager.getKeyButtonName(buttonName);
            }
        }

        keyBindButton.onClick.AddListener(() => {startRebindFor(buttonName);});
    }

    void Update()
    {
        checkKeyRebinds();
    }

    private void startRebindFor(string buttonName)
    {
        Debug.Log("StartRebindFor: " + buttonName);
        
        //if the button text from the menu matches a name from the keys dictionary, that is the key we are rebinding

        buttonToRebind = buttonName;
    }

    private void checkKeyRebinds()
    {
        if(buttonToRebind != null)
        {
            if(Input.anyKeyDown)
            {
                // Loop through all possible keys and see if it was pressed down
                foreach(KeyCode keyCode in Enum.GetValues(typeof(KeyCode)))
                {
                    if(Input.GetKeyDown(keyCode))
                    {
                        keyManager.setKeyBinding(buttonToRebind, keyCode);
                        keyBindButtonText.GetComponent<Text>().text = keyCode.ToString();
                        buttonToRebind = null;
                        break;
                    }
                }
            }
        }
    }

    private void goBack()
    {
        gameState.setCurrentActiveMenu(controlsMenu);
    }
}
