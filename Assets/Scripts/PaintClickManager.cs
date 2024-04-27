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

    [SerializeField] int paintID; // the paintings id


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
        // Debug.Log("ID in puzzle: " + paintID);

        pcp = GameObject.Find("PaintStoryManager").GetComponent<PaintingClickPuzzle>();
        correctOrder = GameObject.Find("PaintStoryManager").GetComponent<PaintingClickPuzzle>().correctOrder; // get the correct paintings order from the other script

        StartCoroutine(WaitForSec());

        pcp.setPaintOrderID(paintID); // inform/pass the order id to general script
    }

    IEnumerator WaitForSec()
    {
        // Debug.Log("Coroutine started");
        GameObject.Find("PaintStoryManager").GetComponent<PaintingClickPuzzle>().paintAudio[correctOrder.IndexOf(paintID)].Play(); // play the correct audio
        yield return new WaitForSeconds(5); // was 4
        GameObject.Find("PaintStoryManager").GetComponent<PaintingClickPuzzle>().paintAudio[correctOrder.IndexOf(paintID)].Pause(); // pause the correct audio
        // GameObject.Find("PaintStoryManager").GetComponent<PaintingClickPuzzle>().paintAudio[Array.IndexOf(correctOrder, paintID)].Pause(); // pause the correct audio

    }
}
