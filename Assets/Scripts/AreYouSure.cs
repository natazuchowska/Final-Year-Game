using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AreYouSure : MonoBehaviour
{
    [SerializeField] GameObject areYouSurePanel; // screen to display to ask if a user rly wants to return to main menu

    // Start is called before the first frame update
    void Start()
    {
        areYouSurePanel = GameObject.FindGameObjectWithTag("RUSure"); // get ref to object
        closeScreen();
    }

    public void showScreen()
    {
        // when MENU button is clicked
        areYouSurePanel.SetActive(true);
    }

    public void closeScreen()
    {
        // when NO button is clicked
        areYouSurePanel.SetActive(false);
    }
}
