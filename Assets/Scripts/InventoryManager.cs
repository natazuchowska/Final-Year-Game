using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InventoryManager : MonoBehaviour
{
    private GameObject inventoryCanvas;
    private bool isOpen = false;

    private void Start()
    {
        inventoryCanvas = GameObject.FindGameObjectWithTag("Inventory"); // find the reference to inventory canvas
        inventoryCanvas.SetActive(false); // hide inentory by default
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
