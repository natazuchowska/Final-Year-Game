using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ElectricitySnapController : MonoBehaviour
{
    public List<Transform> snapPoints;
    public List<Draggable> draggableObjects;

    [SerializeField] public static bool lightOn = true; // lamp on and needs to be switched off (if initialized here, when set to false would the false value persist when script is executed again?)

    //instantiate slots to false
    bool slot1 = false;

    public float snapRange = 0.5f;

    public AudioSource audioPlayer; // to play door unlocking sound


    // Start is called before the first frame update
    void Start()
    {
        foreach (Draggable draggable in draggableObjects)
        {
            draggable.dragEndedCallback = OnDragEnded; // delegate type storing reference to method, will invoke OnDragEnded in OnMouseUp
            draggable.dragInProgressCallback = OnDragInProgress; // when the mouse is holding the object
        }

        Debug.Log("lightOn val in electricity box: " + lightOn);
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

            if (snapIndex == 0)
            {
                // check if correct key
                if (draggable.gameObject.name == "sardynka")
                {
                    /*slot1 = true;*/
                    Debug.Log("object inserted OK");
                    audioPlayer.Play(); // play door unlocking sound

                    lightOn = false; // light now turned off
                    Debug.Log("lightOn val in electricity box: " + lightOn);
                }
                else
                {
                    /*slot1 = false;*/
                    Debug.Log("wrong object");
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
