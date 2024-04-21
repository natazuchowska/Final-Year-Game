using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DialogueActivator : MonoBehaviour, IInteractable // make the class implement the Interactable interface
{
    [SerializeField] public DialogueObject dialogueObject;

    [SerializeField] public GameObject dialogueBackground;

    public static bool displayNavArrow = false;


    private void Start()
    {
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
                displayNavArrow = false;
            }
            else
            {
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
            }

        }
        else
        {
            dialogueBackground.SetActive(true);
            player.DialogueUI.ShowDialogue(dialogueObject);
        }
    }
}
