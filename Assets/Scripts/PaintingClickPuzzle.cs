using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PaintingClickPuzzle : MonoBehaviour
{
    // general script for checking for the correct order od clicks

    [SerializeField] public List<Button> paints;
    [SerializeField] public List<GameObject> texts;

    GameObject keyReward; // key to get when puzzle solved

    public static int[] correctOrder = { 6, 5, 3, 4, 2, 1 }; //the order in which paintings should be clicked

    // ------------ BACKGROUNDS -------------------------
    [SerializeField] GameObject paintV1;
    [SerializeField] GameObject paintV2;
    [SerializeField] GameObject paintV3;
    [SerializeField] GameObject paintV4;
    [SerializeField] GameObject paintV5;
    // --------------------------------------------------

    [SerializeField] public int[] dialogueOrder; // get order of dialogue from 1st scene

    public static int howManyPaintings;
    public static int howManyCorrectSoFar = 0; // how many have been clicked ok so far

    public static bool puzzleSolved = false;
    public AudioSource audioPlayer; // to play rewarding sound when puzzle solved
    public static bool soundPlayed = false;
    private int puzzleFlag = 0;

    public static int paintPuzzleID; // get the order_id of the painting being clicked from the singular paintings cript (WAS STATIC)


    // Start is called before the first frame update
    void Start()
    {
        dialogueOrder = GameObject.Find("GameManager").GetComponent<DialogueOrderManager>().getFinalOrder(); // get the array from dialogueOrderManager script

        // get BACKGROUNDS
        /*ogBackground = GameObject.Find("paintings_puzzle");
        variant1Background = GameObject.Find("paintingsVariant");*/

        // hide all painting variants by default
        paintV1.SetActive(false);
        paintV2.SetActive(false);
        paintV3.SetActive(false);
        paintV4.SetActive(false);
        paintV5.SetActive(false);

        string correctStr = "";
        for(int i=0; i<6; i++)
        {
            correctStr += correctOrder[i].ToString();
            correctStr += ", ";
        }
        Debug.Log("correct puzzle order is: " + correctStr);

        howManyPaintings = paints.Count;

        keyReward = GameObject.FindGameObjectWithTag("KeyReward"); // get reference to the key sprite
        keyReward.SetActive(false); // hide key until puzzle not solved

        correctOrder = decideOrder();
    }

    private int[] decideOrder()
    {

        if (dialogueOrder[1] == 1)
        {
            for(int i=0; i<6; i++)
            {
                correctOrder[i] = i+1;
            }

            // activate the appropriate graphic
            paintV2.SetActive(true);

            Debug.Log("order version 1");
        }
        else if (dialogueOrder[1] == 2)
        {
            for (int i = 0; i < 6; i++)
            {
                correctOrder[(i+2) % 6] = i+1;
            }

            // activate the appropriate graphic
            paintV3.SetActive(true);

            Debug.Log("order version 2");
        }
        else if (dialogueOrder[1] == 3)
        {
            for (int i = 0; i < 6; i++)
            {
                correctOrder[(i+4) % 6] = i+1;
            }

            // activate the appropriate graphic
            paintV4.SetActive(true);

            Debug.Log("order version 3");
        }
        else if (dialogueOrder[1] == 4)
        {
            for (int i = 0; i < 6; i++)
            {
                correctOrder[(i+3) % 6] = i+1;
            }

            // activate the appropriate graphic
            paintV5.SetActive(true);

            Debug.Log("order version 4");
        }
        else
        {
            // in case dialogue hs not been attempted at all
            paintV1.SetActive(true);
        }

        Debug.Log("start click chain from painting nr " + correctOrder[0]);
        return correctOrder;
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

        if(puzzleSolved == false)
        {
            if (paintPuzzleID == correctOrder[howManyCorrectSoFar])
            {
                howManyCorrectSoFar++; // go to next in order
            }
            else
            {
                howManyCorrectSoFar = 0; // restart
            }

            if (howManyCorrectSoFar == howManyPaintings)
            {
                Debug.Log("PUZZLE SOLVED!!!!!");
                puzzleSolved = true;

                GameObject.Find("GameManager").GetComponent<GameManager>().markAsSolved(0); // mark appropriate puzzle flag in game mngr as solved
            }
        }
       
    }
}
