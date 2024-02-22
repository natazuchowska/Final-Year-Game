using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class CursorManager : MonoBehaviour
{
    public Texture2D currentCursor;

    private Camera mainCamera;

    [SerializeField] Texture2D normalCursor;
    [SerializeField] Texture2D interactCursor; // for pickups and talking to characters
    [SerializeField] Texture2D puzzleCursor; // for zooming in on puzzles
    [SerializeField] Texture2D dragCursor; // for draggable items 
    [SerializeField] Texture2D talkCursor; // for talking with characters

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.ForceSoftware);
        currentCursor = normalCursor; // initialise current cursor to be the normal one
    }

    public void ChangeCursor(int whichOne)
    {
        if (whichOne == 0)
        {
            currentCursor = dragCursor;
        }
        else if (whichOne == 1)
        {
            currentCursor = interactCursor;
        }
        else if (whichOne == 2)
        {
            currentCursor = puzzleCursor;
        }
        else if(whichOne == 3)
        {
            currentCursor = talkCursor;
        }
        else
        {
            // regular cursor called from OnMouseExit
            currentCursor = normalCursor;
        }

        Cursor.SetCursor(currentCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}