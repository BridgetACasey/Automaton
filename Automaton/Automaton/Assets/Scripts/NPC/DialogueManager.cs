using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

//Manages and selects what dialogue lines to output based on the current level
//These dialogue lines also change if the player has selected a response

public class DialogueManager : MonoBehaviour
{
    private KeyManager keys;
    private PlayerMovement playerMove;
    private GameStateManager gameState;
    private Sigma sigma;
    private CameraManager cameraManager;
    private Queue<string> dialogueLines;
    private Text nameText;
    public bool introComplete;
    private string[] playerResponses;
    private string[] introLines;
    private string[] response1Lines;
    private string[] response2Lines;
    private string[] response3Lines;
    private string[] nextLines;

    [Header("References")]
    public Dialogue dialogue;
    public GameObject dialogueBox;
    public GameObject responseBox;
    public GameObject dialogueText;
    public Button response1;
    public Button response2;
    public Button response3;
    public Button response4;
    public AudioSource enterAudio;
    public AudioSource exitAudio;
    public bool inDialogue;
    
    void Start()
    {
        gameState = GameObject.FindObjectOfType<GameStateManager>();
        sigma = GameObject.FindObjectOfType<Sigma>();
        keys = GameObject.FindObjectOfType<KeyManager>();
        cameraManager = GameObject.FindObjectOfType<CameraManager>();
        playerMove = GameObject.FindObjectOfType<PlayerMovement>();
        dialogueLines = new Queue<string>();
        nameText = dialogueBox.GetComponentInChildren<Text>();

        introComplete = false;
        inDialogue = false;
    }

    
    void Update()
    {
        if(inDialogue)
        {
            sigma.GetComponent<Interactable>().enabled = false;

            if(keys.checkButtonCode("Interact"))
            {
                displayNextLine();
            }
        }
    }

    public void startConversation()
    {
        dialogueLines.Clear();
        loadLines();

        enterAudio.Play();

        if(!introComplete)
        {
            nextLines = introLines;
        }

        nameText.text = dialogue.npcName.ToString();
        dialogueBox.SetActive(true);
        cameraManager.enabled = false;
        playerMove.setCanMove(false);
        inDialogue = true;

        foreach (string line in nextLines)
        {
            dialogueLines.Enqueue(line);
        }

        displayNextLine();
    }

    public void displayNextLine()
    {
        if(dialogueLines.Count == 0)
        {
            introComplete = true;
            checkResponse();
            return;
        }

        StopAllCoroutines();
        string line = dialogueLines.Dequeue();
        StartCoroutine(GetComponent<TextTyper>().printText(dialogueText, line, 0f, 60));
    }

    public void exitConversation()
    {
        exitAudio.Play();
        sigma.GetComponent<Interactable>().enabled = true;
        introComplete = false;
        cameraManager.enabled = true;
        playerMove.setCanMove(true);
        dialogueBox.SetActive(false);
        responseBox.SetActive(false);
        inDialogue = false;
    }

    public void checkResponse()
    {
        dialogueBox.SetActive(false);
        responseBox.SetActive(true);

        response1.onClick.AddListener(() => selectNextLines(response1Lines));
        response2.onClick.AddListener(() => selectNextLines(response2Lines));
        response3.onClick.AddListener(() => selectNextLines(response3Lines));
        response4.onClick.AddListener(exitConversation);
    }

    public void setResponseText(string response1text, string response2text, string response3text, string response4text)
    {
        response1.GetComponentInChildren<Text>().text = response1text;
        response2.GetComponentInChildren<Text>().text = response2text;
        response3.GetComponentInChildren<Text>().text = response3text;
        response4.GetComponentInChildren<Text>().text = response4text;
    }

    public void loadLines()
    {
        //Decide lines based on current loaded level

        if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            introLines = dialogue.loadTutorialLines_Intro();
            response1Lines = dialogue.loadTutorialLines_Response1();
            response2Lines = dialogue.loadTutorialLines_Response2();
            response3Lines = dialogue.loadTutorialLines_Response3();
        }

        else if (SceneManager.GetActiveScene().name == "Level 1")
        {
            introLines = dialogue.loadLevel1Lines_Intro();
            response1Lines = dialogue.loadLevel1Lines_Response1();
            response2Lines = dialogue.loadLevel1Lines_Response2();
            response3Lines = dialogue.loadLevel1Lines_Response3();
        }

        else if (SceneManager.GetActiveScene().name == "Level 2")
        {
            introLines = dialogue.loadLevel2Lines_Intro();
            response1Lines = dialogue.loadLevel2Lines_Response1();
            response2Lines = dialogue.loadLevel2Lines_Response2();
            response3Lines = dialogue.loadLevel2Lines_Response3();
        }

        else if (SceneManager.GetActiveScene().name == "Level 3")
        {
            introLines = dialogue.loadLevel3Lines_Intro();
            response1Lines = dialogue.loadLevel3Lines_Response1();
            response2Lines = dialogue.loadLevel3Lines_Response2();
            response3Lines = dialogue.loadLevel3Lines_Response3();
        }

        else if (SceneManager.GetActiveScene().name == "End Scene")
        {
            introLines = dialogue.loadEndSceneLines_Intro();
            response1Lines = dialogue.loadEndSceneLines_Response1();
            response2Lines = dialogue.loadEndSceneLines_Response2();
            response3Lines = dialogue.loadEndSceneLines_Response3();
        }
    }

    public void selectNextLines(string[] lines)
    {
        this.nextLines = lines;
        responseBox.SetActive(false);
        startConversation();
    }
}
