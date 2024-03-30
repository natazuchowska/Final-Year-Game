using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NavigationControllerSingle : MonoBehaviour
{
    [SerializeField] public Button goToRoom;
    [SerializeField] GameObject goToText;

    private bool playerInArea = false;

    // round room id: 1
    // glasshouse gf id: 6

    int sceneID; // to check scene

    // Start is called before the first frame update
    void Start()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex; // get the id of the scene

        goToRoom.interactable = false;

        if(goToText != null) // if theres a text to an arrow
        {
            goToText.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerInArea) // door now open so go straight to the next room
        {
            goToRoom.interactable = true;

            if (goToText != null) // if theres a text to an arrow
            {
                goToText.SetActive(true);
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

            if(goToRoom != null)
            {
                goToRoom.interactable = false;
            }

            if (goToText != null) // if theres a text to an arrow
            {
                goToText.SetActive(false);
            }
        }
    }
}
