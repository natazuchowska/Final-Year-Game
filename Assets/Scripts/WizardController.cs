using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class WizardController : MonoBehaviour
{
    /*public GameObject uiObject;*/
    GameObject plant;


    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit the character");
            /*uiObject.SetActive(true);*/
        }

        if(col.gameObject.CompareTag("Plant"))
        {
            // goToRRButton.SetActive(true); // make the navigation visible now
            
        }
    }
   /* IEnumerator WaitForSec() // wait for 4 seconds and then stop displaying the message
    {
        yield return new WaitForSeconds(4);
        uiObject.SetActive(false);
    }
*/
}
