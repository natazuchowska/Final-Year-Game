using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Unity.VisualScripting;

public class WizardController : MonoBehaviour
{
    private Animator animator;
    public bool isSpeaking = false;
    public int convoTopic;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {

        // if there is a dialogueUI in the scene
        if (GameObject.Find("DialogueCanvas") != null)
        {
            if (GameObject.Find("DialogueCanvas").GetComponent<DialogueUI>().isOpen == true)
            {
                convoTopic = GameObject.Find("DialogueCanvas").GetComponent<DialogueUI>().getTopicID(); // check which topic is chosen is convo
                
                SetSpeakingAnimation();

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

    private void SetSpeakingAnimation() // make character speak when dialogue window open
    {
        animator.SetBool("isSpeaking", true);
    }

    private void SetIdleAnimation() // make character speak when dialogue window open
    {
        animator.SetBool("isSpeaking", false);
    }

    private void SetTopicAnimation(int topic)
    {
        animator.SetInteger("convoTopic", topic);
    }
}
