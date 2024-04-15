using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Timeline;

public class DialogueOrderManager : MonoBehaviour
{
    private int count = 0;
    [SerializeField] private List<int> dialogueOrder;
    // public static List<int> dialogueOrder = new List<int>(); // to store order of chosen dialogue topics
    public static bool[] chosenSoFar = {false, false, false, false, false}; // flag dialogue options which have already been chosen
    string dialogueOrderSt;

    public void RecordResponse(int ID)
    {
        if(count<dialogueOrder.Count)
        {
            dialogueOrder.Add(ID); // apend to the list of chosen topics
            // count++;
        }

        //dialogueOrder.Append(ID); // add topic id to list of chosen ones
        Debug.Log("ID of chosen topic: " + ID);

    }

    public void PrintChosenOrder()
    {
        dialogueOrderSt = "";

        if(dialogueOrder != null)
        {
            foreach (int ID in dialogueOrder)
            {
                if(ID != 0 && chosenSoFar[ID] == false)
                {
                    dialogueOrderSt += ID;
                    dialogueOrderSt += ", ";
                    chosenSoFar[ID] = true; // mark that this option was already chosen so it won't be added to array again if chosen again in the dialogue
                }
            }
        }

        Debug.Log(dialogueOrder == null);
        Debug.Log("order of chosen topics is: " + dialogueOrderSt);
    }

    public List<int> getFinalOrder()
    {
        return dialogueOrder; // return the list of topics
    }
}
