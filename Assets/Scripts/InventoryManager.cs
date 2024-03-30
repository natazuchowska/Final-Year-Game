using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private GameObject inventoryCanvas;
    public bool isOpen = false;

    [SerializeField] GameObject optionsCanvas;

    [SerializeField] public GameObject invIcon;
    [SerializeField] public Button invButton;

    GameObject player;
    bool playerIsMoving;

    bool gamePaused; // if pausescreen displayed we want to close the inventory

    public float maxRotationTimer = 0.5f;

    private void Awake()
    {
        inventoryCanvas = GameObject.FindGameObjectWithTag("Inventory"); // find the reference to inventory canvas
        player = GameObject.FindGameObjectWithTag("Player"); // get reference to the player object

        invIcon = GameObject.Find("InventoryButton");
        invButton = GameObject.Find("InventoryButton").GetComponent<Button>();
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
        isOpen = true;
        OpenInventory();
    }

    private void Update()
    {
    
        if (Input.GetKeyDown(KeyCode.Space))
        {
            // open inventory with inventory button or space key
            OpenInventory();
        }

        // only do this if there is a player sprite in the current scene
        if (player.activeInHierarchy == true)
        {
            playerIsMoving = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().isMoving; // check if player is moving

            if (playerIsMoving == true)
            {
                if(isOpen == true)
                {
                    inventoryCanvas.SetActive(false);
                    isOpen = false;
                } 
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

    public void HighlightButton()
    {
        Debug.Log("HighlightButton() executed");


        StartCoroutine(WaitForSec());
        
    }

    IEnumerator WaitForSec()
    {
        Debug.Log("WaitForSec() executed");

        invButton.interactable = false;
        // invIcon.transform.localScale = new Vector3(1.7f, 1.7f, 1);

        StartCoroutine(RotateIcon());

        yield return new WaitForSeconds(0.5f);

        invIcon.transform.rotation = Quaternion.identity; // reset the rotation 
        invButton.interactable = true;
        // invIcon.transform.localScale = new Vector3(1.5f, 1.5f, 1);
    }

    IEnumerator RotateIcon()
    {
        float timer = 0f;
        while (timer <= maxRotationTimer)
        {
            if(timer < maxRotationTimer/2)
            {
                invIcon.transform.Rotate(new Vector3(0, 0, -20) * 2 * Time.deltaTime);
            }
            else
            {
                invIcon.transform.Rotate(new Vector3(0, 0, 20) * 2 * Time.deltaTime);
            }
            timer += Time.deltaTime;
            yield return null;
        }
    }
}
