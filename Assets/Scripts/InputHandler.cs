using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
public class InputHandler : MonoBehaviour
{
    private Camera mainCamera;
    private bool playerIsJumping = false; // detect whether the player was clicked and is umping atm -> not used for now

    private void Awake()
    {
        mainCamera = Camera.main;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        if(!context.started) { return; }

        var rayHit = Physics2D.GetRayIntersection(mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue()));

        if(!rayHit.collider) { return; }

        Vector3 itemScale = rayHit.transform.localScale;

        // if clicking on player make him jump -> disabled this functionality for now
        if(rayHit.collider.gameObject.CompareTag("Player"))
        {
            Animator playerAnimator = rayHit.collider.gameObject.GetComponent<Animator>();

            if(!playerIsJumping) { playerAnimator.SetBool("isInAir", true); }
            else { playerAnimator.SetBool("isInAir", false); }

            playerIsJumping = !playerIsJumping; // make player stop jumping by clicking on him again
        }
    }
}
