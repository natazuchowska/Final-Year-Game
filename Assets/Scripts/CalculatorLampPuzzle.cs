using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class CalculatorLampPuzzle : MonoBehaviour
{
    // general script for checking for the correct order of clicks

    public static bool lightOn = true; // is lamp light in swimming pool turned on(?)

    bool electricityFlow;

    public int[] correctOrder; // = { 8, 1, 0, 5, 9}; //the order in which calculator buttons should be clicked, 9 is R button for confirmation

    // ------------ BACKGROUNDS -------------------------
    [SerializeField] GameObject beforeBackground;
    [SerializeField] GameObject afterLampOnBackground;
    [SerializeField] GameObject afterLampOffBackground;

    // ------------ BUTTONS -----------------------------
    [SerializeField] List<Button> calculatorButtons; // referenced in the inspector

    public static int howManyCorrectSoFar = 0; // how many have been clicked ok so far

    public static bool puzzleSolved = false;
    public AudioSource audioPlayer; // to play rewarding sound when puzzle solved
    public static bool soundPlayed = false;
    private static int puzzleFlag = 0;

    public static int paintPuzzleID; // get the order_id of the painting being clicked from the singular paintings cript (WAS STATIC)


    // Start is called before the first frame update
    void Start()
    {
        correctOrder = FishNumber.GetCorrectOrder();

        // hide the backgrounds of open box
        afterLampOnBackground.SetActive(false);
        afterLampOffBackground.SetActive(false);

        electricityFlow = SnapController.electricityFlow; // get the electricity flow value from snapcontroller

        if(electricityFlow == true)
        {
            if(!puzzleSolved)
            {
                afterLampOnBackground.SetActive(true);
            }
            else
            {
                afterLampOffBackground.SetActive(true);
            }
            beforeBackground.SetActive(false);
        }
    }

    private void Update()
    {
        if(electricityFlow == true)
        {
            if(!puzzleSolved)
            {
                afterLampOnBackground.SetActive(true);
            }
            else
            {
                if(puzzleFlag == 0)
                {
                    afterLampOffBackground.SetActive(true);
                    puzzleFlag = 1; // to execute only once

                    // play sound only once
                    if (soundPlayed == false)
                    {
                        PlaySound();
                        soundPlayed = true;
                    }
                }   
            }
            beforeBackground.SetActive(false);
        }
    }

    public void PlaySound()
    {
        this.audioPlayer.Play();
    }

    public void setClickOrderID(Button btn)
    {
        // Debug.Log("correct order of buttons in calclampscript is: " + correctOrder[0] + " " + correctOrder[1] + " " + correctOrder[2] + " " + correctOrder[3] + " " + correctOrder[4]);

        if(puzzleSolved == false)
        {
/*          Debug.Log("index of button in calcButtons list: " + calculatorButtons.IndexOf(btn));
            Debug.Log("value in correctOrder array: " + correctOrder[howManyCorrectSoFar]);*/
            if (calculatorButtons.IndexOf(btn) == correctOrder[howManyCorrectSoFar])
            {
                howManyCorrectSoFar++; // go to next in order
            }
            else
            {
                howManyCorrectSoFar = 0; // restart
            }

            if (howManyCorrectSoFar == 5) // need to click 4 buttons and accept with RED
            {
                puzzleSolved = true;
                lightOn = false; // inform to turn off the lamp light sprite in swimming pool

                GameObject.Find("GameManager").GetComponent<GameManager>().markAsSolved(2); // mark appropriate puzzle flag in game mngr as solved
            }
        }
       
    }
}
