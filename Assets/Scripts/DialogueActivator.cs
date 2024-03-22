using UnityEngine;

public class DialogueActivator : MonoBehaviour, IInteractable // make the class implement the Interactable interface
{
    [SerializeField] private DialogueObject dialogueObject;
    // DialogueUI dialogueUI;


    private void Start()
    {
        // dialogueUI = GameObject.Find("DialogueCanvas").GetComponent<DialogueUI>();
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
        player.DialogueUI.ShowDialogue(dialogueObject);
    }
}
