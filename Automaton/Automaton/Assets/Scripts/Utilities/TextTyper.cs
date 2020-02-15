using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using System.Linq;
using UnityEngine;

//Responsible for breaking up a string passed in into a character array and printing them to the screen in increments
//Also calculates when to take new lines given a specified character limit

public class TextTyper : MonoBehaviour
{
    private List<char> totalChars;
    private List<string>finalCharList;
    public int maxCharsPerLine, currentCharInLine;
    private KeyManager keys;

    private void calculateSpacing(string writtenText)
    {
        for(int counter = 0; counter < writtenText.ToCharArray().Length; counter++)
        {
            totalChars.Add(writtenText.ToCharArray()[counter]);
        }

        //Calculating spacing
        for (int counter = 0; counter < totalChars.Count; counter++)
        {
            currentCharInLine++;

            if (currentCharInLine == maxCharsPerLine)
            {
                if(totalChars[counter].Equals(' '))
                {
                    totalChars.Insert(counter, '\n');
                }

                //Inserts hyphens in the middle of words if at the current line character limit
                else
                {
                    totalChars.Insert(counter,'-');
                    totalChars.Insert(counter + 1, '\n');
                }

                currentCharInLine = 0;
            }

            finalCharList.Add(totalChars[counter].ToString());
        }
    }

    public IEnumerator printText(GameObject textObj, string writtenText, float delay, int maxChars)
    {
        keys = GameObject.FindObjectOfType<KeyManager>();
        totalChars = new List<char>();
        finalCharList = new List<string>();
        currentCharInLine = 0;
        this.maxCharsPerLine = maxChars;

        textObj.GetComponent<Text>().text = "";
        calculateSpacing(writtenText);

        
            //Printing them to the screen
            foreach (string character in finalCharList)
            {
                textObj.GetComponent<Text>().text += character;
                yield return new WaitForSeconds(delay);
            }
        
    }
}
