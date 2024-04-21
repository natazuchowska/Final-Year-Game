/*using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;*/
using UnityEngine;
/*using UnityEngine.SceneManagement;*/

public class Draggable : MonoBehaviour
{
    public delegate void DragEndedDelegate(Draggable draggableObject); // delegate type - can hold reference to another method

    public DragEndedDelegate dragEndedCallback; // want to invoke the callback when the drag is over
    
    // ---------------------------------------------------------------------------------------------

    public delegate void DragInProgressDelegate(Draggable draggableObject); // delegate type - can hold reference to another method

    public DragInProgressDelegate dragInProgressCallback; // want o invoke the callback when the drag is in progress

    // ---------------------------------------------------------------------------------------------

    public delegate void BeforeDragDelegate(Draggable draggableObject); // delegate type - can hold reference to another method

    public BeforeDragDelegate whereBeforeDragCallback; // want o invoke the callback when the drag is in progress

    GameObject snapControlObj; // get reference to snap control object
    [SerializeField] SnapController snapControl; // to reference snap controller

    private bool isDragged = false;
    private Vector3 mouseDragStartPoistion;
    private Vector3 spriteDragStartPosition;

    // store ID of current scene
    int sceneID;

    private void Update()
    {
        snapControlObj = GameObject.Find("SnapPointsContainer"); // try find the snap points container object

        if (snapControlObj != null) // if object exists in the scene
        {
            snapControl = snapControlObj.GetComponent<SnapController>(); // ref the snap points script
        }
    }

    // when an object is clicked
    private void OnMouseDown()
    {
        if (PauseController.isPaused) { return; } // do not drag if game paused

        isDragged = true;

        sceneID = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().getSceneID(); // get id of current scene from gamemanager

        mouseDragStartPoistion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spriteDragStartPosition = transform.localPosition;
        if(this.gameObject.tag != "Key") // only snap back if not a key
        {
            // scene has no snap slots so return
            if (!(sceneID == 3 || sceneID == 10 || sceneID == 12 || sceneID == 13 || sceneID == 14 || sceneID == 15)) { return; }

            // whereBeforeDragCallback(this);
            snapControl.OnBeforeDrag(this);

        }
    }

    // when the object is being dragged
    private void OnMouseDrag()
    {
        if(isDragged)
        {
            transform.localPosition = spriteDragStartPosition + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseDragStartPoistion);
            
            // scene has no snap slots so return
            if (!(sceneID == 3 || sceneID == 10 || sceneID == 12 || sceneID == 13 || sceneID == 14 || sceneID == 15)) { return; }

            // dragInProgressCallback(this);
            snapControl.OnDragInProgress(this);
        }
    }

    // when the object is released
    private void OnMouseUp()
    {
        if (PauseController.isPaused) { return; } // do not drag if game paused

        isDragged = false;

        // scene has no snap slots so return
        if (!(sceneID == 3 || sceneID == 10 || sceneID == 12 || sceneID == 13 || sceneID == 14 || sceneID == 15)) { return; }

        // dragEndedCallback(this);
        snapControl.OnDragEnded(this);
    }
}
