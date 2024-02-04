using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class DoorNavigation : MonoBehaviour
{
    [SerializeField] public Button goBackButton;
    [SerializeField] public Button goThruDoorButton;
    GameObject goThruDoorText;

    private bool canGo; // check if door unclocked to go through

    // hide goToButtons by default
    private void Awake()
    {
        goBackButton.interactable = true;
        goThruDoorButton.interactable = false;

        goThruDoorText = GameObject.FindGameObjectWithTag("GoThruText"); // get the reference to the text object
        goThruDoorText.SetActive(false); // hide text

        canGo = KeySnapController.canGoThru1; // get the value of canGoThru fom KeySnapController
        Debug.Log(canGo);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Key"))
        {   
            goBackButton.interactable = false; // can't go back to the room
            goThruDoorButton.interactable = true; // can go to the next room
            goThruDoorText.SetActive(true); // show text by altering opacity

            Debug.Log("can go thru now");
        }
    }
}
