using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WizardController : MonoBehaviour
{
    /*public GameObject uiObject;*/

    void OnCollisionEnter2D(Collision2D col)
    {
        if (col.gameObject.CompareTag("Player"))
        {
            Debug.Log("Hit the character");
            /*uiObject.SetActive(true);*/
        }

        if(col.gameObject.CompareTag("Plant"))
        {
            Debug.Log("THANK YOU FOR THE PLANT!");
        }
    }
   /* IEnumerator WaitForSec() // wait for 4 seconds and then stop displaying the message
    {
        yield return new WaitForSeconds(4);
        uiObject.SetActive(false);
    }
*/
}
