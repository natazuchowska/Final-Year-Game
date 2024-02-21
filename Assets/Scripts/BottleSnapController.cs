using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BottleSnapController : MonoBehaviour
{
    public List<Transform> snapPoints;
    public List<Draggable> draggableObjects;

    Vector3 initialPos;

    //instantiate slots to false
    bool slot1 = false;
    bool slot2 = false;
    bool slot3 = false;
    // public int howManyCorrect = 0; // how many bottles ok

    public float snapRange = 0.5f;

    GameObject bottleKeyReward; // reward for solving the puzzle

    // steam to be disabled when correct bottle inserted
    GameObject steamLeft;
    GameObject steamMid;
    GameObject steamRight;

    GameObject backgroundAfter; // to change the background when puzzle solved

    public AudioSource audioPlayer; // to play rewarding sound when puzzle solved

    // store ID of current scene
    int sceneID;


    // Start is called before the first frame update
    void Start()
    {
        foreach (Draggable draggable in draggableObjects)
        {
            draggable.dragEndedCallback = OnDragEnded; // delegate type storing reference to method, will invoke OnDragEnded in OnMouseUp
            draggable.dragInProgressCallback = OnDragInProgress; // when the mouse is holding the object
            draggable.whereBeforeDragCallback = OnBeforeDrag;
        }

        bottleKeyReward = GameObject.FindGameObjectWithTag("KeyReward"); // get the key object
        bottleKeyReward.SetActive(false); // deactivate key until puzzle not solved

        // find and store reference to steam sprites
        steamLeft = GameObject.FindGameObjectWithTag("SteamL");
        steamMid = GameObject.FindGameObjectWithTag("SteamM");
        steamRight = GameObject.FindGameObjectWithTag("SteamR");

        // set to be visible on start
        steamLeft.SetActive(true);
        steamMid.SetActive(true);
        steamRight.SetActive(true);

        backgroundAfter = GameObject.FindGameObjectWithTag("BackgroundAfter"); // open little door revealing the key
        backgroundAfter.SetActive(false);
    }

    private void OnBeforeDrag(Draggable draggable)
    {
        initialPos = draggable.transform.localPosition; // store the position before drag happens
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
               /* if(snapIndex == 0 && slot1 == false)
                {
                    steamLeft.SetActive(true);
                }
                if(snapIndex == 1 && slot2 == false)
                {
                    steamMid.SetActive(true);
                }
                if(snapIndex == 2 && slot3 == false)
                {
                    steamRight.SetActive(true);
                }*/
            }
        }

        if (closestSnapPoint != null && closestDistance <= snapRange /* && takenSnapPoints[snapIndex] == 0*/) // if item can be inserted into the slot
        {
            draggable.transform.localPosition = closestSnapPoint.localPosition; // put the object in the centre of the (closest) snappoint

            if (draggable.gameObject.CompareTag("SnapBottle")) // if the object inserted in the slot is a snapBottle
            {
                if (snapIndex == 0)
                {
                    // check which bottle that is
                    if (draggable.gameObject.name == "leftBottle")
                    {
                        slot1 = true;
                        Debug.Log("left OK");
                        steamLeft.SetActive(false);
                    }
                    else
                    {
                        slot1 = false;
                        steamLeft.SetActive(true);
                    }
                }
                if (snapIndex == 1)
                {
                    // check which bottle that is
                    if (draggable.gameObject.name == "middleBottle")
                    {
                        slot2 = true;
                        Debug.Log("middle OK");
                        steamMid.SetActive(false);
                    }
                    else
                    {
                        slot2 = false;
                        steamMid.SetActive(true);
                    }
                }
                if (snapIndex == 2)
                {
                    // check which bottle that is
                    if (draggable.gameObject.name == "rightBottle")
                    {
                        slot3 = true;
                        Debug.Log("right OK");
                        steamRight.SetActive(false);
                    }
                    else
                    {
                        slot3 = false;
                        steamRight.SetActive(true);
                    }
                }

                // are all in correct order?
                if (slot1 == true && slot2 == true && slot3 == true)
                {
                    Debug.Log("CORRECT ORDER! CONGRATS!!!!");

                    bottleKeyReward.SetActive(true);
                    backgroundAfter.SetActive(true);
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

        if (closestSnapPoint != null && closestDistance <= snapRange)
        {
            if (draggable.gameObject.CompareTag("Bottle")) // if the thing being dragged in the slot is a plant object
            {
                draggable.gameObject.GetComponent<Renderer>().material.color = Color.grey; // change object's clour to grey when put near the slot
            }
        }

        if (closestSnapPoint != null && closestDistance > snapRange) // outside of snap range so reset the colour
        {
            // takenSnapPoints[snapPoints.IndexOf(closestSnapPoint)] = 0;

            if (draggable.gameObject.CompareTag("Bottle")) // if the thing being dragged in the slot is a plant object
            {
                draggable.gameObject.GetComponent<Renderer>().material.color = Color.white; // reset to original colour when out of slot interaction range
            }
        }
    }
}
