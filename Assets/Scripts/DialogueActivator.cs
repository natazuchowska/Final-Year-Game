using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueActivator : MonoBehaviour, IInteractable // make the class implement the Interactable interface
{
    [SerializeField] public DialogueObject dialogueObject;
    // DialogueUI dialogueUI;

    [SerializeField] public GameObject dialogueBackground;

    private static bool alreadyTalked = false;
    public static bool displayNavArrow = false;


    private void Start()
    {
        // dialogueUI = GameObject.Find("DialogueCanvas").GetComponent<DialogueUI>();
/*
        if(dialogueBackground = null) // not initialized yet (2nd dialogue scene)
        {
            dialogueBackground = GameObject.FindGameObjectWithTag("DialogueBackground");
        }*/

        if(dialogueBackground != null)
        {
            dialogueBackground.SetActive(false);
        }
    }

    void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 11)
        {
            if(dialogueBackground.activeSelf == true)
            {
                // GameObject.Find("GoBackButton").GetComponent<Button>().interactable = false; // hide the nav arrow when dialogue displayed
                displayNavArrow = false;
            }
            else
            {
                // GameObject.Find("GoBackButton").GetComponent<Button>().interactable = true;
                displayNavArrow = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && collision.TryGetComponent(out PlayerController player))
        {
            player.Interactable = this;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && collision.TryGetComponent(out PlayerController player))
        {
            if(player.Interactable is DialogueActivator dialogueActivator && dialogueActivator == this)
            {
                player.Interactable = null;
            }
        }
    }

    public void Interact(PlayerController player)
    {
        if(SceneManager.GetActiveScene().buildIndex == 11) // swimming pool -> disable fish talking if light turned off
        {
            if(LampController.lightOn == true) // only display fish dialogue if lamp is on
            {
                dialogueBackground.SetActive(true);
                player.DialogueUI.ShowDialogue(dialogueObject);
                alreadyTalked = true; 
            }

        }
        else
        {
            dialogueBackground.SetActive(true);
            player.DialogueUI.ShowDialogue(dialogueObject);
        }
        
    }
}
