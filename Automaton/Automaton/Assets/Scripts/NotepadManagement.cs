using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

//Handles checks to see if notepads have been collected through each scene
//Stored as a series of static booleans because of Unity's garbage collector deleting all objects, stored values, and reinitialising arrays when transitioning to a new scene

public class NotepadManagement : MonoBehaviour
{
    public static bool notepad_1;
    public static bool notepad_2;
    public static bool notepad_3;
    public static bool notepad_4;
    public static bool notepad_5;

    public static void updateCollectedNotepads(int index)
    {
        if(index == 0)
        {
            notepad_1 = true;
        }

        else if(index == 1)
        {
            notepad_2 = true;
        }

        else if (index == 2)
        {
            notepad_3 = true;
        }

        else if (index == 3)
        {
            notepad_4 = true;
        }

        else if (index == 4)
        {
            notepad_5 = true;
        }
    }

    public static bool getCollectedIndexOf(int index)
    {
        bool check = false;

        if (index == 0)
        {
            check = notepad_1;
        }

        else if (index == 1)
        {
            check = notepad_2;
        }

        else if (index == 2)
        {
            check = notepad_3;
        }

        else if (index == 3)
        {
            check = notepad_4;
        }

        else if (index == 4)
        {
            check = notepad_5;
        }

        return check;
    }
}
