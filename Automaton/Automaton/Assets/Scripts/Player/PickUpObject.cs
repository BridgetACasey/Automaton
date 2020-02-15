using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Handles how the player picks up and drops objects.
//If the player is holding and object, pressing E will drop it, otherwise pressing E over an object will pick it up and make it a child of the holdPoint
//If it is an energy cube, it will fall downwards. If it is a bomb cube, it will be thrown.

public class PickUpObject : MonoBehaviour
{
    private EnergyCube energyCube;
    private BombCube bombCube;
    private GameObject currentHeldObject;
    private HeadsUpDisplay hudScript;
    private bool pickedUp;
        
    public bool isThrowing;
    public GameObject holdPoint;

    void Start()
    {
        energyCube = GameObject.FindObjectOfType<EnergyCube>();
        bombCube = GameObject.FindObjectOfType<BombCube>();
        hudScript = GameObject.FindObjectOfType<HeadsUpDisplay>();
        currentHeldObject = null;
        pickedUp = false;
    }

    void Update()
    {
        if(currentHeldObject != null)
        {
            if (pickedUp)
            {
                currentHeldObject.transform.position = holdPoint.transform.position;
                currentHeldObject.transform.rotation = holdPoint.transform.rotation;
                StartCoroutine(drop());
            }

            if (currentHeldObject.GetComponent<Rigidbody>().velocity != Vector3.zero)
                isThrowing = true;

            else
                isThrowing = false;
        }
    }

    public void pickUp(GameObject obj)
    {
        if (currentHeldObject != null)
        {
            currentHeldObject.transform.parent = null;
            currentHeldObject.GetComponent<Rigidbody>().useGravity = true;

            if (currentHeldObject.GetComponent<BombCube>())
            {
                currentHeldObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 700f);
            }
        }

        currentHeldObject = obj;
        currentHeldObject.GetComponent<Rigidbody>().useGravity = false;
        currentHeldObject.transform.parent = holdPoint.transform;
        pickedUp = true;
    }

    private IEnumerator drop()
    {
        yield return new WaitForSeconds(0.1f);

        if (currentHeldObject != null)
        {
            if (Input.GetKeyDown(currentHeldObject.GetComponent<Interactable>().getKey()))
            {
                if (hudScript.getCurrentInteractingObject() != null && hudScript.getCurrentInteractingObject().GetComponent<Pedestal>())
                {
                    StopCoroutine(drop());
                }

                else
                {
                    pickedUp = false;
                    currentHeldObject.transform.parent = null;
                    currentHeldObject.GetComponent<Rigidbody>().useGravity = true;

                    if (currentHeldObject.GetComponent<BombCube>())
                    {
                        currentHeldObject.GetComponent<Rigidbody>().AddForce(Camera.main.transform.forward * 700f);

                        if (!currentHeldObject.GetComponent<Rigidbody>().IsSleeping())
                            isThrowing = true;

                        else
                            isThrowing = false;
                    }

                    currentHeldObject = null;
                }
            }
        }
    }

    public void placeObject(Pedestal pedestal, GameObject placePoint)
    {
        if(this.currentHeldObject != null)
        {
            pickedUp = false;
            currentHeldObject.transform.parent = placePoint.transform;
            currentHeldObject.transform.position = placePoint.transform.position;
            currentHeldObject.transform.rotation = placePoint.transform.rotation;
            currentHeldObject.GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeAll;
            currentHeldObject.GetComponent<Interactable>().enabled = false;
            pedestal.GetComponent<Interactable>().enabled = false;
            this.currentHeldObject = null;
        }
    }

    public void setPickedUp(bool picked)
    {
        this.pickedUp = picked;
    }

    public bool getPickedUp()
    {
        return pickedUp;
    }

    public void setCurrentHeldObject(GameObject obj)
    {
        this.currentHeldObject = obj;
    }

    public GameObject getCurrentHeldObject()
    {
        return currentHeldObject;
    }
}
