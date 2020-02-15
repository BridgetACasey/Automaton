using UnityEngine;
using UnityEngine.SceneManagement;

public class GameStateManager : MonoBehaviour
{
    //This is the overall game manager, used to set up and initiate core elements of gameplay.

    #region Variables

    private bool gameIsPaused, inMenu, cursorActive;
    private CameraManager camera;
    private DialogueManager dialogueManager;
    private Codex codex;
    private GameObject currentMenu;

    public GameObject[] menus;
    public GameObject canvas;

    #endregion Variables

    void Start ()
    {
        codex = GameObject.FindObjectOfType<Codex>();
        dialogueManager = GameObject.FindObjectOfType<DialogueManager>();
        camera = GameObject.FindObjectOfType<CameraManager>();
        cursorActive = false;
        gameIsPaused = false;
        inMenu = false;
	}


    void Update()
    {
        if(inMenu)
        {
            canvas.GetComponent<PauseMenu>().enabled = true;
            canvas.GetComponent<Codex>().enabled = false;
        }

        else if(codex.inCodexView)
        {
            canvas.GetComponent<PauseMenu>().enabled = false;
            canvas.GetComponent<Codex>().enabled = true;
        }

        else
        {
            canvas.GetComponent<PauseMenu>().enabled = true;
            canvas.GetComponent<Codex>().enabled = true;
        }

         checkMenus();

        if (inMenu || dialogueManager.inDialogue || codex.inCodexView || SceneManager.GetActiveScene().name == "Main Menu")
        {
            setCursorActive(true);
        }

        else
        {
            setCursorActive(false);
        }
	}

    #region Menus

    //Checks to see if any menus are currently active in game, and pauses the game accordingly
    public void checkMenus()
    {
         int totalActiveMenus = 0;

        /* If there is at least one active menu, the game timer will stop and remain in a 'menu state.'
         * If the total number of active menus is none, the in-game time will resume.
         */

        foreach(GameObject current in menus)
        {
            if(current.activeSelf)
            {
                totalActiveMenus++;

                inMenu = true;
                gameIsPaused = true;
                Time.timeScale = 0f;
            }

            else if(!current.activeSelf && totalActiveMenus == 0)
            {
                inMenu = false;
                gameIsPaused = false;
                Time.timeScale = 1f;
            }
        }
    }

    public void setCurrentActiveMenu(GameObject activeMenu)
    {
        this.currentMenu = activeMenu;

        for(int counter = 0; counter < menus.Length; counter++)
        {
            if (menus[counter] == currentMenu)
            {
                menus[counter].SetActive(true);
            }

            else if(menus[counter] != currentMenu)
            {
                menus[counter].SetActive(false);
            }
        }
    }

    public void setCursorActive(bool active)
    {
        cursorActive = active;

        if(cursorActive)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }

        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    #endregion Menus

    #region Getters and Setters

    public void setInMenu(bool menuStatus)
    {
        inMenu = menuStatus;
    }

    public bool getInMenu()
    {
        return inMenu;
    }

    public void setGameIsPaused(bool pauseStatus)
    {
        gameIsPaused = pauseStatus;
    }

    public bool getGameIsPaused()
    {
        return gameIsPaused;
    }

    #endregion Getters and Setters

    public GameStateData setGameStateData()
    {
        GameStateData data = new GameStateData();

        data.gameIsPaused = this.gameIsPaused;
        data.inMenu = this.inMenu;
        data.cursorActive = this.cursorActive;
        data.currentMenu = this.currentMenu;

        return data;
    }
}

[System.Serializable]
public struct GameStateData
{
    public bool gameIsPaused, inMenu, cursorActive;
    public GameObject currentMenu;
}
