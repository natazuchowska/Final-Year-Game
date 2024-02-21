using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
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

    // public AudioSource audioSource; // to play pickup sound on item pickup

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Inventory>();
        pickUpItem = this.gameObject;

        pickUpButton = pickUpItem.GetComponent<Button>(); // get the button of the collectible object
    }

    // add to inventory on mouse CLICK
    private void OnMouseDown()
    {
        var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));

        if (!rayHit.collider) { return; }

        // Debug.Log(rayHit.collider.gameObject.name); //display the name of the clicked object on the console

        Vector3 itemScale = rayHit.transform.localScale;

        // if collectible add to inventory on click
        if (rayHit.collider.gameObject.CompareTag("Collectible") || rayHit.collider.gameObject.CompareTag("KeyReward") || rayHit.collider.gameObject.CompareTag("Bottle"))
        {
            Debug.Log("item added to inventory NOW");
            // rayHit.collider.gameObject.GetComponent<Renderer>().material.color = Color.red;

            gm = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();


            // CHECK WHICH BOTTLE WAS COLLECTED AND MARK THE APPROPRIATE FLAG IN GM --------------------------------------
            if (rayHit.collider.gameObject.name == "bottle1")
            {
                gm.bottle1 = true; //say that bottle has been collected so should not be rendered with next new scene load
            }
            if (rayHit.collider.gameObject.name == "bottle2")
            {
                gm.bottle2 = true; //say that bottle has been collected so should not be rendered with next new scene load
            }
            if (rayHit.collider.gameObject.name == "bottle3")
            {
                gm.bottle3 = true; //say that bottle has been collected so should not be rendered with next new scene load
            }
            if (rayHit.collider.gameObject.name == "bottle4")
            {
                gm.bottle4 = true; //say that bottle has been collected so should not be rendered with next new scene load
            }
            if (rayHit.collider.gameObject.name == "bottle5")
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
                Debug.Log("inventory size: " + inventory.slots.Length); 

                if (inventory.isFull[i] == false) // if the slot empty add the item to it
                {
                    // ISSUE --> when item dropped and trying to pick up again - Null Reference -> AudioSource is not initialized eith an item -> how to do it?
                    // audioSource.Play(); // play pickup sound

                    inventory.isFull[i] = true; //set slot to full now

                    Debug.Log("adding item to slot " + i);

                    GameObject inventoryObj = Instantiate(itemButton, inventory.slots[i].transform); // make sure the button spawns at the same position as the graphic (instantiate as child of that slot)
                    inventoryObj.AddComponent<Draggable>(); // add a draggable script to the object so it can be used by dragging out of inventory
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }

    private void OnMouseEnter()
    {
        // Debug.Log("Mouse over interactable object!!!!");

        if (gameObject.CompareTag("Collectible"))
        {
            // CHANGE CURSON ICON HERE (to show that obj is collectible)
            gameObject.GetComponent<Renderer>().material.color = Color.green;
            // --------------------------------------------------------------
            
        }
    }

    private void OnMouseExit()
    {
        // Debug.Log("Mouse NOT over interactable object ANYMORE!!!!");

        if (gameObject.CompareTag("Collectible"))
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
