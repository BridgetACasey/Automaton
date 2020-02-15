using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

//Handles and checks whether the level has been complete
//Checks how many pedestals are in the scene and how many have energy cubes placed on them
//Also handles restart function for the level

public class LevelManager : MonoBehaviour
{
    private KeyManager keyManager;
    private bool canOpenDoor;
    private ControlPanel panel;
    public GameObject[] pedestals;
    public GameObject controlPanel;
    public int totalCubesPlaced;

    void OnEnable()
    {
        totalCubesPlaced = 0;
        pedestals = GameObject.FindGameObjectsWithTag("Pedestal");
        keyManager = GameObject.FindObjectOfType<KeyManager>();
    }

    void Update()
    {
        if(keyManager.checkButtonCode("Restart"))
        {
            restartLevel();
        }

        if (SceneManager.GetActiveScene().name != "Tutorial" && SceneManager.GetActiveScene().name != "End Scene")
        {
            foreach(GameObject pedestal in pedestals)
            {
                if(pedestal.GetComponent<Pedestal>().isActiveAndEnabled)
                {
                    if(pedestal.GetComponent<Pedestal>().getCubePlaced())
                    {
                        totalCubesPlaced++;
                        pedestal.GetComponent<Pedestal>().enabled = false;

                        if (totalCubesPlaced == pedestals.Length)
                        {
                            canOpenDoor = true;
                            return;
                        }
                    }
                }
            }
        }

        else if (SceneManager.GetActiveScene().name == "End Scene")
        {
            panel = GameObject.FindObjectOfType<ControlPanel>();

            if (panel.hasOverridden)
            {
                canOpenDoor = true;
                return;
            }
        }

        else if (SceneManager.GetActiveScene().name == "Tutorial")
        {
            canOpenDoor = true;
            return;
        }
    }

    public void restartLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public bool getCanOpenDoor()
    {
        return canOpenDoor;
    }
}
