using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Represents the data for the computer object in the final room.
//Once interacted with, the blue door in the final room becomes accessible.

public class ControlPanel : MonoBehaviour
{
    private Interactable interaction;
    private KeyManager keyManager;
    private HeadsUpDisplay hudScript;
    private LevelManager levelManager;

    public AudioSource audio;
    public bool hasOverridden;

    void Start()
    {
        keyManager = GameObject.FindObjectOfType<KeyManager>();
        hudScript = GameObject.FindObjectOfType<HeadsUpDisplay>();
        interaction = this.gameObject.GetComponent<Interactable>();
        levelManager = GameObject.FindObjectOfType<LevelManager>();

        interaction.setCommandText("Override Lockdown\n" + this.gameObject.name);
        hasOverridden = false;
    }

    void Update()
    {
        interaction.setKey(keyManager.buttonCodes["Interact"]);

        if (interaction == hudScript.getCurrentInteractingObject())
        {
            if (Input.GetKeyDown(interaction.getKey()))
            {
                StartCoroutine(hudScript.notify("MANUALLY OVERRIDING LOCKDOWN..."));
                audio.Play();
                interaction.enabled = false;
                hasOverridden = true;
            }
        }
    }
}
