using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Represents an energy cube object. Holds information for what appears in the HUD when interacting with it.

public class EnergyCube : MonoBehaviour
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
                Debug.Log("Energy time!");
                pickUp.setPickedUp(true);
                pickUp.pickUp(this.gameObject);
            }
        }
    }
}
