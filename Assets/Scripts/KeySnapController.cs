using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class KeySnapController : MonoBehaviour
{
    public List<Transform> snapPoints;
    public List<Draggable> draggableObjects;

    [SerializeField] public static bool canGoThru;

    // nav buttons
    [SerializeField] public Button goBackButton;
    [SerializeField] public Button goThruDoorButton;

    //instantiate slots to false
    bool slot1 = false;
    bool slot2 = false;

    public float snapRange = 0.5f;

    public AudioSource audioPlayer; // to play door unlocking sound

    int sceneID; // to check which door it is


    // Start is called before the first frame update
    void Start()
    {
        foreach (Draggable draggable in draggableObjects)
        {
            draggable.dragEndedCallback = OnDragEnded; // delegate type storing reference to method, will invoke OnDragEnded in OnMouseUp
            draggable.dragInProgressCallback = OnDragInProgress; // when the mouse is holding the object
        }

        sceneID = SceneManager.GetActiveScene().buildIndex; // get the id of the scene
        canGoThru = false; // door locked by default

        goBackButton.interactable = true; // can go back
        goThruDoorButton.interactable = false; // can't go thru (door locked)

        Debug.Log(canGoThru);
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

        if (closestSnapPoint != null && closestDistance <= snapRange /* && takenSnapPoints[snapIndex] == 0*/) // if item can be inserted into the slot
        {
            draggable.transform.localPosition = closestSnapPoint.localPosition; // put the object in the centre of the (closest) snappoint

            if (draggable.gameObject.CompareTag("Key")) // if the object inserted in the slot is a snapBottle
            {
                if(sceneID == 15) // BASEMENT DOOR
                {
                    if (snapIndex == 0)
                    {
                        // check if correct key
                        if (draggable.gameObject.name == "keyTop")
                        {
                            slot1 = true;
                            Debug.Log("top key OK");
                            audioPlayer.Play(); // play door unlocking sound
                        }
                        else
                        {
                            slot1 = false;
                        }
                    }
                    if (snapIndex == 1)
                    {
                        // check if correct key
                        if (draggable.gameObject.name == "keyBottom")
                        {
                            slot2 = true;
                            Debug.Log("bottom key OK");
                            audioPlayer.Play(); // play door unlocking sound
                        }
                        else
                        {
                            slot2 = false;
                        }
                    }

                    // are all keys in place?
                    if (slot1 == true && slot2 == true)
                    {
                        Debug.Log("DOOR NOW OPEN");
                        canGoThru = true;
                    }
                }

                if(sceneID == 14) // GLASSHOUSE DOOR
                {
                    if (snapIndex == 0)
                    {
                        // check if correct key
                        if (draggable.gameObject.name == "glasshouseKey")
                        {
                            slot1 = true;
                            Debug.Log("key OK");
                            audioPlayer.Play(); // play door unlocking sound
                            canGoThru = true;
                        }
                        else
                        {
                            slot1 = false;
                        }
                    }
                }

                if(canGoThru == true)
                {
                    goBackButton.interactable = false; // can't go back to the room
                    goThruDoorButton.interactable = true; // can go to the next room

                    Debug.Log("can go thru now");
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

        /*if (closestSnapPoint != null && closestDistance <= snapRange)
        {
            if (draggable.gameObject.CompareTag("Plant") || draggable.gameObject.CompareTag("Bottle")) // if the thing being dragged in the slot is a plant object
            {
                draggable.gameObject.GetComponent<Renderer>().material.color = Color.grey; // change object's clour to grey when put near the slot
            }
        }

        if (closestSnapPoint != null && closestDistance > snapRange) // outside of snap range so reset the colour
        {
            // takenSnapPoints[snapPoints.IndexOf(closestSnapPoint)] = 0;

            if (draggable.gameObject.CompareTag("Plant") || draggable.gameObject.CompareTag("Bottle")) // if the thing being dragged in the slot is a plant object
            {
                draggable.gameObject.GetComponent<Renderer>().material.color = Color.white; // reset to original colour when out of slot interaction range
            }
        }*/
    }
}
