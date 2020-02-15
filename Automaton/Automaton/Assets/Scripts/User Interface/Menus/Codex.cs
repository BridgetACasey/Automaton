using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//Handles the transitions from the codex screen to the notepad view screen, and checks if the have been collected first

public class Codex : MonoBehaviour
{
    private GameStateManager gameState;
    private PlayerMovement playerMove;
    private CameraManager camera;
    private KeyManager keys;
    private NotepadView view;
    private HeadsUpDisplay hudScript;

    public Notepad inSceneNotepad;

    [Header("Buttons")]
    public Button Pad1;
    public Button Pad2;
    public Button Pad3;
    public Button Pad4;
    public Button Pad5;
    public Button backButton;

    [Header("Menu Screens")]
    public GameObject pauseMenu;
    public GameObject mainScreen;
    public GameObject notepadView;
    public bool inCodexView;

    

    void Start()
    {
        inCodexView = false;

        playerMove = GameObject.FindObjectOfType<PlayerMovement>();
        camera = GameObject.FindObjectOfType<CameraManager>();
        gameState = GameObject.FindObjectOfType<GameStateManager>();
        hudScript = GameObject.FindObjectOfType<HeadsUpDisplay>();
        keys = GameObject.FindObjectOfType<KeyManager>();
        view = GameObject.FindObjectOfType<NotepadView>();

        Pad1.onClick.AddListener(() => viewNotepad(0));
        Pad2.onClick.AddListener(() => viewNotepad(1));
        Pad3.onClick.AddListener(() => viewNotepad(2));
        Pad4.onClick.AddListener(() => viewNotepad(3));
        Pad5.onClick.AddListener(() => viewNotepad(4));

        backButton.onClick.AddListener(resumeGame);
    }

    void Update()
    {
        if (!gameState.getInMenu())
        {
            if (keys.checkButtonCode("Inventory"))
            {
                checkPause();
            }
        }
    }

    public void checkPause()
    {
        if (inCodexView && mainScreen.activeSelf)
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
        view.gameObject.SetActive(false);
        mainScreen.SetActive(true);
        playerMove.setCanMove(false);
        camera.enabled = false;
        inCodexView = true;
    }

    public void resumeGame()
    {
        view.gameObject.SetActive(false);
        mainScreen.SetActive(false);
        playerMove.setCanMove(true);
        camera.enabled = true;
        inCodexView = false;
    }

    public void viewNotepad(int index)
    {
        view.gameObject.SetActive(true);

        if (NotepadManagement.getCollectedIndexOf(index))
        {
            mainScreen.SetActive(false);
            notepadView.SetActive(true);
            view.loadTextForNotepad(index);
        }

        else
        {
            StartCoroutine(hudScript.notify("Not collected this pad yet"));
        }
    }

    public Notepad getSceneNotepad()
    {
        return inSceneNotepad;
    }
}
