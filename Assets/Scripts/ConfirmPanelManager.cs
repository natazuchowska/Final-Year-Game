using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfirmPanelManager : MonoBehaviour
{
    private GameObject confirmPanel;
    private bool alreadyDisplayed = false;


    private void Start()
    {
        confirmPanel = GameObject.Find("confirmPanel"); // get reference to the panel to display
        confirmPanel.SetActive(false); // hide panel by default


    }

    private void Update()
    {
        // if one inserted but not both 
        if((SnapController.keySlot2 == true || SnapController.keySlot3 == true && !(SnapController.keySlot2 == true && SnapController.keySlot3 == true)) && !alreadyDisplayed)
        {
            StartCoroutine(DisplayForSec());
            alreadyDisplayed = true;
        }
    }

    IEnumerator DisplayForSec()
    {
        yield return new WaitForSeconds(1.5f); // wait for the animation of turning the key to finish executing
        confirmPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmPanel.SetActive(false);
    }
}

