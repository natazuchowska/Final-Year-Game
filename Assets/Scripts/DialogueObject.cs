using UnityEngine;

[CreateAssetMenu(menuName ="Dialogue/DialogueObject")]
public class DialogueObject : ScriptableObject
{
    [SerializeField] int narrativeID;
    [SerializeField][TextArea] private string[] dialogue;
    [SerializeField] private Response[] responses;

    public string[] Dialogue => dialogue; //prevents code from the outside from witing to it (read only)

    public bool HasResponses => Responses != null && Responses.Length > 0; // check if there are any responses

    public Response[] Responses => responses; // getter for the responses 


    public int getID()
    {
        return this.narrativeID;
    }
}
