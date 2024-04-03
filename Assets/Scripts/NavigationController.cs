using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NavigationController : MonoBehaviour
{
    bool doorOpen; // check if path to the next room is unlocked (glasshouse)

    // nav buttons
    [SerializeField] public Button goThruDoor;
    [SerializeField] public Button goToRoom;

    private bool playerInArea = false;

    [SerializeField] GameObject goToText;

    // round room id: 1
    // glasshouse gf id: 6

    int sceneID; // to check scene

    // Start is called before the first frame update
    void Start()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex; // get the id of the scene

        if(sceneID == 1) // door1 - to glasshouse
        {
            doorOpen = SnapController.canGoThru1; // can go thru door? (static var controlled by door script)
        }
        if(sceneID == 6) // door2 - to basement
        {
            doorOpen = SnapController.canGoThru2; // can go thru door? (static var controlled by door script)
        }

        /*goThruDoor.interactable = true; // need to go to door and unlock
        goToRoom.interactable = false; // can't go thru (door locked)*/

        // initially hide both as player far away
        goThruDoor.interactable = false;
        goToRoom.interactable = false;

        Debug.Log(doorOpen);

        if(doorOpen == null) // if no door then same as door open
        {
            doorOpen = true;
        }

        if (goToText != null) // if theres a text to an arrow
        {
            goToText.SetActive(false);
        }
    }

    private void Update()
    {
        if(playerInArea)
        {
            if (goToText != null) // if theres a text to an arrow
            {
                goToText.SetActive(true);
            }

            if(doorOpen) // door now open so go straight to the next room
            {
                goToRoom.interactable = true;
            }
            else if(!doorOpen)  // take player to the door scene
            {
                goThruDoor.interactable = true;
            }
        }
        else
        {
            if (goToText != null) // if theres a text to an arrow
            {
                goToText.SetActive(false);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            playerInArea = true;
        }
        else
        {
            playerInArea = false;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(collision.CompareTag("Player"))
        {
            playerInArea = false;
            
            if (goToRoom != null)
            {
                goToRoom.interactable = false;
            }

            if (goThruDoor != null)
            {
                goThruDoor.interactable = false;
            }
        }
        
    }
}
