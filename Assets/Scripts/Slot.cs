using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Inventory inventory;
    public int i; // slot id

    public AudioSource audioPlayer;
    private GameObject useIcon;

    private ChangeSprite invCanvas;

    public bool slotActiveToUse = false;

    int sceneID;

    [SerializeField] string childInSlot; // for testing

    private void Start()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex; // get the id of current scene

        inventory = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Inventory>();
        invCanvas = GameObject.Find("InventoryCanvas").GetComponent<ChangeSprite>(); // script to change arrows sprites to interactive
    }

    private void Update()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex; // get the id of current scene

        if (transform.childCount <= 0)
        {
            inventory.isFull[i] = false;
        }

        foreach(Transform child in transform)
        {
            childInSlot = child.name;
            CanUseItem(childInSlot);

            // if child dropped
            if(!slotActiveToUse)
            {
                invCanvas.ChangeSpriteDisabled(i);
            }
        }
        
    }

    public void DropItem()
    {
        audioPlayer.Play(); // play respawn sound 

        foreach(Transform child in transform) // for each child in slot
        {
            if(!child.CompareTag("Cross") && slotActiveToUse)
            {
                child.GetComponent<Spawn>().SpawnDroppedItem();
                GameObject.Destroy(child.gameObject);

                child.name = null;
                slotActiveToUse = false;
            }
            
        }
    }

    public void CanUseItem(string childName)
    {
        // glasshouse door
        if (sceneID == 14)
        {
            if (childName.Equals("Key1Button(Clone)"))
            {
                slotActiveToUse = true;
                invCanvas.ChangeSpriteActive(i);
            }
            else
            {
                invCanvas.ChangeSpriteDisabled(i);
                slotActiveToUse = false;
            }
        }

        // bottles puzzle
        if(sceneID == 10)
        {
            if (childName.Equals("LeftBottleButton(Clone)") || childName.Equals("RightBottleButton(Clone)") || childName.Equals("MiddleBottleButton(Clone)") || childName.Equals("Bottle1Button(Clone)") || childName.Equals("Bottle2Button(Clone)") || childName.Equals("Bottle3Button(Clone)") || childName.Equals("Bottle4Button(Clone)") || childName.Equals("Bottle5Button(Clone)") || childName.Equals("Bottle6Button(Clone)"))
            {
                slotActiveToUse = true;
                invCanvas.ChangeSpriteActive(i);
            }
            else
            {
                invCanvas.ChangeSpriteDisabled(i);
                slotActiveToUse = false;
            }
        }

        if(sceneID == 15)
        {
            if(childName.Equals("Key2Button(Clone)") || childName.Equals("Key3Button(Clone)"))
            {
                slotActiveToUse = true;
                invCanvas.ChangeSpriteActive(i);
            }
            else
            {
                invCanvas.ChangeSpriteDisabled(i);
                slotActiveToUse = false;
            }
        }
    }
}
