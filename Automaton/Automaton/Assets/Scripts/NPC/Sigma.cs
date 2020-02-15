using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//The main class for managing Sigma as an object. Responsible for handling dialogue triggers and rotating towards the character once within range.

public class Sigma : MonoBehaviour
{
    private HeadsUpDisplay hudScript;
    private GameObject player;
    private Interactable interaction;
    private KeyManager keyManager;

    void Start()
    {
        keyManager = GameObject.FindObjectOfType<KeyManager>();
        hudScript = GameObject.FindObjectOfType<HeadsUpDisplay>();
        interaction = this.gameObject.GetComponent<Interactable>();
        player = GameObject.FindGameObjectWithTag("Player");

        interaction.setCommandText("Talk\n" + this.gameObject.name);
    }

    void Update()
    {
        interaction.setKey(keyManager.buttonCodes["Interact"]);

        if (interaction == hudScript.getCurrentInteractingObject())
        {
            rotateCharacter();

            if(Input.GetKeyDown(interaction.getKey()))
            {
                 triggerDialogue();
            }
        }
    }

    public void triggerDialogue()
    {
        FindObjectOfType<DialogueManager>().startConversation();
    }

    public void rotateCharacter()
    {
        float rotationTime = 10f;
        Vector3 lookPosition = player.transform.position - transform.position;
        lookPosition.y = 0;
        Quaternion rotation = Quaternion.LookRotation(lookPosition);
        transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationTime);
        rotationTime = rotationTime + Time.deltaTime;
    }
}
