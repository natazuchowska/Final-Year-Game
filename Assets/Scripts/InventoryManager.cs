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
    bool playerIsMoving;

    private void Start()
    {
        inventoryCanvas = GameObject.FindGameObjectWithTag("Inventory"); // find the reference to inventory canvas
        inventoryCanvas.SetActive(false); // hide inentory by default
        isOpen = false;

        optionsCanvas = GameObject.Find("OptionsCanvas");
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
        // player = GameObject.FindGameObjectWithTag("Player"); // get reference to the player object
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        
        inventoryCanvas.SetActive(false); // close inventory (if was open) on every new scene load
        isOpen = false;
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            // open inventory with inventory button or space key
            OpenInventory();
        }

        playerIsMoving = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().isMoving; // check if player is moving
        if (playerIsMoving == true)
        {
            inventoryCanvas.SetActive(false);
            isOpen = false;
        }
    }

    public void OpenInventory()
    {
        if(!isOpen) {
            inventoryCanvas.SetActive(true); // open on first click if closed
        }
        else {
            inventoryCanvas.SetActive(false); // close on second click if open
        }

        isOpen = !isOpen;
    }
}
