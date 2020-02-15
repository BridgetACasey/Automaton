using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

//Holds the data for a notepad, including whether or not it has been collected already

public class Notepad : MonoBehaviour
{
    private Interactable interaction;
    private KeyManager keyManager;
    private HeadsUpDisplay hudScript;
    private Codex codex;

    private bool collected;

    public int index;

    void Start()
    {
        keyManager = GameObject.FindObjectOfType<KeyManager>();
        hudScript = GameObject.FindObjectOfType<HeadsUpDisplay>();
        codex = GameObject.FindObjectOfType<Codex>();
        interaction = this.gameObject.GetComponent<Interactable>();

        collected = false;
        interaction.setCommandText("Read\n" + this.gameObject.name);
    }

    void Update()
    {
        interaction.setKey(keyManager.buttonCodes["Interact"]);

        if (interaction == hudScript.getCurrentInteractingObject())
        {
            if (Input.GetKeyDown(interaction.getKey()))
            {
                if (!collected)
                {
                    codex.getSceneNotepad().setCollected(true);
                    NotepadManagement.updateCollectedNotepads(codex.getSceneNotepad().index);
                    StartCoroutine(hudScript.notify("Copied data from " + codex.getSceneNotepad().name.ToString()));
                }

                else
                {
                    StartCoroutine(hudScript.notify("Already copied data"));
                }
            }
        }
    }

    public void setCollected(bool collected)
    {
        this.collected = collected;
    }

    public bool getCollected()
    {
        return collected;
    }
}
