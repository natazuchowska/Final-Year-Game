using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    [SerializeField]
    private float _speed = 2.4f;

    //private float jumpingPower = 8f;
    public bool isFacingRight = true;

    private Animator animator;
    public bool isMoving = false; // to set walking animation (was private before but inventory needs to access it to close on movement)
    //private bool isInAir = false; // to set jumping animation
    public bool isThinking = false; //to set thinking animation

    private AudioSource walkAudio; // walking sound
    private bool walkSoundPlaying = false;

    [SerializeField] private DialogueUI dialogueUI; // reference to interact with dialogues

    public DialogueUI DialogueUI => dialogueUI; // getter for dialogueUI

    public bool canTalkRn = false; // check if player can talk to a character (flag)

    [SerializeField] GameObject dialogueBackground;

    int sceneID;

    public float horizontalInput;

    public IInteractable Interactable { get; set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();

        walkAudio = this.GetComponent<AudioSource>();
    }


    // Update is called once per frame
    void Update()
    {
        horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0);
        transform.Translate(direction * _speed * Time.deltaTime);

        sceneID = SceneManager.GetActiveScene().buildIndex; // get the id of current scene    

        if ((sceneID == 3 || sceneID == 11) && dialogueBackground == null)
        {
            dialogueBackground = GameObject.Find("DialogueCircle").GetComponent<DialogueActivator>().dialogueBackground;
            // dialogueBackground.SetActive(false);
        }

        // if there is a dialogueUI in the scene
        if (GameObject.Find("DialogueCanvas") != null)
        {
            dialogueUI = GameObject.Find("DialogueCanvas").GetComponent<DialogueUI>();
            if (dialogueUI.isOpen) return; // do not move while the dialogue window is open
        }

        // rb.velocity = new Vector2(horizontal * speed, vertical * speed); // calculate the speed of the player

        // set the correct direction of the character sprite
        if (!isFacingRight && horizontalInput > 0f)
        {
            Flip();
        }
        else if (isFacingRight && horizontalInput < 0f)
        {
            Flip();
        }
    }

    public void TalkToCharacter()
    {
        Debug.Log("CALLED TALK TO CHARACTER METHOD");

        if (GameObject.Find("DialogueCircle").GetComponent<SpeechBubbleManager>().inArea == true)
        {
            // dialogueBackground = GameObject.Find("talking_background");
            // dialogueBackground.SetActive(true);

            if (sceneID == 3 || sceneID == 11)
            {
                dialogueBackground.SetActive(true);

                isThinking = true;
                // SetThinkingAnimation();
            }

            DialogueObject dialogueObject = GameObject.Find("DialogueCircle").GetComponent<DialogueActivator>().dialogueObject;
            this.DialogueUI.ShowDialogue(dialogueObject);
            // Interactable.Interact(this); // same as Interactable?.Interact(this) --> can then omit the 'if(Intractable != null)' as equivalent
        }
    }

    private void FixedUpdate()
    {
        SetWalkingAnimation();
        SetThinkingAnimation();

        if (isMoving && !walkSoundPlaying)
        {
            walkAudio.Play();
            walkSoundPlaying = true;
        }
        else
        {
            walkAudio.Pause();
            walkSoundPlaying = false;
        }
    }

    private void SetWalkingAnimation() // set the walk animation if player is moving (isMoving == true)
    {
        isMoving = horizontalInput!= 0f;
        animator.SetBool("isMoving", isMoving);
    }

    private void SetThinkingAnimation() // set the thinking animation when talking to another character
    {
        animator.SetBool("isThinking", isThinking);
    }


    // change the size of the character sprite if going away/coming closer

    // flip the character baesd on which direction it is going
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    IEnumerator WaitForSec() // wait for 4 seconds and then stop displaying the message
    {
        yield return new WaitForSeconds(1);
    }


}
