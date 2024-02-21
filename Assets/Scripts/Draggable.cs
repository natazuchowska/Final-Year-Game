using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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



    private bool isDragged = false;
    private Vector3 mouseDragStartPoistion;
    private Vector3 spriteDragStartPosition;

    // when an object is clicked
    private void OnMouseDown()
    {
        isDragged = true;
        mouseDragStartPoistion = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        spriteDragStartPosition = transform.localPosition;
        if(this.gameObject.tag != "Key") // only snap back if not a key
        {
            whereBeforeDragCallback(this);
        }
    }

    // when the object is being dragged
    private void OnMouseDrag()
    {
        if(isDragged)
        {
            transform.localPosition = spriteDragStartPosition + (Camera.main.ScreenToWorldPoint(Input.mousePosition) - mouseDragStartPoistion);
            dragInProgressCallback(this);
        }
    }

    // when the object is released
    private void OnMouseUp()
    {
        isDragged = false;
        dragEndedCallback(this);
    }
}
