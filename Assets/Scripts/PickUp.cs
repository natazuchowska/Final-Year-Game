using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.InputSystem;

public class PickUp : MonoBehaviour
{
    private Inventory inventory;
    public GameObject itemButton;

    // public AudioSource audioSource; // to play pickup sound on item pickup

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<Inventory>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player"))
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

    private void OnMouseEnter()
    {
        Debug.Log("Mouse over interactable object!!!!");

        if (gameObject.CompareTag("Collectible"))
        {
            gameObject.GetComponent<Renderer>().material.color = Color.green;
        }
    }

    private void OnMouseExit()
    {
        Debug.Log("Mouse NOT over interactable object ANYMORE!!!!");

        if (gameObject.CompareTag("Collectible"))
        {
            gameObject.GetComponent<Renderer>().material.color = Color.white;
        }
    }
}
