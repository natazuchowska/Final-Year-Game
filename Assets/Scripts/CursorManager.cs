using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class CursorManager : MonoBehaviour
{
    public Texture2D currentCursor;

    private Camera mainCamera;

    [SerializeField] Texture2D normalCursor;
    [SerializeField] Texture2D normalCursorDark;
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
        else if (whichOne == 3)
        {
            currentCursor = talkCursor;
        }
        else if(whichOne == -1)
        {
            Debug.Log("Changing to DARK CURSOR");
            currentCursor = normalCursorDark;
        }
        else
        {
            // regular cursor called from OnMouseExit
            currentCursor = normalCursor;
        }

        Cursor.SetCursor(currentCursor, Vector2.zero, CursorMode.ForceSoftware);
    }

    private void Update()
    {
        if(mainCamera == null)
        {
            mainCamera = Camera.main;
        }

        if (SceneManager.GetActiveScene().buildIndex == 0 || SceneManager.GetActiveScene().buildIndex == 11)
        {
            var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));

            if (!rayHit.collider)
            {
                return;
            }

            if(GameObject.Find("dialogue_background") != null)
            {
                if (GameObject.Find("dialogue_background").activeSelf == true)
                {
                    if (rayHit.collider.gameObject.CompareTag("DialogueBackground"))
                    {
                        if(currentCursor == normalCursor)
                        {
                            ChangeCursor(-1);
                        }
                    }
                    else
                    {
                        ChangeCursor(4);
                    }
                }
            }
            
        }
    }

}