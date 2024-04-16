using UnityEngine;
using TMPro;
using System.Collections;
using UnityEngine.SceneManagement;

public class DialogueUI : MonoBehaviour
{
    [SerializeField] private GameObject dialogueBox;
    [SerializeField] private TMP_Text textLabel;
    /*[SerializeField] private DialogueObject testDialogue;*/

    public bool isOpen { get; private set; }

    private ResponseHandler responseHandler;
    public TypeWriterEffect typeWriterEffect;

    [SerializeField] private AudioSource characterVoice;
    [SerializeField] private AudioSource topic1;
    [SerializeField] private AudioSource topic2;
    [SerializeField] private AudioSource topic3;
    [SerializeField] private AudioSource topic4;
    [SerializeField] public AudioSource speakNow;

    GameObject Character;

    int ID;

    DialogueOrderManager recordOrder; // to store picked order of responses (topics)

    private void Start()
    {
        typeWriterEffect = GetComponent<TypeWriterEffect>();
        responseHandler = GetComponent<ResponseHandler>();

        speakNow = characterVoice; // initialise with default voice

        recordOrder = GameObject.Find("GameManager").GetComponent<DialogueOrderManager>();

        CloseDialogueBox(); // by default do not display the dialogue box
    }

    public void ShowDialogue(DialogueObject dialogueObject)
    {
        isOpen = true;

        dialogueBox.SetActive(true); // display the dialogue box

        ID = dialogueObject.getID();
        recordOrder.RecordResponse(ID); // get id of topic and pass to dialogueOrderManager to record it was picked

        // choose and assign the approriate voice for the topic picked -> main scene
        if(SceneManager.GetActiveScene().buildIndex == 3) // **WAS 0
        {
            switch (ID)
            {
                case 0:
                    speakNow = characterVoice;
                    break;
                case 1:
                    speakNow = topic1;
                    break;
                case 2:
                    speakNow = topic2;
                    break;
                case 3:
                    speakNow = topic3;
                    break;
                case 4:
                    speakNow = topic4;
                    break;
            }
        }

        // swimming pool
        if(SceneManager.GetActiveScene().buildIndex == 11)
        {
            GameObject.Find("MainMusic").GetComponent<BackgroundMusicManager>().currentAudio.Pause();
        }

        speakNow.Play(); // play the character voice sound
        StartCoroutine(StepThroughDialogue(dialogueObject));
    }

    public IEnumerator StepThroughDialogue(DialogueObject dialogueObject)
    {
        if (GameObject.Find("Character") != null)
        {
            if(SceneManager.GetActiveScene().buildIndex == 0) // only set animations in 1st scene
            {
                GameObject.Find("Character").GetComponent<WizardController>().SetSpeakingAnimation();
            }
        }

        for (int i = 0; i<dialogueObject.Dialogue.Length; i++)
        {
            string dialogue = dialogueObject.Dialogue[i];

            yield return RunTypingEffect(dialogue);

            textLabel.text = dialogue;
           
            if(i == dialogueObject.Dialogue.Length - 1 && dialogueObject.HasResponses) { break; }

            yield return null; 
            yield return new WaitUntil(() => (Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))); // wait with displaying the next line until ENTER key not hit
        }

        if(dialogueObject.HasResponses)
        {
            responseHandler.ShowResponses(dialogueObject.Responses); // show responses if there are any

            if (GameObject.Find("Character") != null)
            {
                GameObject.Find("Character").GetComponent<WizardController>().SetTopicAnimation(0);
                GameObject.Find("Character").GetComponent<WizardController>().SetIdleAnimation();
            }
        }
        else
        {
            // ====================
            GameObject.Find("CursorManager").GetComponent<CursorManager>().ChangeCursor(4); // change cursor to normal when dialogue finished
            // ====================
            CloseDialogueBox(); // close dialogue box after whole dialogue has been diaplayed
           

            if(GameObject.Find("DialogueCircle") != null)
            {
                GameObject.Find("DialogueCircle").GetComponent<DialogueActivator>().dialogueBackground.SetActive(false);
            }

            // swimming pool
            if (SceneManager.GetActiveScene().buildIndex == 11)
            {
                GameObject.Find("MainMusic").GetComponent<BackgroundMusicManager>().currentAudio.Play();
            }

            recordOrder.PrintChosenOrder(); // show order of topics chosen by player
        }
        speakNow.Pause();
    }

    private IEnumerator RunTypingEffect(string dialogue)
    {
        typeWriterEffect.Run(dialogue, textLabel);

        while(typeWriterEffect.isRunning)
        {
            yield return null; // wait 1 frame

            if(Input.GetKeyDown(KeyCode.Return) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.RightArrow))
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

    public int getTopicID()
    {
        return ID;
    }
}
