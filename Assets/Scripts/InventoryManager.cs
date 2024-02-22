using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private GameObject inventoryCanvas;
    public bool isOpen = false;

    [SerializeField] GameObject optionsCanvas;

    GameObject player;
    bool playerIsMoving;

    bool gamePaused; // if pausescreen displayed we want to close the inventory

    private void Awake()
    {
        inventoryCanvas = GameObject.FindGameObjectWithTag("Inventory"); // find the reference to inventory canvas
    }

    private void Start()
    {
        optionsCanvas = GameObject.Find("OptionsCanvas");

        inventoryCanvas.SetActive(false); // hide inentory by default
        isOpen = false;
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        inventoryCanvas = GameObject.FindGameObjectWithTag("Inventory"); // find the reference to inventory canvas
        isOpen = true;
        OpenInventory();
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // open inventory with inventory button or space key
            OpenInventory();
        }

        player = GameObject.FindGameObjectWithTag("Player"); // get reference to the player object
        playerIsMoving = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().isMoving; // check if player is moving

        // only do this if there is a player sprite in the current scene
        if(player.active == true)
        {
            if (playerIsMoving == true)
            {
                inventoryCanvas.SetActive(false);
                isOpen = false;
            }
        }

        gamePaused = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PauseController>().isPaused; // get value if game paused or not
        // if paused -> close the inventory

        if(gamePaused == true)
        {
            inventoryCanvas.SetActive(false);
            isOpen = false;
        }
    }

    public void OpenInventory()
    {
        // if this is a scene where there is an inventory
        if(inventoryCanvas != null)
        {
            if (!isOpen)
            {
                inventoryCanvas.SetActive(true); // open on first click if closed
            }
            else
            {
                inventoryCanvas.SetActive(false); // close on second click if open
            }

            isOpen = !isOpen;
        }
    }
}
