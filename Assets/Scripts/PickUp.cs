using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class PickUp : MonoBehaviour
{
    private Inventory inventory;
    public GameObject itemButton;

    private Camera mainCamera;

    public GameObject pickUpItem;
    public Button pickUpButton; // button of the thing being picked up

    private GameManager gm; // reference to gamemanager script

    [SerializeField] private InventoryManager invMngr;

    public AudioSource pickUpAudio; // to play pickup sound on item pickup

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {

        inventory = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Inventory>();
        pickUpItem = this.gameObject;

        pickUpButton = pickUpItem.GetComponent<Button>(); // get the button of the collectible object
        pickUpAudio = GameObject.Find("PickupAudio").GetComponent<AudioSource>();

        invMngr = GameObject.Find("InventoryButton").GetComponent<InventoryManager>(); // mngr script attached to inventory icon
    }


    // add to inventory on mouse CLICK
    private void OnMouseDown()
    {
        if (PauseController.isPaused) { return; } // do nothing if game paused

        var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));

        if (!rayHit.collider) { return; }

        Vector3 itemScale = rayHit.transform.localScale;

        // if collectible add to inventory on click
        if (rayHit.collider.gameObject.CompareTag("Collectible") || rayHit.collider.gameObject.CompareTag("KeyReward") || rayHit.collider.gameObject.CompareTag("Bottle"))
        {
            Debug.Log("item added to inventory NOW");

            gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();


            // CHECK WHICH BOTTLE WAS COLLECTED AND MARK THE APPROPRIATE FLAG IN GM --------------------------------------
            if (rayHit.collider.gameObject.name == "middleBottle")
            {
                gm.bottle1 = true; //say that bottle has been collected so should not be rendered with next new scene load
            }
            if (rayHit.collider.gameObject.name == "bottle2")
            {
                gm.bottle2 = true; //say that bottle has been collected so should not be rendered with next new scene load
            }
            if (rayHit.collider.gameObject.name == "leftBottle")
            {
                gm.bottle3 = true; //say that bottle has been collected so should not be rendered with next new scene load
            }
            if (rayHit.collider.gameObject.name == "bottle4")
            {
                gm.bottle4 = true; //say that bottle has been collected so should not be rendered with next new scene load
            }
            if (rayHit.collider.gameObject.name == "rightBottle")
            {
                gm.bottle5 = true; //say that bottle has been collected so should not be rendered with next new scene load
            }
            if (rayHit.collider.gameObject.name == "bottle6")
            {
                gm.bottle6 = true; //say that bottle has been collected so should not be rendered with next new scene load
            }
            // -----------------------------------------------------------------------------------------------------------

            for (int i = 0; i < inventory.slots.Length; i++)
            {
                // Debug.Log("inventory size: " + inventory.slots.Length); 

                if (inventory.isFull[i] == false) // if the slot empty add the item to it
                {
                    pickUpAudio.Play(); // play pickup sound

                    inventory.isFull[i] = true; //set slot to full now

                    invMngr.HighlightButton(); // highlight the inventory icon

                    // Debug.Log("adding item to slot " + i);

                    GameObject inventoryObj = Instantiate(itemButton, inventory.slots[i].transform); // make sure the button spawns at the same position as the graphic (instantiate as child of that slot)
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }

    private void OnMouseEnter()
    {
        if (PauseController.isPaused) { return; } // do nothing if game paused

        if (gameObject.CompareTag("Collectible") || gameObject.CompareTag("Bottle"))
        {
            // CHANGE CURSON ICON HERE (to show that obj is collectible)
            gameObject.GetComponent<Renderer>().material.color = Color.grey;
            // --------------------------------------------------------------
            
        }
    }

    private void OnMouseExit()
    {
        if (PauseController.isPaused) { return; } // do nothing if game paused

        if (gameObject.CompareTag("Collectible") || gameObject.CompareTag("Bottle"))
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
