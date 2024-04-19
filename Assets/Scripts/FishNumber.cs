using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishNumber : MonoBehaviour
{
    public static int[] fishNum;
    public static int[] correctOrder;

    // Start is called before the first frame update
    void Start()
    {
        if(fishNum == null) // if not generated before
        {
            if (fishNum == null) // only generate the fish number once
            {
                fishNum = DialogueObject.GenerateFishNumber();

                correctOrder = new int[5];

                for (int i = 0; i < 4; i++)
                {
                    correctOrder[i] = fishNum[i];
                }
                correctOrder[4] = 9; // RED (id == 9) button for confirmation
            }
        }
    }

    public static int[] GetCorrectOrder()
    {
        return correctOrder;
    }

}
