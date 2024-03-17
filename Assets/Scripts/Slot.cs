using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    private Inventory inventory;
    public int i;

    public AudioSource audioPlayer;

    private void Start()
    {
        inventory = GameObject.FindGameObjectWithTag("GameManager").GetComponent<Inventory>();
        // sceneID = SceneManager.GetActiveScene().buildIndex; // get the id of current scene
    }

    private void Update()
    {
        if(transform.childCount <= 0)
        {
            inventory.isFull[i] = false;
        }
    }

    public void DropItem()
    {
        audioPlayer.Play(); // play respawn sound 

        foreach(Transform child in transform) // for each child in slot
        {
            
            if(!child.CompareTag("Cross")) // get the item in slot, not the use icon
            {
                child.GetComponent<Spawn>().SpawnDroppedItem();
                GameObject.Destroy(child.gameObject);
            }
            
        }
    }
}
