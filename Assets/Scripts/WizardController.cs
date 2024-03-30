using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class WizardController : MonoBehaviour
{
    private Animator animator;
    public bool isSpeaking = false;
    public int convoTopic;

    private Camera mainCamera;

    [SerializeField] GameObject player;

    private void Awake()
    {
        if(SceneManager.GetActiveScene().buildIndex == 0)
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
            if (GameObject.Find("DialogueCanvas").GetComponent<DialogueUI>().isOpen == true && SceneManager.GetActiveScene().buildIndex == 0)
            {
                convoTopic = GameObject.Find("DialogueCanvas").GetComponent<DialogueUI>().getTopicID(); // check which topic is chosen is convo
             
                // SetSpeakingAnimation();

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
        player.GetComponent<PlayerController>().TalkToCharacter();
    }

    public void SetSpeakingAnimation() // make character speak when dialogue window open
    {
        animator.SetBool("isSpeaking", true);
    }

    public void SetIdleAnimation() // make character speak when dialogue window open
    {
        animator.SetBool("isSpeaking", false);
    }

    public void SetTopicAnimation(int topic)
    {
        if(SceneManager.GetActiveScene().buildIndex == 0) // only animate in the first scene
        {
            animator.SetInteger("convoTopic", topic);
        }
    }
}
