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

  /*  public void OnClick(InputAction.CallbackContext context)
    {
        if (!context.started) { return; }

        var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));

        if (!rayHit.collider) { return; }

        // Debug.Log(rayHit.collider.gameObject.name); //display the name of the clicked object on the console

        Vector3 itemScale = rayHit.transform.localScale;

        // if collectible add to inventory on click
        if (rayHit.collider.gameObject.CompareTag("Collectible"))
        {
            Debug.Log("item added to inventory NOW");
            rayHit.collider.gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
    }*/

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            for (int i = 0; i < inventory.slots.Length; i++)
            {
                if (inventory.isFull[i] == false) // if the slot empty add the item to it
                {
                    // ISSUE --> when item dropped and trying to pick up again - Null Reference -> AudioSource is not initialized eith an item -> how to do it?
                    // audioSource.Play(); // play pickup sound

                    inventory.isFull[i] = true; //set slot to full now
                    Instantiate(itemButton, inventory.slots[i].transform); // make sure the button spawns at the same position as the graphic (instantiate as child of that slot)
                    Destroy(gameObject);
                    break;
                }
            }
        }
    }

    public void AddToInventory()
    {
        for (int i = 0; i < inventory.slots.Length; i++)
        {
            if (inventory.isFull[i] == false) // if the slot empty add the item to it
            {
                // ISSUE --> when item dropped and trying to pick up again - Null Reference -> AudioSource is not initialized eith an item -> how to do it?
                // audioSource.Play(); // play pickup sound

                inventory.isFull[i] = true; //set slot to full now
                Instantiate(itemButton, inventory.slots[i].transform); // make sure the button spawns at the same position as the graphic (instantiate as child of that slot)
                Destroy(gameObject);
                break;
            }
        }
    }

    private void OnMouseEnter()
    {
        // Debug.Log("Mouse over interactable object!!!!");

        if (gameObject.CompareTag("Collectible"))
        {
            /*gameObject.GetComponent<Renderer>().material.color = Color.green;*/
            // pickUpButton.onClick.AddListener(TaskOnClick); // add click listener
        }
    }

    // todo when button clicked 
    private void TaskOnClick()
    {
        Debug.Log("object to be added to inventory NOW");
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
