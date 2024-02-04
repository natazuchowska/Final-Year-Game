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

    // round room id: 1
    // glasshouse gf id: 6

    int sceneID; // to check scene

    // Start is called before the first frame update
    void Start()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex; // get the id of the scene

        if(sceneID == 1) // door1 - to glasshouse
        {
            doorOpen = KeySnapController.canGoThru1; // can go thru door? (static var controlled by door script)
        }
        if(sceneID == 6) // door2 - to basement
        {
            doorOpen = KeySnapController.canGoThru2; // can go thru door? (static var controlled by door script)
        }

        goThruDoor.interactable = true; // need to go to door and unlock
        goToRoom.interactable = false; // can't go thru (door locked)

        Debug.Log(doorOpen);

        if (doorOpen == true)
        {
            // door now open so go straight to the next room
            goThruDoor.interactable = false;
            goToRoom.interactable = true;
        }
    }
}
