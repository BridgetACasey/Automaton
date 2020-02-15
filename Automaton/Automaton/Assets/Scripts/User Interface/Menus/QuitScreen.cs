using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//Quit screen that pops up once the player tries to quit in game to check if they are sure
//Handles button presses between yes, no, and back

public class QuitScreen : MonoBehaviour
{
    #region Variables

    private GameStateManager gameState;
    private KeyManager keys;

    [Header("Buttons")]
    public Button yes;
    public Button no;

    [Header("Menus")]
    public GameObject pauseMenu;
    public GameObject quitScreen;

    #endregion Variables

    void Start()
    {
        gameState = GameObject.FindObjectOfType<GameStateManager>();
        keys = GameObject.FindObjectOfType<KeyManager>();

        yes.onClick.AddListener(yesButton);
        no.onClick.AddListener(noButton);

        quitScreen.SetActive(true);
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        goBack();
    }

    private void yesButton()
    {
        Debug.Log("QUIT TIME!");
        Application.Quit();
    }

    private void noButton()
    {
        pauseMenu.SetActive(true);
        quitScreen.SetActive(false);
    }

    public void goBack()
    {
        if(keys.checkButtonCode("Menu/Go Back"))
        {
            pauseMenu.SetActive(true);
            quitScreen.SetActive(false);
        }
    }
}
