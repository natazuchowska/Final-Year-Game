using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    /* SCENE INDICES -----------------------------
     * 0 -> main scene
     * 1 -> round room
     * 6 -> glasshouse
     * 7 -> basement
     * 8 -> paintings PUZZLE
     * 9 -> chess PUZZLE HINT
     * 10 -> bottles PUZZLE
     * 11 -> swimming pool
     * 12 -> cable fix PUZZLE
     * 13 -> electricity (lamp light) PUZZLE
     * 14 -> glasshouse DOOR (1)
     * 15 -> basement DOOR (2)
     ------------------------------------------ */

    [SerializeField] private int sceneID; // strore the id of the current scene
    [SerializeField] GameObject player;

    // collectibles flags (do not render in scene on load if previously already collected)
    public bool bottle1 = false;
    public bool bottle2 = false;
    public bool bottle3 = false;
    public bool bottle4 = false;
    public bool bottle5 = false;
    public bool bottle6 = false;

    GameObject[] pickUps;


    public Button goToRRButton;
    // [SerializeField] public int firstTime = 0; // flag for 1st plant puzzle

    public static bool plantGiven; // has plant been given back to the wizard? if so -> unblock the left door


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        player = GameObject.FindGameObjectWithTag("Player"); // get reference to the player object
        plantGiven = false;

        // MAIN SCENE
        if (sceneID == 0)
        {
            goToRRButton = GameObject.Find("RoundRoomButton").GetComponent<Button>(); // get reference to the navigation button
            goToRRButton.interactable = false;
        }
    }

    // called first
    void OnEnable()
    {
        Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
        // player = GameObject.FindGameObjectWithTag("Player"); // get reference to the player object
    }

    // called second
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded: " + scene.name);
        Debug.Log(mode);

        // inventoryCanvas.SetActive(false); // close inventory (if was open) on every new scene load

        sceneID = SceneManager.GetActiveScene().buildIndex; // get the id of current scene

        if (sceneID == 8 || sceneID == 9 || sceneID == 10 || sceneID == 12 || sceneID == 14 || sceneID == 15)
        {
            puzzleSceneAction(); // this is a puzzle scene/ door scene so perform the appropriate actions
        }
        else
        {
            player.SetActive(true);
        }

        if (sceneID == 1) // ROUND ROOM
        {
            player.transform.localPosition = new Vector3(-3, -3, -3); // move player to coordinates in the scene

            pickUps = GameObject.FindGameObjectsWithTag("Bottle"); // get ref to bottles, check if previously collected, if so destroy 

            foreach(GameObject pickUp in pickUps)
            { 
                if((pickUp.name == "bottle1" && bottle1 == true) || (pickUp.name == "bottle2" && bottle2 == true) || (pickUp.name == "bottle3" && bottle3 == true) || (pickUp.name == "bottle4" && bottle4 == true) || (pickUp.name == "bottle5" && bottle5 == true) || (pickUp.name == "bottle6" && bottle6 == true))
                {
                    Destroy(pickUp); // gameobj shouldn't be rendered anymore so destroy when attempted
                }
            }
        }

        if (sceneID == 6) // GLASSHOUSE
        {
            player.transform.localPosition = new Vector3(-0.5f, -3.4f, -1); // move player 
        }

        if (sceneID == 11) // SWIMMING POOL (GLASSHOUSE TOP FLOOR)
        {
            player.transform.localPosition = new Vector3(2.1f, 1.4f, -1); // move player 
            player.transform.localScale = new Vector3(-0.16f, 0.16f, 0.16f); // rescale player only in this room
        }
        else
        {
            player.transform.localScale = new Vector3(0.3f, 0.3f, 0.3f); // rescale player back to normal size
        }

        // MAIN SCENE
        if (sceneID == 0)
        {
            goToRRButton = GameObject.Find("RoundRoomButton").GetComponent<Button>(); // get reference to the navigation button
            goToRRButton.interactable = false;
        }

    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // current scene is a puzzle scene
    void puzzleSceneAction()
    {
        player.SetActive(false); // hide the player sprite
        Debug.Log("PUZZLE SCENE - DISABLE PLAYER");
    }

    // set initial position and scale of the player in the newly loaded scene
    void setInitialPlayer()
    {

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
