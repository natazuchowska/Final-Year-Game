using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable // make the class implement the Interactable interface
{
    [SerializeField] public DialogueObject dialogueObject;
    // DialogueUI dialogueUI;

    [SerializeField] public GameObject dialogueBackground;


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
        dialogueBackground.SetActive(true);
        player.DialogueUI.ShowDialogue(dialogueObject);
    }
}
