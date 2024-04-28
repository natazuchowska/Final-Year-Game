/*using System.Collections;
using System.Collections.Generic;*/
using UnityEngine;
/*using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.InputSystem;*/
using UnityEngine.SceneManagement;

public class WizardController : MonoBehaviour
{
    private Animator animator;
    public bool isSpeaking = false;
    public int convoTopic;

    [SerializeField] GameObject player;

    private void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex == 3) // main scene
        {
            animator = GetComponent<Animator>();
        }

        player = GameObject.FindGameObjectWithTag("Player");
    }

    private void FixedUpdate()
    {
        // if there is a dialogueUI in the scene
        if (GameObject.Find("DialogueCanvas") != null)
        {
            if (GameObject.Find("DialogueCanvas").GetComponent<DialogueUI>().isOpen == true && SceneManager.GetActiveScene().buildIndex == 3)
            {
                convoTopic = GameObject.Find("DialogueCanvas").GetComponent<DialogueUI>().getTopicID(); // check which topic is chosen is convo


                switch (convoTopic)
                {
                    case 0:
                        SetTopicAnimation(0);
                        return;
                    case 1:
                        SetTopicAnimation(0);
                        return;
                    case 2:
                        SetTopicAnimation(0);
                        return;
                    case 3:
                        SetTopicAnimation(3);
                        return;
                    case 4:
                        SetTopicAnimation(4);
                        return;
                }
            }
            else
            {
                SetIdleAnimation();
            }
        }
       
    }

    private void OnMouseDown()
    {
        if(PauseController.isPaused) { return;  }

        if (GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>().checkIfSolved(2) == true) // lamp turned off -> disable fish dialogue
        {
            return;
        }

        if (GameObject.Find("InventoryCanvas")!= null && GameObject.Find("InventoryCanvas").activeSelf == true)
        {
            Debug.Log("ENTERED THE CLOSE INVENTORY CASE WITH DIALOGUE");
            GameObject.Find("InventoryButton").GetComponent<InventoryManager>().OpenInventory(); // close inventory
        }
        player.GetComponent<PlayerMovement>().TalkToCharacter(); // BUILD V -> was PlayerController
    }

    public void SetSpeakingAnimation() // make character speak when dialogue window open
    {
        animator.SetBool("isSpeaking", true);
    }

    public void SetIdleAnimation() // make character speak when dialogue window open
    {
        if (SceneManager.GetActiveScene().buildIndex == 3) // only animate in the first scene
        {
            animator.SetBool("isSpeaking", false);
        }
    }

    public void SetTopicAnimation(int topic)
    {
        if(SceneManager.GetActiveScene().buildIndex == 3) // only animate in the first scene
        {
            animator.SetInteger("convoTopic", topic);
        }
    }
}
