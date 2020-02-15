using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

//Represents a blue door, and checks from the level manager if the player has completed the level so the door can be opened

public class EndDoor : MonoBehaviour
{
    private Interactable interaction;
    private KeyManager keyManager;
    private HeadsUpDisplay hudScript;
    private LevelManager levelManager;

    public GameObject fadeTransition;

    void Start()
    {
        keyManager = GameObject.FindObjectOfType<KeyManager>();
        hudScript = GameObject.FindObjectOfType<HeadsUpDisplay>();
        interaction = this.gameObject.GetComponent<Interactable>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();

        interaction.setCommandText("Open\n" + this.gameObject.name);
    }

    void Update()
    {
        interaction.setKey(keyManager.buttonCodes["Interact"]);

        if (interaction == hudScript.getCurrentInteractingObject())
        {
            if (Input.GetKeyDown(interaction.getKey()))
            {
                if (levelManager.getCanOpenDoor())
                {
                    this.gameObject.GetComponent<Animator>().Play("EndDoorOpening");
                    
                    if(SceneManager.GetActiveScene().buildIndex == 5)
                    {
                        fadeTransition.GetComponent<FadeTransition>().fadeToLevel(0);
                    }

                    else
                    {
                        fadeTransition.GetComponent<FadeTransition>().fadeToLevel(SceneManager.GetActiveScene().buildIndex + 1);
                    }  
                }

                else
                {
                    StartCoroutine(hudScript.notify("Cannot open this door yet"));
                }
            }
        }
    }
}
