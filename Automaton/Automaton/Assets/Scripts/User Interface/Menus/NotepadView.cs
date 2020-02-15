using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

//Stores and displays the text for each notepad
//Sets the notepad view active accordingly

public class NotepadView : MonoBehaviour
{
    private KeyManager keys;
    private Codex codex;

    public Button backButton;
    public GameObject padText;
    public GameObject codexObj;
    public GameObject viewPage;
    public Dictionary<int, string> notepadText;

    void Start()
    {
        keys = GameObject.FindObjectOfType<KeyManager>();
        codex = GameObject.FindObjectOfType<Codex>();
        notepadText = new Dictionary<int, string>();

        notepadText[0] = "Lucas,\n\nAnother failed attempt. The last Delta bot we tried to send back seems to have vanished completely, so we tried again with a newer model - 003A. He was insubordinate at first, so we wiped his memory core and reinstalled it. Appears more docile now. Will update you as things progress.\n\nDr S. Giles, Head of Engineering";
        notepadText[1] = "Sophia,\n\nI'm starting to worry about my Sigma bot. He seems to have developed a curiosity about our work, the Delta line specifically. He keeps asking if he can meet one, claims he really wants a 'cool friend.' Someone who'll never leave him. It was endearing at first, but now its become a nuisance. I gave him a few whacks over the head, but he doesn't seem to have gotten the message.\n\nDr L. Malik, Head of Biomedicine";
        notepadText[2] = "Lucas,\n\nWe're really close now. We tried again with DELTA - 003A, and this time he actually came back. We only managed to manipulate a fraction of spacetime enough to send him a few seconds into the past, but it's something. Soon, we'll finally rid ourselves of this sickness. And don't worry too much about Sigma, he's always been a bit odd. I'm sure he's just got a few extra lines of code that shouldn't be there. P.S. Do you want your bangers and mash tonight with or without gravy?\n\nDr S. Giles, Head of Engineering";
        notepadText[3] = "Sophia,\n\nMy concern over Sigma is only growing. He keeps asking more questions about the experiment and the Delta bots. I've only told him what everyone else knows, that we're trying to get those notes back. Still, his behaviour grows more erratic. I caught him trying to follow me into 003A's chamber last week, and this morning he poured a bucket of glue over one of the Epsilon bot's legs after a heated argument. Something about 'keeping him in place.' I don't know what to do.\n\nDr L. Malik, Head of Biomedicine";
        notepadText[4] = "Lucas,\n\nThis is it. Gather your things and meet me in the test chamber. Everyone's going to be there, you know the drill. If something happens, if something comes back with Delta that we don't want, then initiate a total lockdown. We've loaded up the remaining bots in the emergency escape bay, we head straight there at the first sign of trouble. Leave Sigma, he's clearly defective.If we can't save him, then leave Delta too. We can always rebuild and replace them, make them better. I can't replace you.Please, be careful.\n\nYours, Sophia";

        backButton.onClick.AddListener(goBack);
    }

    void Update()
    {
        if(viewPage.activeSelf)
        {
            codex.enabled = false;

            if(keys.checkButtonCode("Inventory"))
            {
                goBack();
            }
        }
    }

    public void loadTextForNotepad(int index)
    {
        Debug.Log("index: " + index);
        StopAllCoroutines();
        StartCoroutine(GetComponent<TextTyper>().printText(padText, notepadText[index], 0f, 48));
    }

    public void goBack()
    {
        codex.enabled = true;
        viewPage.SetActive(false);
        codexObj.SetActive(true);
    }
}
