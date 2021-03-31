using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJumpingState : PlayerState {
    
    public override void Enter(PlayerStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        stateInput.anim.Play("Player_Jump");
    }

    public override void Update(PlayerStateInput stateInput)
    {
        stateInput.playerController.isGrounded = stateInput.playerController.checkIfGrounded();

        if (stateInput.playerController.canDash() && stateInput.playerControls.InGame.Dash.WasPressedThisFrame()) {
            character.ChangeState<PlayerDashState>();
            return;
        }

        if (stateInput.playerControls.InGame.SwitchLeft.WasPressedThisFrame()) {
            stateInput.playerController.switchGun(false);
        }

        if (stateInput.playerControls.InGame.SwitchRight.WasPressedThisFrame()) {
            stateInput.playerController.switchGun(true);
        }

        if (stateInput.playerControls.InGame.Shoot.WasPressedThisFrame()) {
            stateInput.anim.SetTrigger("shoot");
            stateInput.playerController.Shoot();
        }

        if (stateInput.playerControls.InGame.Jump.WasPressedThisFrame() && stateInput.playerController.canJump())
        {
            stateInput.playerController.Jump();
        }

        if (stateInput.rb.velocity.y <= 0)
        {
            character.ChangeState<PlayerFallingState>();
            return;
        }
        if (stateInput.playerControls.InGame.Jump.WasReleasedThisFrame())
        {
            stateInput.playerController.JumpRelease();
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
            stateInput.player.transform.rotation = Quaternion.Euler(0, horizontalMovement == -1 ? 180 : 0, 0);
            // stateInput.spriteRenderer.flipX = horizontalMovement == -1;
        }
        stateInput.lastXDir = horizontalMovement;
    }


    public override void FixedUpdate(PlayerStateInput stateInput)
    {
        stateInput.playerController.HandleMovement();
    }
}
