using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerLaunchState : PlayerState
{
    private GameObject currentWall;
    private float launchTime = 1f;
    private float timer;

    public override void Enter(PlayerStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        timer = launchTime;
        //stateInput.anim.Play("Player_Jump");
    }

    public override void Update(PlayerStateInput stateInput)
    {
        stateInput.playerController.isGrounded = stateInput.playerController.checkIfGrounded();

        if (stateInput.playerController.isGrounded)
        {
            character.ChangeState<PlayerIdleState>();
            return;
        }
        if (stateInput.rb.velocity.y <= 0)
        {
            character.ChangeState<PlayerFallingState>();
            return;
        }
        
        
        // Movement animations and saving previous input
        int horizontalMovement = (int)Mathf.Sign(stateInput.playerControls.InGame.Move.ReadValue<Vector2>().x);
        if (stateInput.playerControls.InGame.Move.ReadValue<Vector2>().x > -0.1f && stateInput.playerControls.InGame.Move.ReadValue<Vector2>().x < 0.1f) {
            horizontalMovement = 0;
        }
        if (horizontalMovement != 0 && stateInput.lastXDir != horizontalMovement)
        {
            Debug.Log(stateInput.lastXDir + " " + horizontalMovement);
            stateInput.player.transform.rotation = Quaternion.Euler(0, horizontalMovement == -1 ? 180 : 0, 0);
            
            // stateInput.spriteRenderer.flipX = horizontalMovement == -1;

        }
        stateInput.lastXDir = horizontalMovement;
    }
    public override void FixedUpdate(PlayerStateInput stateInput)
    {
        if (timer >= 0)
        {
            stateInput.playerController.HandleLerpMovement();
        } else
        {
            stateInput.playerController.HandleMovement();
        }
        

    }

}
