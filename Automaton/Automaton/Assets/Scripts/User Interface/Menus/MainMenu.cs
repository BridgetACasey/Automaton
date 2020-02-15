using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

//Represents the main menu, handles transitioning to the main game and qutting the application

public class MainMenu : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settingsMenu;
    public GameObject quitScreen;
    public FadeTransition transition;

    public void playGame()
    {
        transition.fadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void quitGame()
    {
        Debug.Log("QUIT TIME!");
        Application.Quit();
    }
}
