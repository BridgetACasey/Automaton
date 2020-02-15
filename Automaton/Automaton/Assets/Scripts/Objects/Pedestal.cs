using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Represents a pedestal
//Checks what cube the player is holding once interacted with and if it can be placed on the pedestal

public class Pedestal : MonoBehaviour
{
    private Interactable interaction;
    private KeyManager keyManager;
    private HeadsUpDisplay hudScript;
    private PickUpObject pickUp;
    public bool cubePlaced;

    public GameObject placePoint;

    void Start()
    {
        keyManager = GameObject.FindObjectOfType<KeyManager>();
        hudScript = GameObject.FindObjectOfType<HeadsUpDisplay>();
        pickUp = GameObject.FindObjectOfType<PickUpObject>();
        interaction = this.gameObject.GetComponent<Interactable>();

        cubePlaced = false;
        interaction.setCommandText("Place Cube\n" + this.gameObject.name);
    }

    void Update()
    {
        interaction.setKey(keyManager.buttonCodes["Interact"]);

        if (interaction == hudScript.getCurrentInteractingObject())
        {
            if (Input.GetKeyDown(interaction.getKey()))
            {
                Debug.Log("Checking cube...");
                placeCube();
            }
        }
    }

    private void placeCube()
    {
        if(pickUp.getCurrentHeldObject() != null)
        {
            if(pickUp.getCurrentHeldObject().GetComponent<EnergyCube>())
            {
                Debug.Log("Placing Cube");
                pickUp.placeObject(this.gameObject.GetComponent<Pedestal>(), placePoint);
                this.cubePlaced = true;
            }

            else
            {
                //Debug.Log("Cannot place that cube here");
                StartCoroutine(hudScript.notify("Cannot place that cube here"));
            }
        }

        else
        {
            //Debug.Log("No cube to place");
            StartCoroutine(hudScript.notify("No cube to place"));
        }
    }

    public bool getCubePlaced()
    {
        return cubePlaced;
    }
}
