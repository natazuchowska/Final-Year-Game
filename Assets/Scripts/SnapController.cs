using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SnapController : MonoBehaviour
{
    public List<Transform> snapPoints;
    public List<Draggable> draggableObjects;

    public float snapRange = 0.5f;
    Vector3 initialPos;


    int sceneID; // to check which scene it is

    // public AudioSource audioPlayer; // to play rewarding sound when puzzle solved

    [SerializeField] public static bool canGoThru1; // glasshouse door
    [SerializeField] public static bool canGoThru2; // basement door


    // nav buttons
    [SerializeField] public Button goBackButton;
    [SerializeField] public Button goThruDoorButton;

    //instantiate slots to false
    bool keySlot1 = false;
    bool keySlot2 = false;
    bool keySlot3 = false;

    public AudioSource audioPlayer; // to play door unlocking sound

    [SerializeField] GameObject keyInserted; // sprite of the key after insertion to lock -> 1st door
    [SerializeField] GameObject keyInserted1; // sprite of the key after insertion to lock -> 2nd door top lock
    [SerializeField] GameObject keyInserted2; // sprite of the key after insertion to lock -> 2nd door bottom lock
    public float maxRotationTimer = 0.5f;

    // scenes with snap slots
    // ids: 0 10 12 13 14 15

    void Awake()
    {
        foreach (Draggable draggable in draggableObjects)
        {
            draggable.dragEndedCallback = OnDragEnded; // delegate type storing reference to method, will invoke OnDragEnded in OnMouseUp
            draggable.dragInProgressCallback = OnDragInProgress; // when the mouse is holding the object
            draggable.whereBeforeDragCallback = OnBeforeDrag;
        }

        sceneID = SceneManager.GetActiveScene().buildIndex; // get the id of the scene
        /*canGoThru1 = false; // door locked by default
        canGoThru2 = false; // door locked by default*/

        if(sceneID == 14 || sceneID == 15)
        {
            goBackButton.interactable = true; // can go back
            goThruDoorButton.interactable = false; // can't go thru (door locked)

            Debug.Log(canGoThru1);
            Debug.Log(canGoThru2);
        }


        if (sceneID == 14) // 1st door
        {
            keyInserted = GameObject.FindGameObjectWithTag("KeyInserted"); // get reference to key sprite after inserting it to lock
            keyInserted.SetActive(false); // hide as long as key not inserted correctly
        }
        if (sceneID == 15) // 2nd door
        {
            // find keys by object names
            keyInserted1 = GameObject.Find("keyInserted1");
            keyInserted2 = GameObject.Find("keyInserted2");

            keyInserted1.SetActive(false);
            keyInserted2.SetActive(false);
        }

    }

    // wken key is dropped out of inventory
    private void Update()
    {
        foreach (Draggable draggable in draggableObjects)
        {
            draggable.dragEndedCallback = OnDragEnded; // delegate type storing reference to method, will invoke OnDragEnded in OnMouseUp
            draggable.dragInProgressCallback = OnDragInProgress; // when the mouse is holding the object
            draggable.whereBeforeDragCallback = OnBeforeDrag;
        }
    }

    // Start is called before the first frame update
    /*    void Start()
        {
            foreach (Draggable draggable in draggableObjects)
            {
                draggable.dragEndedCallback = OnDragEnded; // delegate type storing reference to method, will invoke OnDragEnded in OnMouseUp
                draggable.dragInProgressCallback = OnDragInProgress; // when the mouse is holding the object
                draggable.whereBeforeDragCallback = OnBeforeDrag;
            }
        }*/

    public void OnBeforeDrag(Draggable draggable) // was private before with delegates
    {
        initialPos = draggable.transform.localPosition; // store the position before drag happens

    }

    public void OnDragEnded(Draggable draggable)
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
                snapIndex = snapPoints.IndexOf(snapPoint); // get the index of snappoint
            }
        }

        if(closestSnapPoint != null && closestDistance <= snapRange /* && takenSnapPoints[snapIndex] == 0*/) // if item can be inserted into the slot
        {
            draggable.transform.localPosition = closestSnapPoint.localPosition; // put the object in the centre of the (closest) snappoint

            // MAIN SCENE ===========================================================================
            if (draggable.gameObject.CompareTag("Plant"))
            {
                Debug.Log("Character is saying: Thank you for the plant girlie");
                GameObject plant = draggable.gameObject;
                plant.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                plant.transform.localPosition = new Vector3(plant.transform.localPosition.x, plant.transform.localPosition.y, -3); // move the plant to the front
                plant.GetComponent<Renderer>().material.color = Color.white;

                GameManager.plantGiven = true; // inform game mngr that plant was received
            }

            // DOOR SCENES ===========================================================================
            if (draggable.gameObject.CompareTag("Key")) // if the object inserted in the slot is a key
            {
                if (sceneID == 15) // BASEMENT DOOR
                {
                    if (snapIndex == 0) // top lock
                    {
                        // check if correct key
                        if (draggable.gameObject.name == "keyTop")
                        {
                            keySlot2 = true;
                            Debug.Log("top key OK");

                            draggable.gameObject.transform.Translate(draggable.gameObject.transform.position.x, draggable.gameObject.transform.position.y, 10); // hide key sprite (push to the back)
                            keyInserted1.SetActive(true); // show the inserted key sprite

                            StartCoroutine(RotateKey(2));

                            audioPlayer.Play(); // play door unlocking sound
                        }
                        else
                        {
                            keySlot2 = false;
                        }
                    }
                    if (snapIndex == 1) // bottom lock
                    {
                        // check if correct key
                        if (draggable.gameObject.name == "keyBottom")
                        {
                            keySlot3 = true;
                            Debug.Log("bottom key OK");

                            draggable.gameObject.transform.Translate(draggable.gameObject.transform.position.x, draggable.gameObject.transform.position.y, 10); // hide key sprite (push to the back)
                            keyInserted2.SetActive(true); // show the inserted key sprite

                            StartCoroutine(RotateKey(3));

                            audioPlayer.Play(); // play door unlocking sound
                        }
                        else
                        {
                            keySlot3 = false;
                        }
                    }

                    // are all keys in place?
                    if (keySlot2 == true && keySlot3 == true)
                    {
                        Debug.Log("DOOR NOW OPEN");
                        canGoThru2 = true;

                        goBackButton.interactable = false; // can't go back to the room
                        goThruDoorButton.interactable = true; // can go to the next room
                    }
                }

                if (sceneID == 14) // GLASSHOUSE DOOR
                {
                   if(snapIndex == 0)
                   {
                        // check if correct key
                        if (draggable.gameObject.CompareTag("Key"))
                        {
                            keySlot1 = true;
                            Debug.Log("key OK");

                            draggable.gameObject.transform.Translate(draggable.gameObject.transform.position.x, draggable.gameObject.transform.position.y, 10); // hide key sprite (push to the back)
                            keyInserted.SetActive(true); // show the inserted key sprite

                            StartCoroutine(RotateKey(1));

                            audioPlayer.Play(); // play door unlocking sound
                            canGoThru1 = true;

                            goBackButton.interactable = false; // can't go back to the room
                            goThruDoorButton.interactable = true; // can go to the next room
                        }
                        else
                        {
                            keySlot1 = false;
                        }
                   } 
                    
                }
            }
        }

        if (closestDistance > snapRange) //not in snap distance so snap back to original position
        {
            draggable.transform.localPosition = new Vector3(initialPos.x, initialPos.y, -9); // go back to the initial position of the object if not put in the snap slot
        }
    }

    public void OnDragInProgress(Draggable draggable)
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

    IEnumerator RotateKey(int whichKey)
    {
        GameObject keyToRotate = null;
        if (whichKey == 1)
        {
            keyToRotate = keyInserted; // 1st door
        }
        else if (whichKey == 2)
        {
            keyToRotate = keyInserted1; // 2nd door top lock
        }
        else if (whichKey == 3)
        {
            keyToRotate = keyInserted2; // 2nd door bottom lock
        }
        else
        {
            yield return null;
        }

        float timer = 0f;
        while (timer <= maxRotationTimer)
        {
            keyToRotate.transform.Rotate(new Vector3(0, 0, -180) * 2 * Time.deltaTime);
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
