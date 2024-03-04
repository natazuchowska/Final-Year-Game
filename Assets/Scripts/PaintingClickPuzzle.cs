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

    public static int[] correctOrder = { -1, -1, -1, -1, -1, -1 }; //the order in which paintings should be clicked

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

        correctOrder = decideOrder();

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
    }

    private int[] decideOrder()
    {
        if (dialogueOrder[1] == 1)
        {
            for(int i=0; i<6; i++)
            {
                correctOrder[i] = i+1;
            }
            Debug.Log("order version 1");
        }
        else if (dialogueOrder[1] == 2)
        {
            for (int i = 0; i < 6; i++)
            {
                correctOrder[(i+2) % 6] = i+1;
            }
            Debug.Log("order version 2");
        }
        else if (dialogueOrder[1] == 3)
        {
            for (int i = 0; i < 6; i++)
            {
                correctOrder[(i+4) % 6] = i+1;
            }
            Debug.Log("order version 3");
        }
        else if (dialogueOrder[1] == 4)
        {
            for (int i = 0; i < 6; i++)
            {
                correctOrder[(i+3) % 6] = i+1;
            }
            Debug.Log("order version 4");
        }

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
