using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnapController : MonoBehaviour
{
    public List<Transform> snapPoints;
    public List<Draggable> draggableObjects;
    // int[] takenSnapPoints; // to store which slots aready occupied
    public bool correctOrder = false; // check if bottles in correct slots
    public int howManyCorrect = 0; // how many bottles ok

    public float snapRange = 0.5f;


    // Start is called before the first frame update
    void Start()
    {
        foreach (Draggable draggable in draggableObjects)
        {
            draggable.dragEndedCallback = OnDragEnded; // delegate type storing reference to method, will invoke OnDragEnded in OnMouseUp
            draggable.dragInProgressCallback = OnDragInProgress; // when the mouse is holding the object
        }

        // takenSnapPoints = new int[snapPoints.Count]; // create an int array to store which slots already have an item in them
                                                     // - if a slot is taken no item should be able to snap to it

        /*for (int i = 0; i < takenSnapPoints.Length; i++) //iterate through taken array
        {
            takenSnapPoints[i] = 0; // initialize with 0 => indicating free slot
        }*/
    }

    private void OnDragEnded(Draggable draggable)
    {
        float closestDistance = -1;
        Transform closestSnapPoint = null;
        int snapIndex = -1;

        foreach(Transform snapPoint in snapPoints)
        {
            float currentDistance = Vector2.Distance(draggable.transform.localPosition, snapPoint.localPosition); // store the distance between the object and snappoint

            if (closestSnapPoint == null || currentDistance < closestDistance)
            {
                // if smaller - update the current minimum
                closestSnapPoint = snapPoint;
                closestDistance = currentDistance;
                snapIndex = snapPoints.IndexOf(closestSnapPoint); // get the index of snappoint
            }
        }

        if(closestSnapPoint != null && closestDistance <= snapRange /* && takenSnapPoints[snapIndex] == 0*/) // if item can be inserted into the slot
        {
            draggable.transform.localPosition = closestSnapPoint.localPosition; // put the object in the centre of the (closest) snappoint
            // takenSnapPoints[snapIndex] = 1; // slot taken now

            if (draggable.gameObject.CompareTag("Plant"))
            {
                Debug.Log("Character is saying: Thank you for the plant girlie");
            }

            if(draggable.gameObject.CompareTag("SnapBottle")) // if the object inserted in the slot is a snapBottle
            {
                // check which bottle that is
                if (draggable.gameObject.name == "leftBottle") 
                {
                    if(snapIndex == 0)
                    {
                        // correct place
                        // Debug.Log("bottleL CORRECT"); 
                        howManyCorrect++;
                        Debug.Log(howManyCorrect);
                    }
                    else
                    {
                        // wrong place
                        if(howManyCorrect>0)
                        {
                            howManyCorrect--;
                        }
                    }
                } 
                if (draggable.gameObject.name == "middleBottle") 
                {
                    if(snapIndex == 1)
                    {
                        // correct place
                        // Debug.Log("bottleM CORRECT");
                        howManyCorrect++;
                        Debug.Log(howManyCorrect);
                    }
                    else
                    {
                        // wrong place
                        if (howManyCorrect > 0)
                        {
                            howManyCorrect--;
                        }
                    }
                }
                if (draggable.gameObject.name == "rightBottle") 
                {
                    if(snapIndex == 2)
                    {
                        // correct place
                        // Debug.Log("bottleR CORRECT");
                        howManyCorrect++;
                        Debug.Log(howManyCorrect);
                    }
                    else
                    {
                        // wrong place
                        if (howManyCorrect > 0)
                        {
                            howManyCorrect--;
                        }
                    }
                }

                // are all in correct order?
                if(howManyCorrect == 3)
                {
                    correctOrder = true;
                    Debug.Log("CORRECT ORDER! CONGRATS!!!!");
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
        }
    }
}
