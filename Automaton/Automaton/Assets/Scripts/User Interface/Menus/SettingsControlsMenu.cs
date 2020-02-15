using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//Represents the controls menu
//Handles transition to the key rebind menu based on which button is pressed

public class SettingsControlsMenu : MonoBehaviour
{
    #region Variables

    private GameStateManager gameState;
    private KeyManager keys;

    [Header("Buttons")]
    public Button backButton;

    public Button grappleButton;
    public Button jumpButton;
    public Button interactButton;
    public Button inventoryButton;
    public Button menuButton;
    public Button restartButton;

    [Header("Key Text")]
    public Text grappleKeyText;
    public Text jumpKeyText;
    public Text interactKeyText;
    public Text inventoryKeyText;
    public Text menuKeyText;
    public Text restartKeyText;

    [Header("Menus")]
    public GameObject controlsMenu;
    public GameObject settingsMenu;
    public GameObject keyBindMenu;

    #endregion Variables

    void OnEnable()
    {
        gameState = GameObject.FindObjectOfType<GameStateManager>();
        keys = GameObject.FindObjectOfType<KeyManager>();

        backButton.onClick.AddListener(goBack);

        grappleKeyText.text = keys.getKeyButtonName("Grapple").ToString();
        jumpKeyText.text = keys.getKeyButtonName("Jump").ToString();
        interactKeyText.text = keys.getKeyButtonName("Interact").ToString();
        inventoryKeyText.text = keys.getKeyButtonName("Inventory").ToString();
        menuKeyText.text = keys.getKeyButtonName("Menu/Go Back").ToString();
        restartKeyText.text = keys.getKeyButtonName("Restart").ToString();

        grappleButton.onClick.AddListener(() => goToKeyBind(grappleButton));
        jumpButton.onClick.AddListener(() => goToKeyBind(jumpButton));
        interactButton.onClick.AddListener(() => goToKeyBind(interactButton));
        inventoryButton.onClick.AddListener(() => goToKeyBind(inventoryButton));
        menuButton.onClick.AddListener(() => goToKeyBind(menuButton));
        restartButton.onClick.AddListener(() => goToKeyBind(restartButton));
    }

    void Update()
    {
        if (keys.checkButtonCode("Menu/Go Back"))
        {
            goBack();
        }
    }

    public void goToKeyBind(Button pressedButton)
    {
        keys.setCurrentActiveButton(pressedButton);
        Debug.Log("Pressed button " + pressedButton.name);
        Debug.Log("Current Button: " + pressedButton);
        gameState.setCurrentActiveMenu(keyBindMenu);
    }

    public void goBack()
    {
        gameState.setCurrentActiveMenu(settingsMenu);
    }
}
