using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] private GameObject inventoryCanvas;
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
        invButton.onClick.AddListener(OpenInventory);

        optionsCanvas = GameObject.Find("OptionsCanvas");
    }

    private void Start()
    {
        isOpen = false;
        inventoryCanvas.SetActive(false);
    }


    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("OnSceneLoaded called");
        isOpen = false;
        inventoryCanvas.SetActive(false);
    }

    private void Update()
    {
        // only do this if there is a player sprite in the current scene
        if (player.activeInHierarchy == true)
        {
            playerIsMoving = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>().isMoving; // check if player is moving BUILD VERSION

            if (playerIsMoving == true)
            {
                if(isOpen == true)
                {
                    inventoryCanvas.SetActive(false);
                    isOpen = false;
                    Debug.Log("player moving case of inventory caled");
                } 
            }

        }

        gamePaused = GameObject.FindGameObjectWithTag("GameManager").GetComponent<PauseController>().isPaused; // get value if game paused or not
        // if paused -> close the inventory

        if(gamePaused == true)
        {
            inventoryCanvas.SetActive(false);
            isOpen = false;

            Debug.Log("game paused case of inventory called");
        }
    }

    public void OpenInventory()
    {
        Debug.Log("called OpenInventory() method by object: " + this.gameObject.name);

        // if this is a scene where there is an inventory
        if(inventoryCanvas != null)
        {
            if (!isOpen)
            {
                Debug.Log("isOpen false if else called");
                inventoryCanvas.SetActive(true); // open on first click if closed
            }
            else
            {
                Debug.Log("isOpen true if else called");
                inventoryCanvas.SetActive(false); // close on second click if open
            }

            isOpen = !isOpen;
            Debug.Log("is inventory canvas active? : " + inventoryCanvas.activeInHierarchy);
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
