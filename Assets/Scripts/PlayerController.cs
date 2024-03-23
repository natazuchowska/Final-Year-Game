using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Unity.VisualScripting;
using System.Xml.Linq;

public class PlayerController : MonoBehaviour
{
    public Rigidbody2D rb;
    public Transform groundCheck;
    public LayerMask groundLayer;

    private float horizontal;
    private float vertical;

    private float speed = 2f;
    //private float jumpingPower = 8f;
    public bool isFacingRight = true;

    private Animator animator;
    public bool isMoving = false; // to set walking animation (was private before but inventory needs to access it to close on movement)
    //private bool isInAir = false; // to set jumping animation
    private bool isThinking = false; //to set thinking animation

    [SerializeField] private DialogueUI dialogueUI; // reference to interact with dialogues

    public DialogueUI DialogueUI => dialogueUI; // getter for dialogueUI

    public IInteractable Interactable { get; set; }

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        // if there is a dialogueUI in the scene
        if(GameObject.Find("DialogueCanvas") != null)
        {
            dialogueUI = GameObject.Find("DialogueCanvas").GetComponent<DialogueUI>();
            if (dialogueUI.isOpen) return; // do not move while the dialogue window is open
        }

        rb.velocity = new Vector2(horizontal * speed, vertical * speed); // calculate the speed of the player

        // set the correct direction of the character sprite
        if(!isFacingRight && horizontal > 0f)
        {
            Flip();
        }
        else if (isFacingRight && horizontal < 0f)
        {
            Flip();
        }

        // ----- MANAGE DIALOGUE ---------------------------------------------

        if(Input.GetKeyDown(KeyCode.T))
        {
            if(Interactable != null)
            {
                Interactable.Interact(this); // same as Interactable?.Interact(this) --> can then omit the 'if(Intractable != null)' as equivalent
            }
        } 
    }

    private void FixedUpdate()
    {
        SetWalkingAnimation();
    }

    private void SetWalkingAnimation() // set the walk animation if player is moving (isMoving == true)
    {

        isMoving = rb.velocity != Vector2.zero;
        animator.SetBool("isMoving", isMoving);
    }


    /*private bool isGrounded()
    {
        return Physics2D.OverlapCircle(groundCheck.position, 0.2f, groundLayer);
    }*/

    // change the size of the character sprite if going away/coming closer
    private void ChangeSize(float change)
    {
        Vector3 localScale = transform.localScale;
        
        if(change < 1.0f) // size down
        {
            if (Math.Abs(localScale.x) > 0.21f && localScale.y > 0.21f) // allow scale 0.1 at most
            {
                StartCoroutine("WaitForSec"); // slow down the resizing process

                localScale.x *= change;
                localScale.y *= change;
                transform.localScale = localScale;
            }
        }
        if (change > 1.0f) // size up
        {
            if (Math.Abs(localScale.x) < 0.29f && localScale.y < 0.29f) // allow scale 0.3 at most
            {
                StartCoroutine("WaitForSec"); // slow down the resizing process

                localScale.x *= change;
                localScale.y *= change;
                transform.localScale = localScale;
            }
        }
    }
    // flip the character baesd on which direction it is going
    private void Flip()
    {
        isFacingRight = !isFacingRight;
        Vector3 localScale = transform.localScale;
        localScale.x *= -1f;
        transform.localScale = localScale;
    }

    public void Move(InputAction.CallbackContext context)
    {
        horizontal = context.ReadValue<Vector2>().x;
        vertical = context.ReadValue<Vector2>().y;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        Debug.Log("OnCollisionEnter2D");
        Debug.Log(col.gameObject.name);
        if (col.gameObject.CompareTag("Plant"))
        {
            Debug.Log("Hit the plant:(");
            isThinking = true;
            animator.SetBool("isThinking", isThinking); //play thinking animaion when stepping upon the plant
        }
    }
    IEnumerator WaitForSec() // wait for 4 seconds and then stop displaying the message
    {
        yield return new WaitForSeconds(1);
    }


}
