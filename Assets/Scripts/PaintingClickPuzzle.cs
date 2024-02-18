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

    GameObject keyReward; // key to get when puzzle solved

    public static int[] correctOrder = { 6, 3, 4, 2, 5, 1 }; //the order in which paintings should be clicked
    public static int howManyPaintings;
    public static int howManyCorrectSoFar = 0; // how many have been clicked ok so far

    public static bool puzzleSolved = false;
    public AudioSource audioPlayer; // to play rewarding sound when puzzle solved
    public static bool soundPlayed = false;
    private int puzzleFlag = 0;

    public static int paintPuzzleID; // get the order_id of the painting being clicked from the singular paintings cript

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(correctOrder[1]); //3

        howManyPaintings = paints.Count;

        keyReward = GameObject.FindGameObjectWithTag("KeyReward"); // get reference to the key sprite
        keyReward.SetActive(false); // hide key until puzzle not solved
    }

    private void Update()
    {
        if(puzzleSolved == true && puzzleFlag == 0)
        {
            keyReward.SetActive(true);
            puzzleFlag = 1; // to execute only once

            // play sound only once
            if(soundPlayed == false)
            {
                PlaySound();
                soundPlayed = true;
            }
        }
    }

    public void PlaySound()
    {
        this.audioPlayer.Play();
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
            puzzleSolved = true;
        }
    }
}
