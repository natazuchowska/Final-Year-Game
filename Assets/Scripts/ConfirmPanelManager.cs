using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ConfirmPanelManager : MonoBehaviour
{
    private GameObject confirmPanel;
    private bool alreadyDisplayed = false;

    private void Start()
    {
        confirmPanel = GameObject.Find("confirmPanel"); // get reference to the panel to display
        confirmPanel.SetActive(false); // hide panel by default
    }

    public void DisplayPanel()
    {
        // if one inserted but not both 
        StartCoroutine(DisplayForSec());
    }

    IEnumerator DisplayForSec()
    {
        yield return new WaitForSeconds(1.5f); // wait for the animation of turning the key to finish executing
        confirmPanel.SetActive(true);
        yield return new WaitForSeconds(2);
        confirmPanel.SetActive(false);
    }
}

