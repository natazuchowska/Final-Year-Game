using System.Collections;
using System.Collections.Generic;
/*using Unity.Mathematics;
using Unity.VisualScripting;
using UnityEditor.SearchService;*/
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
    public AudioSource audioPlayer; // to play rewarding sound when puzzle solved

    [SerializeField] public static bool canGoThru1; // glasshouse door
    [SerializeField] public static bool canGoThru2; // basement door


    // nav buttons
    [SerializeField] public Button goBackButton;
    [SerializeField] public Button goThruDoorButton;

    //instantiate key slots to false
    public static bool keySlot1 = false;
    public static bool keySlot2 = false;
    public static bool keySlot3 = false;

    // check if key dropped in a door scene
    public static int k1D = 0;
    public static int k2D = 0;
    public static int k3D = 0;

    [SerializeField] GameObject key1prev;
    [SerializeField] GameObject key2prev;
    [SerializeField] GameObject key3prev;

    [SerializeField] private AudioSource incorrectLockSound;

    // ------------------------------------------------------------------------------

    //instantiate bottle slots to false
    public static bool bottleSlot1;
    public static bool bottleSlot2;
    public static bool bottleSlot3;

    // flags
    public static int bLD = 0;
    public static int bMD = 0;
    public static int bRD = 0;

    GameObject bottleKeyReward; // reward for solving the puzzle

    // steam to be disabled when correct bottle inserted
    GameObject steamLeft;
    GameObject steamMid;
    GameObject steamRight;

    GameObject bottleLprev;
    GameObject bottleMprev;
    GameObject bottleRprev;


    [SerializeField] GameObject backgroundAfter; // to change the background when puzzle solved
    [SerializeField] AudioSource steamSound;

    // ------------------------------------------------------------------------------

    public static bool electricityFlow = false; //electricity turned off initially, // check if electricity ON -> if yes display open box

    //instantiate slots to false
    bool cableSlot1 = false;
    bool cableSlot2 = false;
    bool cableSlot3 = false;
    bool cableSlot4 = false;

    [SerializeField] GameObject cableFixedRED;
    [SerializeField] GameObject cableFixedBLUE;
    [SerializeField] GameObject cableFixedBLACK1;
    [SerializeField] GameObject cableFixedBLACK2;

    [SerializeField] private AudioSource bzzSound;
    GameObject puzzleSolvedBg;

    // -----------------------------------------------------------------------------

    [SerializeField] GameObject keyInserted; // sprite of the key after insertion to lock -> 1st door
    [SerializeField] GameObject keyInserted1; // sprite of the key after insertion to lock -> 2nd door top lock
    [SerializeField] GameObject keyInserted2; // sprite of the key after insertion to lock -> 2nd door bottom lock
    public float maxRotationTimer = 0.5f;


    // ------------------------------------------------------------------------------

    [SerializeField] public static bool lightOn = true; // lamp on and needs to be switched off (if initialized here, when set to false would the false value persist when script is executed again?)

    GameObject electricityBackgroundAfter;

    // scenes with snap slots
    // ids: 3 10 12 13 14 15

    void Awake()
    {
        foreach (Draggable draggable in draggableObjects)
        {
            draggable.dragEndedCallback = OnDragEnded; // delegate type storing reference to method, will invoke OnDragEnded in OnMouseUp
            draggable.dragInProgressCallback = OnDragInProgress; // when the mouse is holding the object
            draggable.whereBeforeDragCallback = OnBeforeDrag;
        }

        sceneID = SceneManager.GetActiveScene().buildIndex; // get the id of the scene

        // DOOR SCENES
        if (sceneID == 14 || sceneID == 15)
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

            // key1prev = GameObject.Find("glasshouseKey");
            // key1prev.SetActive(false);
        }
        if (sceneID == 15) // 2nd door
        {
            // find keys by object names
            keyInserted1 = GameObject.Find("keyInserted1");
            keyInserted2 = GameObject.Find("keyInserted2");

            if (keySlot2 == false)
            {
                keyInserted1.SetActive(false);
            }
            else
            {
                keyInserted1.SetActive(true);
            }

            if (keySlot3 == false)
            {
                keyInserted2.SetActive(false);
            }
            else
            {
                keyInserted2.SetActive(true);
            }
        }

        // BOTTLES PUZZLE SCENE
        if (sceneID == 10)
        {

            bottleKeyReward = GameObject.FindGameObjectWithTag("KeyReward"); // get the key object
            bottleKeyReward.SetActive(false); // deactivate key until puzzle not solved

            // find and store reference to steam sprites
            steamLeft = GameObject.FindGameObjectWithTag("SteamL");
            steamMid = GameObject.FindGameObjectWithTag("SteamM");
            steamRight = GameObject.FindGameObjectWithTag("SteamR");

            // puzzle has been solved already so display after background and remove draggable and change cursor cripts from bottles
            if (GameObject.Find("GameManager").GetComponent<GameManager>().checkIfSolved(3))
            {
                backgroundAfter.SetActive(true);
                Destroy(steamLeft);
                Destroy(steamMid);
                Destroy(steamRight);

                // draggable.gameObject.name == "rightBottle(Clone)" || draggable.gameObject.name == "rightBottle"
                Destroy(GameObject.Find("rightBottle(Clone)"));
                Destroy(GameObject.Find("rightBottle"));

                Destroy(GameObject.Find("middleBottle(Clone)"));
                Destroy(GameObject.Find("middleBottle"));

                Destroy(GameObject.Find("leftBottle(Clone)"));
                Destroy(GameObject.Find("leftBottle"));

                GameObject.Find("leftBottleSolved").SetActive(true);
                GameObject.Find("middleBottleSolved").SetActive(true);
                GameObject.Find("rightBottleSolved").SetActive(true);

                // hide the key
                bottleKeyReward.transform.localPosition = new Vector3(bottleKeyReward.transform.localPosition.x, bottleKeyReward.transform.localPosition.y, 10);
            }
            else
            {
                if(bottleSlot1 == true)
                {
                    Destroy(GameObject.Find("leftBottle(Clone)"));
                    Destroy(GameObject.Find("leftBottle"));
                    Destroy(steamLeft);
                    GameObject.Find("leftBottleSolved").SetActive(true);
                }
                else
                {
                    GameObject.Find("leftBottleSolved").SetActive(false);
                }

                if (bottleSlot2 == true)
                {
                    Destroy(GameObject.Find("middleBottle(Clone)"));
                    Destroy(GameObject.Find("middleBottle"));
                    Destroy(steamMid);
                    GameObject.Find("middleBottleSolved").SetActive(true);
                }
                else
                {
                    GameObject.Find("middleBottleSolved").SetActive(false);
                }

                if (bottleSlot3 == true)
                {
                    Destroy(GameObject.Find("rightBottle(Clone)"));
                    Destroy(GameObject.Find("rightBottle"));
                    Destroy(steamRight);
                    GameObject.Find("rightBottleSolved").SetActive(true);
                }
                else
                {
                    GameObject.Find("rightBottleSolved").SetActive(false);
                }

                // set to be visible on start
                if (!bottleSlot1 && steamLeft!=null)
                {
                    steamLeft.SetActive(true);
                }
                else
                {
                    steamLeft.SetActive(false);
                }

                if (!bottleSlot2 && steamMid!=null)
                {
                    steamMid.SetActive(true);
                }
                else
                {
                    steamMid.SetActive(false);
                }

                if (!bottleSlot3 && steamRight!=null)
                {
                    steamRight.SetActive(true);
                }
                else
                {
                    steamRight.SetActive(false);
                }
            }

            bottleLprev = GameObject.Find("leftBottle");
            bottleMprev = GameObject.Find("middleBottle");
            bottleRprev = GameObject.Find("rightBottle");

            bottleLprev.SetActive(false);
            bottleMprev.SetActive(false);
            bottleRprev.SetActive(false);

            // if returning to the scene and bottle dropped but not inserted
            if (bottleSlot1 == true || bLD != 0)
            {    
                 if(steamLeft!=null)
                 {
                     steamLeft.SetActive(false);
                 }

                 bottleLprev.SetActive(true);

                if (!bottleSlot1)
                {
                    bottleLprev.transform.localPosition = new Vector3(-7, 0, -3);
                    if (steamLeft != null && !GameObject.Find("GameManager").GetComponent<GameManager>().checkIfSolved(3))
                    {
                        steamLeft.SetActive(true);
                    }
                }
            }

            if (bottleSlot2 == true || bMD != 0)
            {
                if(steamMid!=null)
                {
                    steamMid.SetActive(false);
                }

                bottleMprev.SetActive(true);

                if (!bottleSlot2)
                {
                    bottleMprev.transform.localPosition = new Vector3(-7, 2, -3);
                    if(steamMid!=null && !GameObject.Find("GameManager").GetComponent<GameManager>().checkIfSolved(3))
                    {
                        steamMid.SetActive(true);
                    }
                }
            }

            if (bottleSlot3 == true || bRD != 0)
            {
                if(steamRight!=null)
                {
                    steamRight.SetActive(false);
                }

                bottleRprev.SetActive(true);

                if (!bottleSlot3)
                {
                    bottleRprev.transform.localPosition = new Vector3(-7, -2, -3);
                    if(steamRight!=null && !GameObject.Find("GameManager").GetComponent<GameManager>().checkIfSolved(3))
                    {
                        steamRight.SetActive(true);
                    }
                }
            }
                 
            backgroundAfter.SetActive(false);
        }

        // cables puzzle scene
        if (sceneID == 12)
        {
            puzzleSolvedBg = GameObject.FindGameObjectWithTag("BackgroundAfter");
            puzzleSolvedBg.SetActive(false);

            cableFixedRED.SetActive(false);
            cableFixedBLUE.SetActive(false);
            cableFixedBLACK1.SetActive(false);
            cableFixedBLACK2.SetActive(false);

            if (puzzleSolvedBg != null && GameObject.Find("GameManager").GetComponent<GameManager>().checkIfSolved(1))// check if cables puzzle was already solved -> if so hide the cable sprites
            {
                if (puzzleSolvedBg.activeSelf == false)
                {
                    puzzleSolvedBg.SetActive(true);
                    Destroy(GameObject.Find("cable1"));
                    Destroy(GameObject.Find("cable2"));
                    Destroy(GameObject.Find("cable3"));
                    Destroy(GameObject.Find("cable4"));
                }
            }
        }
    }

    private void Start()
    {
        if(sceneID == 10)
        {

            // puzzle has been solved already so display after background
            if (GameObject.Find("GameManager").GetComponent<GameManager>().checkIfSolved(3))
            {
                backgroundAfter.SetActive(true);
                Destroy(steamLeft);
                Destroy(steamMid);
                Destroy(steamRight);

                // hide the key
                Destroy(bottleKeyReward.GetComponent<CursorChangeObject>());
                Destroy(bottleKeyReward.GetComponent<PickUp>());
            }
        }

        if(sceneID == 14)
        {
            if (k1D != 0 && keySlot1 == false) // if key was previously dropped in the scene render it when entered again
            {
                key1prev.SetActive(true);
            }
            else
            {
                key1prev.SetActive(false);
            }
        }

        if(sceneID == 15)
        {
            if (k2D != 0 && keySlot2 == false) // if key was previously dropped in the scene render it when entered again
            {
                key2prev.SetActive(true);
            }
            else
            {
                key2prev.SetActive(false);
            }

            if (k3D != 0 && keySlot3 == false) // if key was previously droped in the scene render it when entered again
            {
                key3prev.SetActive(true);
            }
            else 
            {
                key3prev.SetActive(false);
            }
        }
    }

    private void Update()
    {
        if(sceneID == 10)
        {
            if (!GameObject.Find("GameManager").GetComponent<GameManager>().checkIfSolved(3))
            {
                foreach (Transform snapPt in snapPoints)
                {
                    if (snapPt.transform.childCount == 0)
                    {
                        if (snapPoints.IndexOf(snapPt) == 0 && steamLeft!=null)
                        {
                            steamLeft.SetActive(true);
                        }
                        if (snapPoints.IndexOf(snapPt) == 1 && steamMid!=null)
                        {
                            steamMid.SetActive(true);
                        }
                        if (snapPoints.IndexOf(snapPt) == 2 && steamRight!=null)
                        {
                            steamRight.SetActive(true);
                        }
                    }
                }
            }
                
        }
        

        foreach (Draggable draggable in draggableObjects)
        {
            draggable.dragEndedCallback = OnDragEnded; // delegate type storing reference to method, will invoke OnDragEnded in OnMouseUp
            draggable.dragInProgressCallback = OnDragInProgress; // when the mouse is holding the object
            draggable.whereBeforeDragCallback = OnBeforeDrag;
        }

    }


    public void OnBeforeDrag(Draggable draggable) // was private before with delegates
    {
        if(sceneID != 10)
        {
            initialPos = draggable.transform.localPosition; // store the position before drag happens

            if (draggable.transform.parent != null)
            {
                initialPos = draggable.transform.parent.transform.localPosition;
            }
        }
    }

    public void OnDragEnded(Draggable draggable)
    {
        float closestDistance = -1;
        Transform closestSnapPoint = null;
        int snapIndex = -1;

        if(sceneID == 10 || sceneID == 12)
        {
            audioPlayer = GameObject.Find("PuzzleSolvedAudio").GetComponent<AudioSource>();
        }

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

        if(closestSnapPoint != null && closestDistance <= snapRange && closestSnapPoint.transform.childCount == 0 /* only if snappoint is not yet taken */) // if item can be inserted into the slot
        {
            if(closestSnapPoint.transform.childCount == 1)
            {
                draggable.transform.localPosition = initialPos;
                return;
            }
            draggable.transform.localPosition = closestSnapPoint.localPosition; // put the object in the centre of the (closest) snappoint
            draggable.gameObject.GetComponent<Renderer>().material.color = Color.white;

            // make the draggable object a child of the snappoint -> bottles puzzle only
            if(sceneID == 10)
            {
                draggable.transform.parent = closestSnapPoint.transform;
            }

            // MAIN SCENE ===========================================================================
            if (draggable.gameObject.CompareTag("Plant"))
            {
                Debug.Log("Character is saying: Thank you for the plant girlie");
                GameObject plant = draggable.gameObject;
                plant.transform.localScale = new Vector3(0.2f, 0.2f, 0.2f);
                plant.transform.localPosition = new Vector3(plant.transform.localPosition.x, plant.transform.localPosition.y, -3); // move the plant to the front
                plant.GetComponent<Renderer>().material.color = Color.white;

                GameManager.plantGiven = true; // inform game mngr that plant was received
                GameObject.Find("doorOpeningAudio").GetComponent<AudioSource>().Play(); // play door sound to show it opened
            }

            // DOOR SCENES ===========================================================================
            if (draggable.gameObject.CompareTag("Key")) // if the object inserted in the slot is a key
            {
                if (sceneID == 15) // BASEMENT DOOR
                {
                    if (snapIndex == 0) // top lock
                    {
                        if(incorrectLockSound == null)
                        {
                            incorrectLockSound = GameObject.Find("IncorrectLockSound").GetComponent<AudioSource>();
                        }

                        // check if correct key
                        if (draggable.gameObject.name == "keyTop(Clone)" || draggable.gameObject.name == "keyTop")
                        {
                            keySlot2 = true;
                            Debug.Log("top key OK");


                            draggable.gameObject.transform.Translate(draggable.gameObject.transform.position.x, draggable.gameObject.transform.position.y, 10); // hide key sprite (push to the back)
                            keyInserted1.SetActive(true); // show the inserted key sprite

                            StartCoroutine(RotateKey(2));
                            audioPlayer.Play(); // play door unlocking sound

                            if(!keySlot3)
                            {
                                GameObject.Find("Canvas").GetComponent<ConfirmPanelManager>().DisplayPanel(); // show '1/2' panel
                            }
                        }
                        else
                        {
                            keySlot2 = false;
                            incorrectLockSound.Play();
                        }
                    }
                    if (snapIndex == 1) // bottom lock
                    {
                        // check if correct key
                        if (draggable.gameObject.name == "keyBottom(Clone)" || draggable.gameObject.name == "keyBottom")
                        {
                            keySlot3 = true;
                            Debug.Log("bottom key OK");

                            draggable.gameObject.transform.Translate(draggable.gameObject.transform.position.x, draggable.gameObject.transform.position.y, 10); // hide key sprite (push to the back)
                            keyInserted2.SetActive(true); // show the inserted key sprite

                            StartCoroutine(RotateKey(3));
                            audioPlayer.Play(); // play door unlocking sound

                            if (!keySlot2)
                            {
                                GameObject.Find("Canvas").GetComponent<ConfirmPanelManager>().DisplayPanel(); // show '1/2' panel
                            }
                        }
                        else
                        {
                            keySlot3 = false;
                            incorrectLockSound.Play();
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
            if((draggable.gameObject.CompareTag("SnapBottle") || draggable.gameObject.CompareTag("Bottle")) && !GameObject.Find("GameManager").GetComponent<GameManager>().checkIfSolved(3))
            {
                if (snapIndex == 0)
                {
                    // check which bottle that is
                    if (draggable.gameObject.name == "leftBottle(Clone)" || draggable.gameObject.name == "leftBottle")
                    {
                        bottleSlot1 = true;
                        steamLeft.SetActive(false);

                        steamSound.Play();
                    }
                    else
                    {
                        bottleSlot1 = false;
                        if (!GameObject.Find("GameManager").GetComponent<GameManager>().checkIfSolved(3))
                        {
                            steamLeft.SetActive(true);
                        }
                    }

                }
                if (snapIndex == 1)
                {
                    // check which bottle that is
                    if ((draggable.gameObject.name == "middleBottle(Clone)" || draggable.gameObject.name == "middleBottle"))
                    {
                        bottleSlot2 = true;
                        steamMid.SetActive(false);

                        steamSound.Play();
                    }
                    else
                    {
                        bottleSlot2 = false;
                        if (!GameObject.Find("GameManager").GetComponent<GameManager>().checkIfSolved(3))
                        {
                            steamMid.SetActive(true);
                        }
                    }
                }
                if (snapIndex == 2)
                {

                    // check which bottle that is
                    if (draggable.gameObject.name == "rightBottle(Clone)" || draggable.gameObject.name == "rightBottle")
                    {
                        bottleSlot3 = true;
                        steamRight.SetActive(false);

                        steamSound.Play();
                    }
                    else
                    {
                        bottleSlot3 = false;

                        if(!GameObject.Find("GameManager").GetComponent<GameManager>().checkIfSolved(3))
                        {
                            steamRight.SetActive(true);
                        }
                    }
                }

                Debug.Log("bottle slot1: " + bottleSlot1 + ", bottle slot2: " + bottleSlot2 + ", bottle slot3: " + bottleSlot3);

                // are all in correct order?
                if (bottleSlot1 == true && bottleSlot2 == true && bottleSlot3 == true)
                {
                    bottleKeyReward.SetActive(true);
                    backgroundAfter.SetActive(true);
                    audioPlayer.Play(); // play puzzle solved sound 

                    GameObject.Find("GameManager").GetComponent<GameManager>().markAsSolved(3); // mark appropriate puzzle flag in game mngr as solved

                }
            }
            if(draggable.gameObject.CompareTag("CableFix"))
            {
                if (snapIndex == 0) // black short
                {
                    // check which bottle that is
                    if (draggable.gameObject.name == "cable1")
                    {
                        cableSlot1 = true;

                        cableFixedBLACK1.SetActive(true);
                        GameObject c = GameObject.Find("cable1");
                        c.transform.localPosition = new Vector3(c.transform.localPosition.x, c.transform.localPosition.y, 10);

                        bzzSound.Play();
                    }
                    else
                    {
                        cableSlot1 = false;
                    }
                }
                if (snapIndex == 1) // blue
                {
                    // check which cable that is
                    if (draggable.gameObject.name == "cable2")
                    {
                        cableSlot2 = true;

                        cableFixedBLUE.SetActive(true);
                        GameObject c = GameObject.Find("cable2");
                        c.transform.localPosition = new Vector3(c.transform.localPosition.x, c.transform.localPosition.y, 10);

                        bzzSound.Play();
                    }
                    else
                    {
                        cableSlot2 = false;
                    }
                }
                if (snapIndex == 2) // red
                {
                    // check which cable that is
                    if (draggable.gameObject.name == "cable3")
                    {
                        cableSlot3 = true;

                        cableFixedRED.SetActive(true);
                        GameObject c = GameObject.Find("cable3");
                        c.transform.localPosition = new Vector3(c.transform.localPosition.x, c.transform.localPosition.y, 10);

                        bzzSound.Play();
                    }
                    else
                    {
                        cableSlot3 = false;
                    }
                }
                if (snapIndex == 3) // black long
                {
                    // check which cable that is
                    if (draggable.gameObject.name == "cable4")
                    {
                        cableSlot4 = true;

                        cableFixedBLACK2.SetActive(true);
                        GameObject c = GameObject.Find("cable4");
                        c.transform.localPosition = new Vector3(c.transform.localPosition.x, c.transform.localPosition.y, 10);

                        bzzSound.Play();
                    }
                    else
                    {
                        cableSlot4 = false;
                    }
                }

                // are all in correct order?
                if (cableSlot1 == true && cableSlot2 == true && cableSlot3 == true && cableSlot4 == true)
                {
                    Debug.Log("CORRECT CABLE FIX! CONGRATS!!!!");

                    electricityFlow = true; // electricity now on -> can open the box with cables on the other wall
                    audioPlayer.Play(); // play puzzle solved sound 

                    if(puzzleSolvedBg != null)
                    {
                        puzzleSolvedBg.SetActive(true);
                    }

                    GameObject.Find("GameManager").GetComponent<GameManager>().markAsSolved(1); // mark appropriate puzzle flag in game mngr as solved
                }
            }
        }

        if (closestDistance > snapRange) //not in snap distance so snap back to original position
        {
            if(sceneID != 10 && sceneID != 12)
            {
                draggable.transform.localPosition = new Vector3(initialPos.x, initialPos.y, -2.6f); // go back to the initial position of the object if not put in the snap slot
            }


            if (sceneID == 10)
            {
                if(snapIndex == 0 && GameObject.Find("SnapPointBL").transform.childCount == 0 && !GameObject.Find("GameManager").GetComponent<GameManager>().checkIfSolved(3))
                {
                    steamLeft.SetActive(true);
                }

                if (snapIndex == 1 && GameObject.Find("SnapPointBM").transform.childCount == 0 && !GameObject.Find("GameManager").GetComponent<GameManager>().checkIfSolved(3))
                {
                    steamMid.SetActive(true);
                }

                if (snapIndex == 2 && GameObject.Find("SnapPointBR").transform.childCount == 0 && !GameObject.Find("GameManager").GetComponent<GameManager>().checkIfSolved(3))
                {
                    steamRight.SetActive(true);
                }
            }
            // draggable.transform.parent = null; // reset the parent
        }

        initialPos = draggable.transform.position; // store the GLOBAL position before drag happens
    }

    public void OnDragInProgress(Draggable draggable)
    {

        float closestDistance = -1;
        Transform closestSnapPoint = null;

        if (sceneID == 10)
        {
            // store position not relative to parent
            if(draggable.transform.parent != null)
            {
                draggable.transform.localPosition = new Vector3(draggable.transform.parent.transform.localPosition.x, draggable.transform.parent.transform.localPosition.y, -2.6f);
            }
            else
            {
                draggable.transform.localPosition = new Vector3(draggable.transform.localPosition.x, draggable.transform.localPosition.y, -2.6f);
            }
        }

        draggable.transform.parent = null; // reset the parent

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
            if (draggable.gameObject.CompareTag("Plant") || draggable.gameObject.CompareTag("SnapBottle") || draggable.gameObject.CompareTag("Bottle") || draggable.gameObject.CompareTag("CableFix")) // if the thing being dragged in the slot is a plant object
            {
                draggable.gameObject.GetComponent<Renderer>().material.color = Color.grey; // change object's clour to grey when put near the slot
            }
        }

        if (closestSnapPoint != null && closestDistance > snapRange) // outside of snap range so reset the colour
        {
            // takenSnapPoints[snapPoints.IndexOf(closestSnapPoint)] = 0;

            if (draggable.gameObject.CompareTag("Plant") || draggable.gameObject.CompareTag("SnapBottle") || draggable.gameObject.CompareTag("CableFix")) // if the thing being dragged in the slot is a plant object
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

    public static void markBottleDropped(int i)
    {
        switch(i)
        {
            case 1:
                bLD = 1;
                return;
            case 2:
                bMD = 1;
                return;
            case 3:
                bRD = 1;
                return;
        }
    }

    public static void markKeyDropped(int i)
    {
        switch (i)
        {
            case 1:
                k1D = 1; // 1st door
                return;
            case 2:
                k2D = 1; // 2nd door top lock
                return;
            case 3:
                k3D = 1; // 2nd door bottom lock
                return;
        }
    }
}
