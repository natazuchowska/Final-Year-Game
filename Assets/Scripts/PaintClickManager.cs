using System;
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
    /*[SerializeField] public GameObject text;*/

    [SerializeField] PaintingClickPuzzle pcp;
    List<int> correctOrder;



    // Start is called before the first frame update
    void Start()
    {
        paintButton.onClick.AddListener(TaskOnClick); // add click listener
        // text.SetActive(false); //hide displaying text

    }

    // todo when button clicked 
    private void TaskOnClick()
    {
        // Debug.Log("displaying text of painting ");
        Debug.Log("ID in puzzle: " + paintID);

        pcp = GameObject.Find("PaintStoryManager").GetComponent<PaintingClickPuzzle>();
        correctOrder = GameObject.Find("PaintStoryManager").GetComponent<PaintingClickPuzzle>().correctOrder; // get the correct paintings order from the other script

        StartCoroutine(WaitForSec());

        pcp.setPaintOrderID(paintID); // inform/pass the order id to general script
    }

    IEnumerator WaitForSec()
    {
        // make texts visible for 4 seconds only and then hide again
        Debug.Log("Coroutine started");
        // text.SetActive(true);
        GameObject.Find("PaintStoryManager").GetComponent<PaintingClickPuzzle>().paintAudio[correctOrder.IndexOf(paintID)].Play(); // play the correct audio
        yield return new WaitForSeconds(4);
        // text.SetActive(false);
        GameObject.Find("PaintStoryManager").GetComponent<PaintingClickPuzzle>().paintAudio[correctOrder.IndexOf(paintID)].Pause(); // pause the correct audio
        // GameObject.Find("PaintStoryManager").GetComponent<PaintingClickPuzzle>().paintAudio[Array.IndexOf(correctOrder, paintID)].Pause(); // pause the correct audio

    }
}
