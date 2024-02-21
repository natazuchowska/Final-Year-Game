/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorChangeObject : MonoBehaviour
{
    GameObject cursorManager;
    CursorManager cmScript;
    string objectTag;
    int cursorCase = 3; // normal cursor

    // Start is called before the first frame update
    void Start()
    {
        cursorManager = GameObject.FindGameObjectWithTag("CursorManager"); // get reference to cursor manager obj
        cmScript = cursorManager.GetComponent<CursorManager>(); // get the cursor manager script

        objectTag = this.gameObject.tag; // store the tag of this gameobject

        // set the passed argument to a correct cursor case
        if(tag == "Plant")
        {
            cursorCase = 0;
        }
        if(tag == "Pickup" || tag == "Character" || tag == "Collectible" || tag == "Bottle")
        {
            cursorCase = 1;
        }
        else
        {
            cursorCase = 3;
        }
    }

    private void OnMouseEnter()
    {
        cmScript.ChangeCursor(cursorCase);
    }


    private void OnMouseExit()
    {
        cmScript.ChangeCursor(3);
    }
}
*/