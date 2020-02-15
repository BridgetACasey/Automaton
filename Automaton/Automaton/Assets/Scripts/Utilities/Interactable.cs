using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Represents the data for one interactable object, to be attached to an object in the scene
//Includes key and command text for the HUD, and the distance this appears at

public class Interactable : MonoBehaviour
{
    [Header("Interactive")]
    public KeyCode key;
    public string commandText;
    public float distance;

    void Start()
    {

    }

    void Update()
    {

    }

    public void setDistance(float distance)
    {
        this.distance = distance;
    }

    public float getDistance()
    {
        return distance;
    }

    public void setKey(KeyCode key)
    {
        this.key = key;
    }

    public void setCommandText(string commandText)
    {
        this.commandText = commandText;
    }

    public KeyCode getKey()
    {
        return key;
    }

    public string getKeyText()
    {
        return key.ToString();
    }

    public string getCommandText()
    {
        return commandText;
    }
}
