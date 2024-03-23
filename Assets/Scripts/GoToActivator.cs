using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class GoToActivator : MonoBehaviour
{
    [SerializeField] public Button goToButton;

    [SerializeField] GameObject bgAfter;
    [SerializeField] GameObject bgBefore;

    int flag = 0;

    // hide goToButtons by default
    private void Awake()
    {
        bgAfter.SetActive(false);
    }

    private void Update()
    {
        if(GameManager.plantGiven == true && flag == 0)
        {
            bgAfter.SetActive(true);
            bgBefore.SetActive(false);
            flag = 1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // show buttons when player goes into a certain area (collides with button collider)
        if (collision.CompareTag("Player") && GameManager.plantGiven == true)
        {
            // goToButton.gameObject.SetActive(true); // show button upon collision with a player
            goToButton.interactable = true; // enable navigation when plant given to wizard
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(goToButton != null && GameManager.plantGiven == true)
        {
           goToButton.interactable = false;
        }
    }
}
