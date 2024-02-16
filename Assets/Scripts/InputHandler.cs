using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputHandler : MonoBehaviour
{
    private Camera mainCamera;
    private bool playerIsJumping = false; // detect whether the player was clicked and is umping atm

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if(!context.started) { return; }

        var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));

        if(!rayHit.collider) { return; }

        // Debug.Log(rayHit.collider.gameObject.name); //display the name of the clicked object on the console

        Vector3 itemScale = rayHit.transform.localScale;
        // if not player nor goTo button - resize object on click
        /*if (!rayHit.collider.gameObject.CompareTag("Player") && !rayHit.collider.gameObject.CompareTag("Untagged") && !rayHit.collider.gameObject.CompareTag("GoTo")) //untagged object usually invisible/not important so do not rescale them
        {
            // make object smaller
            itemScale.x *= 0.8f;
            itemScale.y *= 0.8f;
            rayHit.transform.localScale = itemScale;

            // bring object back to original size after 0.2sec
            StartCoroutine("WaitForSec");
        }*/

        // do something depending on whether yes/no was chosen for displayed prompt
        if(rayHit.collider.gameObject.CompareTag("YES"))
        {
            Debug.Log("PLAY SONG NOW");
        }
        if (rayHit.collider.gameObject.CompareTag("NO"))
        {
            Debug.Log("Player does not want to listen:(");
        }

        // if clicking on player make him jump
        if(rayHit.collider.gameObject.CompareTag("Player"))
        {
            Animator playerAnimator = rayHit.collider.gameObject.GetComponent<Animator>();

            if(!playerIsJumping) { playerAnimator.SetBool("isInAir", true); }
            else { playerAnimator.SetBool("isInAir", false); }

            playerIsJumping = !playerIsJumping; // make player stop jumping by clicking on him again
        }
    }

    IEnumerator WaitForSec() // wait for 0.2 seconds and scale back the object to original size
    {
        yield return new WaitForSeconds(0.2f);
        Vector3 scale = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue())).transform.localScale;
        scale.x *= 1.25f;
        scale.y *= 1.25f;
        Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue())).transform.localScale = scale;
    }
}
