using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerFallingState : PlayerState {
    public override void Enter(PlayerStateInput stateInput, CharacterStateTransitionInfo transitionInfo = null)
    {
        stateInput.anim.Play("Player_Fall");
    }

    public override void Update(PlayerStateInput stateInput)
    {
        stateInput.playerController.isGrounded = Physics2D.OverlapCircle(stateInput.playerController.groundCheck.position, stateInput.playerController.checkRadius, stateInput.playerController.whatIsGround);

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
            stateInput.playerController.Shoot();
        }

        if (stateInput.playerControls.InGame.Jump.WasPressedThisFrame() && stateInput.playerController.canJump())
        {
            stateInput.playerController.hasJumpedOnce = true;
            stateInput.playerController.Jump();
            character.ChangeState<PlayerJumpingState>();
            return;
        }

        if (stateInput.playerController.isGrounded)
        {
            character.ChangeState<PlayerIdleState>();
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
