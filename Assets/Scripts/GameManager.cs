using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

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


    private void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        player = GameObject.FindGameObjectWithTag("Player"); // get reference to the player object

        /*sceneID = SceneManager.GetActiveScene().buildIndex; // get the id of current scene
        player = GameObject.FindGameObjectWithTag("Player"); // get reference to the player object

        if (sceneID == 8 || sceneID == 9 || sceneID == 10 || sceneID == 12)
        {
            puzzleSceneAction(); // this is a puzzle scene so perform the appropriate actions
        }*/
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

        sceneID = SceneManager.GetActiveScene().buildIndex; // get the id of current scene

        if (sceneID == 8 || sceneID == 9 || sceneID == 10 || sceneID == 12 || sceneID == 14 || sceneID == 15)
        {
            puzzleSceneAction(); // this is a puzzle scene/ door scene so perform the appropriate actions
        }
        else
        {
            player.SetActive(true);
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        /*sceneID = SceneManager.GetActiveScene().buildIndex; // get the id of current scene
        player = GameObject.FindGameObjectWithTag("Player"); // get reference to the player object

        if(sceneID == 8 || sceneID == 9 || sceneID == 10 || sceneID == 12)
        {
            puzzleSceneAction(); // this is a puzzle scene so perform the appropriate actions
        }*/
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
