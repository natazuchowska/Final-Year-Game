using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class SpeechBubbleManager : MonoBehaviour
{
    GameObject cursorManager;
    CursorManager cmScript;

    GameObject character;

    GameObject dialogueCircle;

    public bool inArea = false;

    private void Start()
    {
        cursorManager = GameObject.FindGameObjectWithTag("CursorManager"); // get reference to cursor manager obj
        cmScript = cursorManager.GetComponent<CursorManager>(); // get the cursor manager script

        character = GameObject.Find("Character");

        dialogueCircle = GameObject.Find("DialogueCircle");
    }

    private void Update()
    {
        character = GameObject.Find("Character");
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        // player in the right distance so change cursor to speech bubble
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("collision - display speech");

            inArea = true;

            if(character != null)
            {
                character.GetComponent<CursorChangeObject>().DecideCursor();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // player in the right distance so change cursor to speech bubble
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("end of collision - change to normal cursor");

            inArea = false;
            // cmScript.ChangeCursor(4);
            if(dialogueCircle != null && character != null)
            {
                // character.GetComponent<CursorChangeObject>().DecideCursor();
                cmScript.ChangeCursor(4);
            }
        }
    }

}
