using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChangeObject : MonoBehaviour
{
    GameObject cursorManager;
    CursorManager cmScript;
    string objectTag;
    int whichCursor; // normal cursor


    // Start is called before the first frame update
    void Start()
    {
        cursorManager = GameObject.FindGameObjectWithTag("CursorManager"); // get reference to cursor manager obj
        cmScript = cursorManager.GetComponent<CursorManager>(); // get the cursor manager script

        objectTag = this.gameObject.tag; // store the tag of this gameobject
        whichCursor = 3;
    }

    private void Update()
    {
        // objectTag = this.gameObject.tag; // store the tag of this gameobject
        // whichCursor = DecideCursor();
    }

    int DecideCursor()
    {
        Debug.Log("object tag: " + objectTag);

        // set the passed argument to a correct cursor case
        if (objectTag == "Plant" || objectTag == "Key" || objectTag == "SnapBottle")
        {
            return 0;
        }
        else if (objectTag == "Pickup" || objectTag == "Collectible" || objectTag == "Bottle" || objectTag == "KeyReward" || objectTag == "ClickTile")
        {
            /*if(objectTag == "KeyReward")
            {
                if(GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().lightOn == false)
                {
                    return 1; // only show interactable cursor if lamp light has been switched off
                }
                else
                {
                    return 4;
                }
            }
            */
            
            return 1;
     
        }
        else if (objectTag == "Puzzle")
        {
            Debug.Log("cursor change obj case puzzle");
            return 2;
        }
        else if (objectTag == "Character")
        {
            return 3;
        }
        else
        {
            return 4;
        }
    }

    private void OnMouseEnter()
    {
        whichCursor = DecideCursor();
        cmScript.ChangeCursor(whichCursor);
        // cmScript.ChangeCursor(2);
    }

    private void OnMouseDown()
    {
        // on click change to normal cursor
        cmScript.ChangeCursor(4);
    }

    private void OnMouseExit()
    {
        cmScript.ChangeCursor(4);
    }
}
