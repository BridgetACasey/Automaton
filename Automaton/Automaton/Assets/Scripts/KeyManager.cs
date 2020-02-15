using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine.UI;
using UnityEngine;

//This class is responsible for managing custom key bindings set by the player in-game

public class KeyManager : MonoBehaviour
{
    #region Variables

    private Button currentActiveButton;
    public Dictionary<string, KeyCode> buttonCodes;

    #endregion Variables

    //Sets up the default key bindings when the game first launches
    void Start()
    {
        currentActiveButton = null;
        buttonCodes = new Dictionary<string, KeyCode>();

        buttonCodes["Move Forward 1"] = KeyCode.W;
        buttonCodes["Move Forward 2"] = KeyCode.UpArrow;
        buttonCodes["Move Left 1"] = KeyCode.A;
        buttonCodes["Move Left 2"] = KeyCode.LeftArrow;
        buttonCodes["Move Right 1"] = KeyCode.D;
        buttonCodes["Move Right 2"] = KeyCode.RightArrow;
        buttonCodes["Move Backward 1"] = KeyCode.S;
        buttonCodes["Move Backward 2"] = KeyCode.DownArrow;
        buttonCodes["Jump"] = KeyCode.Space;
        buttonCodes["Inventory"] = KeyCode.I;
        buttonCodes["Interact"] = KeyCode.E;
        buttonCodes["Grapple"] = KeyCode.Mouse0;
        buttonCodes["Restart"] = KeyCode.R;
        buttonCodes["Menu/Go Back"] = KeyCode.Tab;
    }

    private void Update()
    {
        
    }

    public void setKeyBinding(string keyName, KeyCode buttonCode)
    {
        //If the key is already bound, unbind it and reassign to the new key
        foreach(string buttonName in getAllButtonNames())
        {
            if(buttonCodes[buttonName] == buttonCode)
            {
                buttonCodes[buttonName] = KeyCode.None;
                break;
            }
        }

        buttonCodes[keyName] = buttonCode;
    }

    public string getKeyButtonName(string buttonName)
    {
        if(!buttonCodes.ContainsKey(buttonName))
        {
            Debug.LogError("ERROR! No such key found: " + buttonName);
            return "None";
        }

        else
        {
            return buttonCodes[buttonName].ToString();
        }
    }

    public string[] getAllButtonNames()
    {
        return buttonCodes.Keys.ToArray();
    }

    public bool checkButtonCode(string buttonName)
    {
        if (!buttonCodes.ContainsKey(buttonName))
        {
            Debug.LogError("ERROR! No such key found: " + buttonName);
            return false;
        }

        else
        {
            return Input.GetKeyDown(buttonCodes[buttonName]);
        }
    }

    public void setCurrentActiveButton(Button button)
    {
        this.currentActiveButton = button;
    }

    public Button getCurrentActiveButton()
    {
        return currentActiveButton;
    }

    public InputData setInputData()
    {
        InputData data = new InputData();

        data.buttonCodes = buttonCodes;

        return data;
    }
}

#region Serialization

[System.Serializable]
public struct InputData
{
    public Dictionary<string, KeyCode> buttonCodes;
}

#endregion Serialization
