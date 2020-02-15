using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Represents a bomb cube, holds the data for what appears in the HUD when interacted with

public class BombCube : MonoBehaviour
{
    private Interactable interaction;
    private KeyManager keyManager;
    private HeadsUpDisplay hudScript;
    private PickUpObject pickUp;
    private Rigidbody rigidBody;

    void Start()
    {
        keyManager = GameObject.FindObjectOfType<KeyManager>();
        hudScript = GameObject.FindObjectOfType<HeadsUpDisplay>();
        interaction = this.gameObject.GetComponent<Interactable>();
        rigidBody = this.gameObject.GetComponent<Rigidbody>();
        pickUp = GameObject.FindObjectOfType<PickUpObject>();

        interaction.setCommandText("Pick Up\n" + this.gameObject.name);
    }

    void Update()
    {
        interaction.setKey(keyManager.buttonCodes["Interact"]);

        if (interaction == hudScript.getCurrentInteractingObject())
        {
            if (Input.GetKeyDown(interaction.getKey()))
            {
                
                pickUp.setPickedUp(true);
                pickUp.pickUp(this.gameObject);
            }
        }
    }
}
