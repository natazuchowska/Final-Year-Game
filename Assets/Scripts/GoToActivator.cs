using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GoToActivator : MonoBehaviour
{
    [SerializeField] public Button goToButton;

    [SerializeField] GameObject bgAfter;
    [SerializeField] GameObject bgBefore;

    [SerializeField] GameObject thankYouMessage;

    int flag = 0;
    int sceneID;

    // hide goToButtons by default
    private void Awake()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex; // get the id of the scene

        if (bgAfter != null && sceneID == 3) // initial scene
        {
            bgAfter.SetActive(false);
            // thankYouMessage = GameObject.Find("thank_you");

            thankYouMessage.SetActive(false);
        }
        
    }

    private void Update()
    {
        sceneID = SceneManager.GetActiveScene().buildIndex; // get the id of the scene

        if(sceneID == 3) // main scene
        {
            if (GameManager.plantGiven == true && flag == 0)
            {
                bgAfter.SetActive(true);
                bgBefore.SetActive(false);

                thankYouMessage.SetActive(true);

                flag = 1;
            }
        }
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(sceneID == 3) // additional plant condition to check
        {
            // show buttons when player goes into a certain area (collides with button collider)
            if (collision.CompareTag("Player") && GameManager.plantGiven == true)
            {
                goToButton.interactable = true; // enable navigation when plant given to wizard
            }
        }
        else
        {
           if (collision.CompareTag("Player"))
           {
                goToButton.interactable = true; // enable navigation when plant given to wizard
           }
           else
            {
                goToButton.interactable = false;
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(sceneID == 3)
        {
            if (goToButton != null && GameManager.plantGiven == true)
            {
                goToButton.interactable = false;
            }
        }
        else
        {
            if (goToButton != null)
            {
                goToButton.interactable = false;
            }
        }
        
    }
}
