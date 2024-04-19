using UnityEngine;
using UnityEngine.SceneManagement;

[CreateAssetMenu(menuName ="Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] int narrativeID;
    [SerializeField][TextArea] private string[] dialogue;
    [SerializeField] private Response[] responses;

    [SerializeField] public static int[] fishNumber;
    [SerializeField] public static int[] possibleNumbers = {0, 1, 3, 4, 5, 8}; // only numbers on the calculator which are unique

    public string[] Dialogue => dialogue; //prevents code from the outside from witing to it (read only)

    public bool HasResponses => Responses != null && Responses.Length > 0; // check if there are any responses

    public Response[] Responses => responses; // getter for the responses 

    public static int[] GenerateFishNumber()
    {
        fishNumber = new int[4];

        for(int i=0; i<4; i++)
        {
            fishNumber[i] = possibleNumbers[Random.Range(0, 6)]; // choose a number randomly from the possible numbers array
        }

        Debug.Log("calling GENERATE FISH NUM method, number is " + fishNumber[0] + fishNumber[1] + fishNumber[2] + fishNumber[3]);

        return fishNumber;
    }

    public int getID()
    {
        return this.narrativeID;
    }
}
