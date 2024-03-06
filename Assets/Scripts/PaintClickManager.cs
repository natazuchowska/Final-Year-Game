using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaintClickManager : MonoBehaviour
{
    // get reference to the correct painting
    [SerializeField] public Button paintButton;

    /*[SerializeField] public GameObject paint;*/

    [SerializeField] int paintID; // the paintings id

    // get reference to texts displayed by paintings
    [SerializeField] public GameObject text;



    // Start is called before the first frame update
    void Start()
    {
        paintButton.onClick.AddListener(TaskOnClick); // add click listener
        text.SetActive(false); //hide displaying text

    }

    // todo when button clicked 
    private void TaskOnClick()
    {
        Debug.Log("displaying text of painting ");
        Debug.Log("ID in puzzle: " + paintID);

        
        StartCoroutine(WaitForSec());
        

        // display paintings text for 6 seconds and then hide again
        // text.SetActive(true);
        // StartCoroutine("WaitForSec");

        // if puzzle has already been solved (key shown) do not call method again

        PaintingClickPuzzle.setPaintOrderID(paintID); // inform/pass the order id to general script
        
    }

    IEnumerator WaitForSec()
    {
        // make texts visible for 4 seconds only and then hide again
        Debug.Log("Coroutine started");
        text.SetActive(true);
        yield return new WaitForSeconds(4);
        text.SetActive(false);
    }
}
