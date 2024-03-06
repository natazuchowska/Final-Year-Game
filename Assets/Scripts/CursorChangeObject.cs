using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System.Net.Security;

public class CursorChangeObject : MonoBehaviour
{
    GameObject cursorManager;
    CursorManager cmScript;
    string objectTag;
    int whichCursor; // normal cursor

    // [SerializeField] public GameObject textLabel;
    int sceneID;

    private void Awake()
    {
        if (sceneID == 1 || sceneID == 11) // the only scenes with characters
        {
            // talkLabel = GameObject.FindGameObjectWithTag("TalkLabel");
            // textLabel.SetActive(false);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        cursorManager = GameObject.FindGameObjectWithTag("CursorManager"); // get reference to cursor manager obj
        cmScript = cursorManager.GetComponent<CursorManager>(); // get the cursor manager script

        sceneID = SceneManager.GetActiveScene().buildIndex; // get the id of current scene    

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
            if(sceneID == 11 && objectTag.Equals("ClickTile"))
            {
                // if lamp light turned off
                if(GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().checkIfSolved(2) == true)
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
            Debug.Log("cursor change obj case puzzle");
            return 2;
        }
        else if (objectTag == "Character")
        {
            // textLabel.SetActive(true);
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
        /*if((sceneID == 1 || sceneID == 11) && textLabel.activeSelf == true)
        {
            textLabel.SetActive(false);
        }*/
        cmScript.ChangeCursor(4);
    }
}
