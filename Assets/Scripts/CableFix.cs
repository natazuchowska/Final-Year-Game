using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CableFix : MonoBehaviour
{
    public List<Transform> snapPoints;
    public List<Draggable> draggableObjects;

    public static bool electricityFlow = false; //electricity turned off initially

    //instantiate slots to false
    bool slot1 = false;
    bool slot2 = false;
    bool slot3 = false;
    bool slot4 = false;

    public float snapRange = 0.5f;
    public AudioSource audioPlayer; // to play rewarding sound when puzzle solved


    // Start is called before the first frame update
    void Start()
    {
        foreach (Draggable draggable in draggableObjects)
        {
            draggable.dragEndedCallback = OnDragEnded; // delegate type storing reference to method, will invoke OnDragEnded in OnMouseUp
            draggable.dragInProgressCallback = OnDragInProgress; // when the mouse is holding the object
        }
    }

    private void OnDragEnded(Draggable draggable)
    {
        float closestDistance = -1;
        Transform closestSnapPoint = null;
        int snapIndex = -1;

        foreach (Transform snapPoint in snapPoints)
        {
            float currentDistance = Vector2.Distance(draggable.transform.localPosition, snapPoint.localPosition); // store the distance between the object and snappoint

            if (closestSnapPoint == null || currentDistance < closestDistance)
            {
                // if smaller - update the current minimum
                closestSnapPoint = snapPoint;
                closestDistance = currentDistance;
                snapIndex = snapPoints.IndexOf(snapPoint); // get the index of snappoint
            }
        }

        if (closestSnapPoint != null && closestDistance <= snapRange) // if item can be inserted into the slot
        {
            draggable.transform.localPosition = closestSnapPoint.localPosition; // put the object in the centre of the (closest) snappoint

            if (draggable.gameObject.CompareTag("CableFix")) // if the object inserted in the slot is a cable for fixing
            {
                if (snapIndex == 0)
                {
                    // check which bottle that is
                    if (draggable.gameObject.name == "cable1")
                    {
                        slot1 = true;
                        Debug.Log("c1 OK");
                    }
                    else
                    {
                        slot1 = false;
                    }
                }
                if (snapIndex == 1)
                {
                    // check which bottle that is
                    if (draggable.gameObject.name == "cable2")
                    {
                        slot2 = true;
                        Debug.Log("c2 OK");
                    }
                    else
                    {
                        slot2 = false;
                    }
                }
                if (snapIndex == 2)
                {
                    // check which bottle that is
                    if (draggable.gameObject.name == "cable3")
                    {
                        slot3 = true;
                        Debug.Log("c3 OK");
                    }
                    else
                    {
                        slot3 = false;
                    }
                }
                if (snapIndex == 3)
                {
                    // check which bottle that is
                    if (draggable.gameObject.name == "cable4")
                    {
                        slot4 = true;
                        Debug.Log("c4 OK");
                    }
                    else
                    {
                        slot4 = false;
                    }
                }

                // are all in correct order?
                if (slot1 == true && slot2 == true && slot3 == true && slot4 == true)
                {
                    Debug.Log("CORRECT CABLE FIX! CONGRATS!!!!");

                    electricityFlow = true; // electricity now on -> can open the box with cables on the other wall
                    audioPlayer.Play(); // play puzzle solved sound 
                }
            }
        }
    }

    private void OnDragInProgress(Draggable draggable)
    {
        float closestDistance = -1;
        Transform closestSnapPoint = null;

        foreach (Transform snapPoint in snapPoints)
        {
            float currentDistance = Vector2.Distance(draggable.transform.localPosition, snapPoint.localPosition); // store the distance between the object and snappoint

            if (closestSnapPoint == null || currentDistance < closestDistance)
            {
                // if smaller - update the current minimum
                closestSnapPoint = snapPoint;
                closestDistance = currentDistance;
            }
        }
    }
}
