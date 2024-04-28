/*using System.Collections;
using System.Collections.Generic;*/
using UnityEngine;
using UnityEngine.SceneManagement;
/*using TMPro;
using System.Net.Security;*/

public class CursorChangeObject : MonoBehaviour
{
    GameObject cursorManager;
    CursorManager cmScript;
    string objectTag;
    int whichCursor; // normal cursor

    int sceneID;

    void Start()
    {
        cursorManager = GameObject.FindGameObjectWithTag("CursorManager"); // get reference to cursor manager obj
        cmScript = cursorManager.GetComponent<CursorManager>(); // get the cursor manager script

        sceneID = SceneManager.GetActiveScene().buildIndex; // get the id of current scene    

        objectTag = this.gameObject.tag; // store the tag of this gameobject
        whichCursor = 3;
    }

    public int DecideCursor()
    {
        // set the passed argument to a correct cursor case
        if (objectTag == "Plant" || objectTag == "Key" || objectTag == "SnapBottle" || objectTag == "CableFix" || objectTag == "NO")
        {
            return 0;
        }
        else if (objectTag == "Pickup" || objectTag == "Collectible" || objectTag == "Bottle" || objectTag == "KeyReward" || objectTag == "ClickTile")
        {
            if(sceneID == 11 && objectTag.Equals("ClickTile"))
            {
                // if lamp light turned off and key not yet collected
                if(GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().checkIfSolved(2) == true && !TileController.keyCollected)
                {
                    // only then change the cursor to handCursor -> otherwise don't show that tile is interractable
                    return 1;
                }
                else
                {
                    // otherwise return the normal cursor
                    return 4;
                }
            }

            return 1;
     
        }
        else if (objectTag == "Puzzle")
        {
            return 2;
        }
        else if (objectTag == "Character")
        {
            if(sceneID == 3 || sceneID == 11)
            {
                if(GameObject.Find("DialogueCircle") == null) { return 4; } // return normal cursor if no dialogue circle in scene (has been disabled)

                //Debug.Log("player in area?: " + GameObject.Find("DialogueCircle").GetComponent<SpeechBubbleManager>().inArea);

                if(GameObject.Find("DialogueCircle")==null || GameObject.Find("DialogueCircle").GetComponent<SpeechBubbleManager>() == false) { return 4; }

                if (GameObject.Find("DialogueCircle").GetComponent<SpeechBubbleManager>().inArea == true)
                {
                    if(sceneID == 11)
                    {
                        if(GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().checkIfSolved(2) == true) // lamp turned off -> disable fish dialogue
                        {
                            return 4;
                        }
                    }
                    return 3;
                }

                if (GameObject.Find("DialogueCircle").GetComponent<SpeechBubbleManager>().inArea == false)
                {
                    return 4;
                }
            }
            
            return 3;
        }
        else
        {
            return 4;
        }
    }

    private void OnMouseEnter()
    {
        if(PauseController.isPaused) { return;  }

        whichCursor = DecideCursor();
        cmScript.ChangeCursor(whichCursor);
    }

    private void OnMouseDown()
    {
        if (PauseController.isPaused) { return; }

        // on click change to normal cursor
        cmScript.ChangeCursor(4);
    }

    private void OnMouseExit()
    {
        if (PauseController.isPaused) { return; }

        cmScript.ChangeCursor(4);
    }
}
