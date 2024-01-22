using UnityEngine;

[System.Serializable]
public class Response 
{
    [SerializeField] private string responseText;
    [SerializeField] private DialogueObject dialogueObject;

    public string ResponseText => responseText; // getter method to get the response text

    public DialogueObject DialogueObject => dialogueObject; // getter method to get the dialogue object
}
