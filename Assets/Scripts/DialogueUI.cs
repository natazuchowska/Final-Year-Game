using UnityEngine;
using TMPro;
using System.Collections;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    /*[SerializeField] private DialogueObject testDialogue;*/

    public bool isOpen { get; private set; }

    private ResponseHandler responseHandler;
    private TypeWriterEffect typeWriterEffect;

    [SerializeField] private AudioSource characterVoice;

    DialogueOrderManager recordOrder; // to store picked order of responses (topics)

    private void Start()
    {
        typeWriterEffect = GetComponent<TypeWriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();

        recordOrder = GameObject.Find("GameManager").GetComponent<DialogueOrderManager>();

        CloseDialogueBox(); // by default do not display the dialogue box
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        isOpen = true;

        dialogueBox.SetActive(true); // display the dialogue box
        StartCoroutine(StepThroughDialogue(dialogueObject));

        int ID = dialogueObject.getID();
        recordOrder.RecordResponse(ID); // get id of topic and pass to dialogueOrderManager to record it was picked
    }

    public IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        for (int i = 0; i<dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];

            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;
           
            if(i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) { break; }

            characterVoice.Play(); // play the talking sound

            yield return null; 
            yield return new WaitUntil(() => Input.GetKeyDown(KeyCode.Return)); // wait with displaying the next line until ENTER key not hit
        }

        if(dialogueObject.HasResponses)
        {

            responseHandler.ShowResponses(dialogueObject.Responses); // show responses if there are any
        }
        else
        { 
            CloseDialogueBox(); // close dialogue box after whole dialogue has been diaplayed
            characterVoice.Pause();

            recordOrder.PrintChosenOrder(); // show order of topics chosen by player
        }
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typeWriterEffect.Run(dialogue, textLabel);

        while(typeWriterEffect.isRunning)
        {
            yield return null; // wait 1 frame

            if(Input.GetKeyDown(KeyCode.Return))
            {
                typeWriterEffect.Stop();
            }
        }

    }

    private void CloseDialogueBox()
    {
        /*Debug.Log(recordOrder.PrintChosenOrder()); // print in which order the topics were chosen
*/
        isOpen = false;

        dialogueBox.SetActive(false);
        textLabel.text = string.Empty;
    }
}
