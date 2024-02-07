using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PaintingClickPuzzle : MonoBehaviour
{
    // general script for checking for the correct order od clicks

    [SerializeField] public List<Button> paints;
    [SerializeField] public List<GameObject> texts;

    public static int[] correctOrder = { 6, 3, 4, 2, 5, 1 }; //the order in which paintings should be clicked
    public static int howManyPaintings;
    public static int howManyCorrectSoFar = 0; // how many have been clicked ok so far

    public static int paintPuzzleID; // get the order_id of the painting being clicked from the singular paintings cript

    /*[SerializeField] public Button paint1;
    [SerializeField] public Button paint2;
    [SerializeField] public Button paint3;
    [SerializeField] public Button paint4;
    [SerializeField] public Button paint5;
    [SerializeField] public Button paint6;*/

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(correctOrder[1]); //3

        howManyPaintings = paints.Count;
    }

    public static void setPaintOrderID(int paintID)
    {
        paintPuzzleID = paintID;
        Debug.Log("this painting has order id of: " + paintPuzzleID);

        if(paintPuzzleID == correctOrder[howManyCorrectSoFar])
        {
            howManyCorrectSoFar++; // go to next in order
        }
        else
        {
            howManyCorrectSoFar = 0; // restart
        }

        if(howManyCorrectSoFar == howManyPaintings)
        {
            Debug.Log("PUZZLE SOLVED!!!!!");
        }
    }
}
