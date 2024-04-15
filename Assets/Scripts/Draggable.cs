/*using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;*/
using UnityEngine;
/*using UnityEngine.SceneManagement;*/

public class Draggable : MonoBehaviour
{


    // ERROR OF NULLREFERENCE -> DELEGATES SHOULD BE REASSIGNED TO A NEW SNAPCONTROLLER WITH EVERY SCENE CHANGE
    public delegate void DragEndedDelegate(Draggable draggableObject); // delegate type - can hold reference to another method

    public DragEndedDelegate dragEndedCallback; // want to invoke the callback when the drag is over
    
    // ---------------------------------------------------------------------------------------------

    public delegate void DragInProgressDelegate(Draggable draggableObject); // delegate type - can hold reference to another method

    public DragInProgressDelegate dragInProgressCallback; // want o invoke the callback when the drag is in progress

    // ---------------------------------------------------------------------------------------------

    public delegate void BeforeDragDelegate(Draggable draggableObject); // delegate type - can hold reference to another method

    public BeforeDragDelegate whereBeforeDragCallback; // want o invoke the callback when the drag is in progress

    /*    // === FOR KEY SNAP CONTROLLER: ================================================================

        public delegate void KeyDragEndedDelegate(Draggable draggableObject); 

        public KeyDragEndedDelegate keyDragEndedCallback; 

        // ---------------------------------------------------------------------------------------------

        public delegate void KeyDragInProgressDelegate(Draggable draggableObject);

        public KeyDragInProgressDelegate keyDragInProgressCallback;

        // ---------------------------------------------------------------------------------------------

        public delegate void KeyBeforeDragDelegate(Draggable draggableObject);

        public KeyBeforeDragDelegate keyWhereBeforeDragCallback;*/


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
        isDragged = true;

        sceneID = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().getSceneID(); // get id of current scene from gamemanager

        mouseDragStartPoistion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spriteDragStartPosition = transform.localPosition;
        if(this.gameObject.tag != "Key") // only snap back if not a key
        {
            // scene has no snap slots so return
            if (!(sceneID == 0 || sceneID == 10 || sceneID == 12 || sceneID == 13 || sceneID == 14 || sceneID == 15)) { return; }

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
            if (!(sceneID == 0 || sceneID == 10 || sceneID == 12 || sceneID == 13 || sceneID == 14 || sceneID == 15)) { return; }

            // dragInProgressCallback(this);
            snapControl.OnDragInProgress(this);
        }
    }

    // when the object is released
    private void OnMouseUp()
    {
        isDragged = false;

        // scene has no snap slots so return
        if (!(sceneID == 0 || sceneID == 10 || sceneID == 12 || sceneID == 13 || sceneID == 14 || sceneID == 15)) { return; }

        // dragEndedCallback(this);
        snapControl.OnDragEnded(this);
    }
}
