using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /* SCENE INDICES -----------------------------
     * 3 -> main scene /->snap slots (WAS 0)
     * 1 -> round room
     * 6 -> glasshouse
     * 7 -> basement
     
     * 8 -> paintings PUZZLE
     * 9 -> chess PUZZLE HINT 
     * 10 -> bottles PUZZLE /->snap slots
    
     * 11 -> swimming pool
     
     * 12 -> cable fix PUZZLE /->snap slots
     * 13 -> electricity (lamp light) PUZZLE /-> snap slots
     
     * 14 -> glasshouse DOOR (1) /->snap slots
     * 15 -> basement DOOR (2) /->snap slots
     ------------------------------------------ */

    [SerializeField] public int sceneID; // store the id of the current scene
    [SerializeField] public int previousSceneID; // store the id of the previous scene


    [SerializeField] GameObject player;

    // collectibles flags (do not render in scene on load if previously already collected)
    public bool bottle1 = false;
    public bool bottle2 = false;
    public bool bottle3 = false;
    public bool bottle4 = false;
    public bool bottle5 = false;
    public bool bottle6 = false;

    GameObject[] pickUps;

    [SerializeField] GameObject optionsCanvas;

    public Button goToRRButton;

    public static bool plantGiven; // has plant been given back to the wizard? if so -> unblock the left door

    bool isFacingRight;
    // public PlayerController playerScript; // unity editor version
    public PlayerMovement playerScript; //CHANGED THIS IN BUILD V

    public bool lightOn;

    public bool newGameStarted = false; // to check when going back to settings/controls where we came from and change 'go back' button redirection


    // FLAG PUZZLES THAT HAVE ALREADY BEEN SOLVED 
    private static bool[] puzzleSolved = new bool[4]; // 4 puzzles in game to be marked as solved(true)/not solved yet (false)
    // id 0 -> paintings
    // id 1 -> cables
    // id 2 -> electricity
    // id 3 -> bottles

    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        player = GameObject.FindGameObjectWithTag("Player"); // get reference to the player object
        playerScript = player.GetComponent<PlayerMovement>(); // get the cursor manager script //CHANGED THIS IN BUILD V

        optionsCanvas = GameObject.Find("OptionsCanvas");

        plantGiven = false;

        // MAIN SCENE
        if (sceneID == 3)
        {
            goToRRButton = GameObject.Find("RoundRoomButton").GetComponent<Button>(); // get reference to the navigation button
            goToRRButton.interactable = false;
        }

        previousSceneID = -1; // initialize with value
    }

    // called first
    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

        isFacingRight = playerScript.isFacingRight; // var from playercontroller script

        sceneID = SceneManager.GetActiveScene().buildIndex; // get the id of current scene

        if(sceneID == 1)
        {
            newGameStarted = true; // change the flag to conditionally change settings/controls goback button redirection

            HintManager.hintActive[0] = true; // paintings hint can now be accessed
            HintManager.hintActive[1] = true; // cables hint can now be accessed
        }

        if (!(sceneID == 8 || sceneID == 9 || sceneID == 10 || sceneID == 12 || sceneID == 14 || sceneID == 15 || sceneID == 0 || sceneID == 5))
        {
            // Debug.Log("ENABLING THE PLAYER");
            player.SetActive(true); // not a puzzle scene
        }
        else
        {
            // Debug.Log("DISABLING THE PLAYER");
            player.SetActive(false); // this is a puzzle scene/ door scene so perform the appropriate actions
        }

/*        if(!(sceneID == 5 || sceneID == 16))
        {
            previousSceneID = sceneID; // remember this scene id for later navigation back
        }*/


        if (sceneID == 1) // ROUND ROOM
        {
            player.SetActive(true);
            player.transform.localPosition = new Vector3(-3, -3, -3); // move player to coordinates in the scene

            pickUps = GameObject.FindGameObjectsWithTag("Bottle"); // get ref to bottles, check if previously collected, if so destroy 

            foreach (GameObject pickUp in pickUps)
            {
                if ((pickUp.name == "middleBottle" && bottle1 == true) || (pickUp.name == "bottle2" && bottle2 == true) || (pickUp.name == "leftBottle" && bottle3 == true) || (pickUp.name == "bottle4" && bottle4 == true) || (pickUp.name == "rightBottle" && bottle5 == true) || (pickUp.name == "bottle6" && bottle6 == true))
                {
                    Destroy(pickUp); // gameobj shouldn't be rendered anymore so destroy when attempted
                }
            }
        }

        if (sceneID == 6) // GLASSHOUSE
        {
            player.SetActive(true);
            player.transform.localPosition = new Vector3(-0.5f, -3.4f, -1); // move player 

            HintManager.hintActive[2] = true; //unblock bottles hint
        }

        if (sceneID == 11) // SWIMMING POOL (GLASSHOUSE TOP FLOOR)
        {
            player.SetActive(true);
            player.transform.localPosition = new Vector3(2.1f, 1.4f, -1); // move player 
            playerScript.speed = 2.0f; // player smaller so needs to proportionally walk a bit slower

            lightOn = LampController.lightOn; // read lightOn value from lamp script

            if (isFacingRight)
            {
                player.transform.localScale = new Vector3(0.16f, 0.16f, 0.16f); // rescale player only in this room
            }
            else
            {
                player.transform.localScale = new Vector3(-0.16f, 0.16f, 0.16f); // rescale player only in this room
            }

            HintManager.hintActive[3] = true; // unblock fish hint
        }
        else
        {
            if (isFacingRight)
            {
                player.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f); // rescale player back to normal size
            }
            else
            {
                player.transform.localScale = new Vector3(-0.3f, 0.3f, 0.3f); // rescale player back to normal size
            }

            playerScript.speed = 2.4f;
        }

        // MAIN SCENE
        if (sceneID == 3)
        {
            goToRRButton = GameObject.Find("RoundRoomButton").GetComponent<Button>(); // get reference to the navigation button
            goToRRButton.interactable = false;
        }

    }

    // getter and setter for checking if puzzles solved
    public bool checkIfSolved(int i)
    {
        return puzzleSolved[i]; // to check if certain puzzle was solved
    }
    public void markAsSolved(int i)
    {
        puzzleSolved[i] = true; // mark appropriate puzzle as already solved
    }
       
    // public getter for ID of current scene
    public int getSceneID()
    {
        return sceneID;
    }
}
