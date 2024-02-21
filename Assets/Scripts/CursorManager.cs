/*using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CursorManager : MonoBehaviour
{
    public Texture2D currentCursor;

    public Texture2D normalCursor;
    public Texture2D interactCursor; // for pickups and talking to characters
    public Texture2D puzzleCursor; // for zooming in on puzzles
    public Texture2D dragCursor; // for draggable items 

    // Start is called before the first frame update
    void Start()
    {
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    public void ChangeCursor(int whichOne)
    {
        if(whichOne == 0)
        {
            currentCursor = dragCursor;
        }
        else if(whichOne == 1)
        {
            currentCursor = interactCursor;
        }
        else if(whichOne == 2)
        {
            currentCursor = puzzleCursor;
        }
        else 
        {
            // regular cursor called from OnMouseExit
            currentCursor = normalCursor;
        }

        
    }

    private void Update()
    {
        Cursor.SetCursor(currentCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void OnMouseDown()
    {
        Cursor.SetCursor(dragCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void OnMouseUp()
    {
        Cursor.SetCursor(normalCursor, Vector2.zero, CursorMode.ForceSoftware);
    }
}
*/