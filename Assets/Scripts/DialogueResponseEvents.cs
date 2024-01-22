using UnityEngine;
using System;
using System.Runtime.CompilerServices;

public class DialogueResponseEvents : MonoBehaviour
{
    [SerializeField] private DialogueObject dialogueObject;
    [SerializeField] private ResponseEvent[] events;

    public ResponseEvent[] Events => events;

    public void OnValidate()
    {
        if(dialogueObject == null) return;
        if (dialogueObject.Responses == null) return;
        if (events != null && events.Length == dialogueObject.Responses.Length) return;

        for(int i=0; i<dialogueObject.Responses.Length; i++)
        {
            Response response = dialogueObject.Responses[i];

            if (events[i] != null)
            {

            }
        }
    }
}
